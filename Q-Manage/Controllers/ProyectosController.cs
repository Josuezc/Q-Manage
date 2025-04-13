using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Q_Manage.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

public class ProyectosController : Controller
{
    private readonly QmanageDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public ProyectosController(QmanageDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    private async Task<List<ApplicationUser>> ObtenerUsuariosPorRol(string roleName)
    {
        var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);

        if (role == null)
        {
            Console.WriteLine($"⚠️ Rol '{roleName}' no encontrado en la base de datos.");
            return new List<ApplicationUser>();
        }

        var userIds = await _context.UserRoles
            .Where(ur => ur.RoleId == role.Id)
            .Select(ur => ur.UserId)
            .ToListAsync();

        if (!userIds.Any())
        {
            Console.WriteLine($"⚠️ No hay usuarios con el rol '{roleName}'.");
            return new List<ApplicationUser>();
        }

        var users = await _context.Users
            .Where(u => userIds.Contains(u.Id))
            .ToListAsync();

        Console.WriteLine($"✅ Se encontraron {users.Count} usuarios con el rol '{roleName}'.");
        return users;
    }


    public async Task<IActionResult> Index()
    {
        var proyectos = await _context.Proyectos
            .Include(p => p.EstadoPago)
            .Include(p => p.EstadoProyecto)
            .Include(p => p.Usuario)
            .ToListAsync();

        return View(proyectos);
    }
 
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var proyecto = await _context.Proyectos
            .Include(p => p.EstadoPago)
            .Include(p => p.EstadoProyecto)
            .Include(p => p.Comentarios)
            .ThenInclude(c => c.Usuario)
            .Include(p => p.Usuario)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (proyecto == null) return NotFound();

        return View(proyecto);
    }
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create()
    {
        ViewBag.EstadosPago = await _context.EstadoPagos.ToListAsync();
        ViewBag.EstadosProyecto = await _context.EstadoProyectos.ToListAsync();
        ViewBag.Clientes = await ObtenerUsuariosPorRol("Client");

        return View();
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Nombre,Descripcion,FechaInicio,FechaFinalizacion,EstadoPagoId,EstadoProyectoId,UsuarioId")] Proyecto proyecto)
    {
        if (string.IsNullOrEmpty(proyecto.UsuarioId))
        {
            ModelState.AddModelError("UsuarioId", "Debe seleccionar un cliente para el proyecto.");
        }
        if (ModelState.IsValid)
        {
            _context.Add(proyecto);
            await _context.SaveChangesAsync();

            var kanban = new Kanban
            {
                Nombre = $"Tablero de {proyecto.Nombre}",
                ProyectoId = proyecto.Id
            };
            _context.Kanbans.Add(kanban);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        ViewBag.EstadosPago = await _context.EstadoPagos.ToListAsync();
        ViewBag.EstadosProyecto = await _context.EstadoProyectos.ToListAsync();
        ViewBag.Clientes = await ObtenerUsuariosPorRol("Client");

        return View(proyecto);
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var proyecto = await _context.Proyectos.FindAsync(id);
        if (proyecto == null) return NotFound();

        ViewBag.EstadosPago = await _context.EstadoPagos.ToListAsync();
        ViewBag.EstadosProyecto = await _context.EstadoProyectos.ToListAsync();
        ViewBag.Clientes = await ObtenerUsuariosPorRol("Client");

        return View(proyecto);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion,FechaInicio,FechaFinalizacion,EstadoPagoId,EstadoProyectoId,UsuarioId")] Proyecto proyecto)
    {
        if (id != proyecto.Id) return NotFound();

        if (string.IsNullOrEmpty(proyecto.UsuarioId))
        {
            ModelState.AddModelError("UsuarioId", "Debe seleccionar un cliente para el proyecto.");
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(proyecto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Proyectos.Any(e => e.Id == proyecto.Id))
                {
                    return NotFound();
                }
                throw;
            }
        }

        ViewBag.EstadosPago = await _context.EstadoPagos.ToListAsync();
        ViewBag.EstadosProyecto = await _context.EstadoProyectos.ToListAsync();
        ViewBag.Clientes = await ObtenerUsuariosPorRol("Client");

        return View(proyecto);
    }

    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var proyecto = await _context.Proyectos
            .Include(p => p.EstadoPago)
            .Include(p => p.EstadoProyecto)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (proyecto == null) return NotFound();

        return View(proyecto);
    }
    [Authorize(Roles = "Admin")]
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var proyecto = await _context.Proyectos
            .Include(p => p.Pagos)
            .Include(p => p.Comentarios)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (proyecto == null)
            return NotFound();

        if (proyecto.Pagos?.Any() == true || proyecto.Comentarios?.Any() == true)
        {
            TempData["ErrorEliminar"] = "❌ No se puede eliminar el proyecto porque tiene elementos asociados. Elimine primero los pagos, comentarios u otras relaciones.";
            return RedirectToAction(nameof(Delete), new { id });
        }

        _context.Proyectos.Remove(proyecto);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}
