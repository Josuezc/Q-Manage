using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Q_Manage.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Q_Manage.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly QmanageDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(
            ILogger<HomeController> logger,
            QmanageDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Redirect("/Identity/Account/Login");

            var roles = await _userManager.GetRolesAsync(user);
            var rol = roles.FirstOrDefault();

            ViewBag.Rol = rol;

            if (rol == "Admin")
            {
                ViewBag.Proyectos = await _context.Proyectos
                    .Include(p => p.EstadoProyecto)
                    .Include(p => p.EstadoPago)
                    .ToListAsync();

                ViewBag.TotalPagos = await _context.Pagos.SumAsync(p => p.Monto);
                ViewBag.TotalProyectos = await _context.Proyectos.CountAsync();
            }
            else if (rol == "User")
            {
                var equipos = await _context.EmpleadoPorEquipos
                    .Where(e => e.UsuarioId == user.Id)
                    .Select(e => e.EquipoId)
                    .ToListAsync();

                var proyectos = await _context.ProyectosPorEquipos
                    .Where(p => equipos.Contains(p.EquipoId))
                    .Include(p => p.Proyecto)
                        .ThenInclude(proy => proy.EstadoProyecto)
                    .Include(p => p.Proyecto)
                        .ThenInclude(proy => proy.EstadoPago)
                    .Select(p => p.Proyecto)
                    .ToListAsync();

                ViewBag.Proyectos = proyectos;
            }
            else if (rol == "Client")
            {
                var proyectos = await _context.Proyectos
                    .Where(p => p.UsuarioId == user.Id)
                    .Include(p => p.EstadoProyecto)
                    .Include(p => p.EstadoPago)
                    .ToListAsync();

                ViewBag.Proyectos = proyectos;
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
