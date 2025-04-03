using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Q_Manage.Models;

namespace Q_Manage.Controllers
{
    public class EquiposController : Controller
    {
        private readonly QmanageDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public EquiposController(QmanageDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        private async Task<List<ApplicationUser>> ObtenerUsuariosParaEquipo(string roleName, int? equipoId = null)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role == null) return new List<ApplicationUser>();

            var userIds = await _context.UserRoles
                .Where(ur => ur.RoleId == role.Id)
                .Select(ur => ur.UserId)
                .ToListAsync();

            var empleadosConDosEquipos = _context.EmpleadoPorEquipos
                .GroupBy(e => e.UsuarioId)
                .Where(g => g.Count() >= 2)
                .Select(g => g.Key)
                .ToHashSet();

            var empleadosEnEquipoActual = new HashSet<string>();
            if (equipoId.HasValue)
            {
                empleadosEnEquipoActual = _context.EmpleadoPorEquipos
                    .Where(e => e.EquipoId == equipoId.Value)
                    .Select(e => e.UsuarioId)
                    .ToHashSet();
            }

            return await _userManager.Users
                .Where(u => userIds.Contains(u.Id) && (!empleadosConDosEquipos.Contains(u.Id) || empleadosEnEquipoActual.Contains(u.Id)))
                .ToListAsync();
        }



        public async Task<IActionResult> Index()
        {
            var equipos = await _context.Equipos
                .Include(e => e.empleadorPorEquipos)
                .Include(e => e.ProyectosPorEquipos)
                .ToListAsync();

            return View(equipos ?? new List<Equipo>());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AsignarProyecto(int equipoId, int proyectoId)
        {
            var existeAsignacion = await _context.ProyectosPorEquipos
                .AnyAsync(p => p.EquipoId == equipoId && p.ProyectoId == proyectoId);

            if (!existeAsignacion)
            {
                var asignacion = new ProyectosPorEquipo
                {
                    EquipoId = equipoId,
                    ProyectoId = proyectoId
                };

                _context.ProyectosPorEquipos.Add(asignacion);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Details), new { id = equipoId });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DesasignarProyecto(int equipoId, int proyectoId)
        {
            var asignacion = await _context.ProyectosPorEquipos
                .FirstOrDefaultAsync(p => p.EquipoId == equipoId && p.ProyectoId == proyectoId);

            if (asignacion != null)
            {
                _context.ProyectosPorEquipos.Remove(asignacion);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Details), new { id = equipoId });
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipo = await _context.Equipos
                .Include(e => e.empleadorPorEquipos)
                    .ThenInclude(ep => ep.Usuario)
                .Include(e => e.ProyectosPorEquipos)
                    .ThenInclude(pe => pe.Proyecto)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (equipo == null)
            {
                return NotFound();
            }

            ViewBag.ProyectosDisponibles = await _context.Proyectos.ToListAsync();

            return View(equipo);
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Usuarios = await ObtenerUsuariosParaEquipo("User");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Equipo equipo, List<string> UsuarioIds)
        {
            if (ModelState.IsValid)
            {
                _context.Add(equipo);
                await _context.SaveChangesAsync();

                foreach (var usuarioId in UsuarioIds)
                {
                    var empleadoPorEquipo = new EmpleadoPorEquipo
                    {
                        UsuarioId = usuarioId,
                        EquipoId = equipo.Id
                    };
                    _context.EmpleadoPorEquipos.Add(empleadoPorEquipo);
                }
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Usuarios = await ObtenerUsuariosParaEquipo("User");
            return View(equipo);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            var equipo = await _context.Equipos
               .Include(e => e.empleadorPorEquipos)
               .FirstOrDefaultAsync(e => e.Id == id);

            if (equipo == null)
            {
                return NotFound();
            }

            ViewBag.Usuarios = await ObtenerUsuariosParaEquipo("User", id);
            return View(equipo);
        }
        [Authorize(Roles = "Admin")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Equipo equipo, List<string> UsuarioIds)
        {
            if (id != equipo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipo);
                    await _context.SaveChangesAsync();

                    var empleadosAnteriores = _context.EmpleadoPorEquipos.Where(e => e.EquipoId == equipo.Id);
                    _context.EmpleadoPorEquipos.RemoveRange(empleadosAnteriores);
                    await _context.SaveChangesAsync();

                    foreach (var usuarioId in UsuarioIds)
                    {
                        var empleadoPorEquipo = new EmpleadoPorEquipo
                        {
                            UsuarioId = usuarioId,
                            EquipoId = equipo.Id
                        };
                        _context.EmpleadoPorEquipos.Add(empleadoPorEquipo);
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Equipos.Any(e => e.Id == equipo.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Usuarios = _context.Users.ToList();
            return View(equipo);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipo = await _context.Equipos
                .Include(e => e.ProyectosPorEquipos) 
                    .ThenInclude(pe => pe.Proyecto)  
                .Include(e => e.empleadorPorEquipos) 
                    .ThenInclude(ep => ep.Usuario)  
                .FirstOrDefaultAsync(m => m.Id == id);

            if (equipo == null)
            {
                return NotFound();
            }

            return View(equipo);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var equipo = await _context.Equipos.FindAsync(id);
            if (equipo != null)
            {
                _context.Equipos.Remove(equipo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquipoExists(int id)
        {
            return _context.Equipos.Any(e => e.Id == id);
        }
    }
}
