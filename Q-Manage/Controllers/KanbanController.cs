namespace Q_Manage.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Q_Manage.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

public class KanbanController : Controller
{
    private readonly QmanageDbContext _context;

    public KanbanController(QmanageDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(int proyectoId)
    {
        var kanban = await _context.Kanbans
            .Include(k => k.Tareas)
                .ThenInclude(t => t.EstadoTarea)
            .Include(k => k.Tareas)
                .ThenInclude(t => t.PrioridadTarea)
            .FirstOrDefaultAsync(k => k.ProyectoId == proyectoId);

        if (kanban == null)
        {
            return NotFound("❌ No se encontró el tablero Kanban.");
        }

        // ✅ Pasamos las listas de Estados y Prioridades a la Vista
        ViewBag.EstadosTarea = await _context.EstadosTarea.ToListAsync();
        ViewBag.PrioridadesTarea = await _context.PrioridadesTarea.ToListAsync();

        ViewBag.ProyectoId = proyectoId;
        return View(kanban);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CrearTarea([Bind("Titulo,Descripcion,KanbanId,EstadoTareaId,PrioridadTareaId")] Tarea tarea)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            Console.WriteLine($"❌ Errores en ModelState: {string.Join(", ", errors)}");
            return BadRequest($"❌ Datos inválidos: {string.Join(", ", errors)}");
        }

        _context.Tareas.Add(tarea);
        await _context.SaveChangesAsync();
        var kanban = await _context.Kanbans.FirstOrDefaultAsync(k => k.Id == tarea.KanbanId);
        if (kanban == null)
        {
            return BadRequest("❌ Error: No se encontró el tablero Kanban.");
        }

        return RedirectToAction("Index", new { proyectoId = kanban.ProyectoId });
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditarTarea(int id, [Bind("Id,Titulo,Descripcion,EstadoTareaId,PrioridadTareaId")] Tarea tareaEditada)
    {
        var tarea = await _context.Tareas
            .Include(t => t.Kanban)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (tarea == null)
        {
            return NotFound("❌ Tarea no encontrada.");
        }

        // 🔹 Asignamos los nuevos valores editados
        tarea.Titulo = tareaEditada.Titulo;
        tarea.Descripcion = tareaEditada.Descripcion;
        tarea.EstadoTareaId = tareaEditada.EstadoTareaId;
        tarea.PrioridadTareaId = tareaEditada.PrioridadTareaId;

        try
        {
            _context.Update(tarea);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { proyectoId = tarea.Kanban.ProyectoId });
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"⚠️ Error al actualizar tarea: {ex.Message}");
            ViewBag.EstadosTarea = await _context.EstadosTarea.ToListAsync();
            ViewBag.PrioridadesTarea = await _context.PrioridadesTarea.ToListAsync();
            return View(tarea);
        }
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EliminarTarea(int id)
    {
        var tarea = await _context.Tareas
            .Include(t => t.Kanban)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (tarea == null)
        {
            return NotFound("❌ Tarea no encontrada.");
        }

        try
        {
            _context.Tareas.Remove(tarea);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { proyectoId = tarea.Kanban.ProyectoId });
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"⚠️ Error al eliminar tarea: {ex.Message}");
            return RedirectToAction("Index", new { proyectoId = tarea.Kanban.ProyectoId });
        }
    }

    [HttpPost]
    public async Task<IActionResult> ActualizarEstadoTarea(int id, int estadoTareaId)
    {
        var tarea = await _context.Tareas.FindAsync(id);
        if (tarea == null)
        {
            return NotFound(new { message = "❌ Tarea no encontrada." });
        }

        tarea.EstadoTareaId = estadoTareaId;
        await _context.SaveChangesAsync();

        var kanban = await _context.Kanbans
            .Include(k => k.Tareas)
            .ThenInclude(t => t.EstadoTarea)
            .Include(k => k.Tareas)
            .ThenInclude(t => t.PrioridadTarea)
            .FirstOrDefaultAsync(k => k.Id == tarea.KanbanId);

        if (kanban == null)
        {
            return NotFound(new { message = "❌ No se encontró el tablero Kanban." });
        }

        ViewBag.EstadosTarea = await _context.EstadosTarea.ToListAsync();
        ViewBag.PrioridadesTarea = await _context.PrioridadesTarea.ToListAsync();

        return PartialView("_ListaTareas", kanban);
    }


}
