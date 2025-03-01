using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Q_Manage.Models;

namespace Q_Manage.Controllers
{
    public class ClienteController : Controller
    {
        private readonly QmanageDbContext _context;

        public ClienteController(QmanageDbContext context)
        {
            _context = context;
        }

        // GET: Cliente
        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            var qmanageDbContext = _context.Proyectos.Include(p => p.EstadoPago).Include(p => p.EstadoProyecto).Include(p => p.Usuario).Where(b => b.UsuarioId == userId);
            return View(await qmanageDbContext.ToListAsync());
        }

        
    }
}
