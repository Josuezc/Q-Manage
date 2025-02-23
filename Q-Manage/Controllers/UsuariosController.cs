using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Q_Manage.Models;
using System.Threading.Tasks;
namespace Q_Manage.Controllers
{
   

    namespace Q_Manage.Controllers
    {
        //[Authorize(Roles = "Admin")] 
        public class UsuariosController : Controller
        {
            private readonly UserManager<IdentityUser> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;
            //private QmanageDbContext _context = new QmanageDbContext();
            public UsuariosController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
            {
                _userManager = userManager;
                _roleManager = roleManager;
            }

            // 🔹 LISTAR USUARIOS
            public async Task<IActionResult> Index()
            {
                var usuarios = await _userManager.Users.ToListAsync(); // ✅ Asegurar que usa ApplicationUser
                return View(usuarios);
            }

            // 🔹 CREAR USUARIO (GET)
            public IActionResult Crear()
            {
                return View();
            }

        }
    }
}
