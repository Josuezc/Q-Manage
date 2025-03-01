using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Q_Manage.Models;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Q_Manage.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly QmanageDbContext _context;
        public UsuariosController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, QmanageDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        // 🔹 LISTAR USUARIOS
        public async Task<IActionResult> Index()
        {

            string userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (User.IsInRole("User"))
            {
                var users = await _userManager.Users.Include(u => u.Proyectos).Where(b => b.Id == userId).ToListAsync();
                var proyectouser = await _context.ProyectosPorEquipos
                             .Include(p => p.Proyecto)
                             .Include(e => e.Equipo)
                                 .ThenInclude(epe => epe.empleadorPorEquipos)
                                     .ThenInclude(u => u.Usuario)
                                     .Where(e => e.Equipo.empleadorPorEquipos.Any(ep => ep.UsuarioId == userId))
                                     .Select(e => e.Proyecto.Nombre) 
                                     .Distinct()
                             .ToListAsync();

                ViewData["ProyectosUsuarios"] =proyectouser;
                return View(users);
            }
            if (User.IsInRole("Admin"))
            {
                var users = await _userManager.Users.Include(u => u.Proyectos).ToListAsync();
                var proyectosUsuarios = await _context.ProyectosPorEquipos
                 .Include(p => p.Proyecto)
                 .Include(e => e.Equipo)
                     .ThenInclude(epe => epe.empleadorPorEquipos)
                         .ThenInclude(u => u.Usuario)
                 .SelectMany(e => e.Equipo.empleadorPorEquipos.Select(ep => new
                 {
                     UsuarioId = ep.UsuarioId,
                     ProyectoNombre = e.Proyecto.Nombre
                 }))
                 .GroupBy(e => e.UsuarioId) 
                 .ToDictionaryAsync(g => g.Key, g => g.Select(p => p.ProyectoNombre).Distinct().ToList()); 

                ViewData["ProyectosUsuarios"] = proyectosUsuarios;
                return View(users);
            }
           
            return View();
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Usuarios/Create
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Create(ApplicationUser user)
        {
            ViewData["Roles"] = new SelectList(await _roleManager.Roles.ToListAsync(), "Name", "Name");
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApplicationUser user, string password, string roleUser)
        {
            if (ModelState.IsValid)
            {
                var passwordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$");

                if (!passwordRegex.IsMatch(password))
                {
                    ModelState.AddModelError("password", "La contraseña debe tener al menos 6 caracteres, incluyendo una mayúscula, una minúscula, un número y un símbolo.");
                  
                }
                var emailExist = await _userManager.FindByEmailAsync(user.Email);
                if (emailExist != null)
                {
                    ModelState.AddModelError("Email", "Este correo electrónico ya está registrado.");
                }
               
                if (user.Email != user.UserName)
                {
                    ModelState.AddModelError("Email", "El correo no coincide.");
                }
                var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
                if (string.IsNullOrWhiteSpace(user.Email) || !emailRegex.IsMatch(user.Email))
                {
                    ModelState.AddModelError("Email", "Debe ingresar un correo electrónico válido.");
                   
                }

                if (!ModelState.IsValid)
                {
                    ViewData["PasswordError"] = "La contraseña debe tener al menos 6 caracteres, incluyendo una mayúscula, una minúscula, un número y un símbolo.";
                    return View(user);
                }

                
                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    user.EmailConfirmed = true;
                    await _userManager.UpdateAsync(user);

                    await _userManager.AddToRoleAsync(user, roleUser);

                    return RedirectToAction(nameof(Index));
                }

            }
            ViewData["Roles"] = new SelectList(await _roleManager.Roles.ToListAsync(), "Name", "Name");
            return View(user);
            
        }

        // GET: Usuarios/Edit/5
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var roles = await _roleManager.Roles.ToListAsync();
           
            var userRoles = await _userManager.GetRolesAsync(user);

            ViewData["Roles"] = roles.Select(r => new SelectListItem
            {
                Value = r.Name,
                Text = r.Name,
                Selected = userRoles.Contains(r.Name)
            }).ToList();

            return View(user);
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ApplicationUser user, string? phone, bool emailConfi, List<string> selectedRoles)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByIdAsync(id);
                if (existingUser == null)
                {
                    return NotFound();
                }

                // Actualizar datos básicos del usuario
                existingUser.Email = user.Email;
                existingUser.PhoneNumber = string.IsNullOrEmpty(phone) ? null : phone;
                existingUser.EmailConfirmed = emailConfi;

                var updateResult = await _userManager.UpdateAsync(existingUser);
                if (!updateResult.Succeeded)
                {
                    foreach (var error in updateResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(user);
                }

                // Actualizar roles (solo si el usuario es Admin)
                if (User.IsInRole("Admin"))
                {
                    var currentRoles = await _userManager.GetRolesAsync(existingUser);
                    var rolesToAdd = selectedRoles.Except(currentRoles);
                    var rolesToRemove = currentRoles.Except(selectedRoles);

                    await _userManager.AddToRolesAsync(existingUser, rolesToAdd);
                    await _userManager.RemoveFromRolesAsync(existingUser, rolesToRemove);
                }

                return RedirectToAction(nameof(Index));
            }

            // Recargar los roles en caso de error
            var roles = await _roleManager.Roles.ToListAsync();
            ViewData["Roles"] = roles.Select(r => new SelectListItem
            {
                Value = r.Name,
                Text = r.Name,
                Selected = selectedRoles.Contains(r.Name)
            }).ToList();

            return View(user);
        }

        // GET: Usuarios/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Usuarios/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
