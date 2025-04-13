using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Q_Manage.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public class PagosController : Controller
{
    private readonly QmanageDbContext _context;

    public PagosController(QmanageDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(int proyectoId)
    {
        var proyecto = await _context.Proyectos.FirstOrDefaultAsync(p => p.Id == proyectoId);

        if (proyecto == null)
        {
            return NotFound("El proyecto no existe o ha sido eliminado.");
        }

        ViewBag.Proyecto = proyecto;

        var pagos = await _context.Pagos
            .Where(p => p.ProyectoId == proyectoId)
            .Include(p => p.EstadoPago)
            .ToListAsync();

        return View(pagos);
    }

    public async Task<IActionResult> Details(int id, int proyectoId)
    {
        Console.WriteLine($"📢 Cargando detalles del pago ID: {id} para el proyecto ID: {proyectoId}");

        var pago = await _context.Pagos
            .Include(p => p.EstadoPago)
            .Include(p => p.Proyecto)
            .FirstOrDefaultAsync(p => p.Id == id && p.ProyectoId == proyectoId);

        if (pago == null)
        {
            Console.WriteLine("❌ ERROR: El pago no existe o no pertenece al proyecto.");
            return NotFound("El pago no existe o no pertenece al proyecto.");
        }

        ViewBag.Proyecto = pago.Proyecto;
        return View(pago);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> Create(int proyectoId)
    {
        if (proyectoId == 0)
        {
            return BadRequest("Error: No se ha especificado un proyecto válido.");
        }

        var proyecto = await _context.Proyectos.FirstOrDefaultAsync(p => p.Id == proyectoId);
        if (proyecto == null)
        {
            return NotFound("Error: El proyecto no existe.");
        }

        ViewBag.Proyecto = proyecto;
        return View(new Pago
        {
            ProyectoId = proyectoId,
            FechaLimite = DateTime.Today.AddDays(7)
        });
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ProyectoId,Monto,FechaLimite")] Pago pago)
    {
        if (pago.ProyectoId == 0)
        {
            ModelState.AddModelError("", "Error: No se ha seleccionado un proyecto.");
        }

        if (pago.EstadoPagoId == 0)
        {
            pago.EstadoPagoId = 1;
        }

        if (!ModelState.IsValid)
        {
            var proyecto = await _context.Proyectos.FirstOrDefaultAsync(p => p.Id == pago.ProyectoId);
            if (proyecto == null)
            {
                return NotFound("Error: El proyecto no existe o ha sido eliminado.");
            }

            ViewBag.Proyecto = proyecto;
            return View(pago);
        }

        _context.Add(pago);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index", new { proyectoId = pago.ProyectoId });
    }
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Edit(int id, int proyectoId)
    {
        var pago = await _context.Pagos.FirstOrDefaultAsync(p => p.Id == id && p.ProyectoId == proyectoId);

        if (pago == null)
        {
            return NotFound("El pago no existe o no pertenece al proyecto.");
        }

        ViewBag.Proyecto = await _context.Proyectos.FirstOrDefaultAsync(p => p.Id == proyectoId);
        ViewBag.EstadosPago = await _context.EstadoPagos.ToListAsync();

        return View(pago);
    }
    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Monto,FechaLimite,EstadoPagoId,ProyectoId")] Pago pago)
    {
        if (id != pago.Id)
        {
            return NotFound();
        }

        if (pago.EstadoPagoId == 0)
        {
            ModelState.AddModelError("EstadoPagoId", "Debe seleccionar un estado de pago válido.");
        }

        if (!ModelState.IsValid)
        {
            ViewBag.EstadosPago = await _context.EstadoPagos.ToListAsync();
            ViewBag.Proyecto = await _context.Proyectos.FirstOrDefaultAsync(p => p.Id == pago.ProyectoId);
            return View(pago);
        }

        try
        {
            _context.Update(pago);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { proyectoId = pago.ProyectoId });
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Pagos.Any(e => e.Id == pago.Id))
            {
                return NotFound();
            }
            throw;
        }
    }
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id, int proyectoId)
    {
        var pago = await _context.Pagos
            .Include(p => p.EstadoPago)
            .FirstOrDefaultAsync(p => p.Id == id && p.ProyectoId == proyectoId);

        if (pago == null)
        {
            return NotFound("El pago no existe o no pertenece al proyecto.");
        }

        ViewBag.Proyecto = await _context.Proyectos.FirstOrDefaultAsync(p => p.Id == proyectoId);
        return View(pago);
    }
    [Authorize(Roles = "Admin")]
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id, int proyectoId)
    {
        var pago = await _context.Pagos.FirstOrDefaultAsync(p => p.Id == id && p.ProyectoId == proyectoId);

        if (pago == null)
        {
            return NotFound("El pago no existe o ya fue eliminado.");
        }

        _context.Pagos.Remove(pago);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index", new { proyectoId = proyectoId });
    }

    [HttpGet]
    public async Task<IActionResult> SubirComprobante(int id, int proyectoId)
    {
        try
        {
            var pago = await _context.Pagos
                .Include(p => p.EstadoPago)
                .Include(p => p.Proyecto)
                .FirstOrDefaultAsync(p => p.Id == id && p.ProyectoId == proyectoId);

            if (pago == null)
            {
                return NotFound("❌ El pago no existe o no pertenece al proyecto.");
            }

            if (pago.Proyecto == null)
            {
                return NotFound("❌ El proyecto asociado al pago no existe.");
            }

            ViewBag.Proyecto = pago.Proyecto;
            return View(pago);
        }
        catch (Exception ex)
        {
            return BadRequest($"⚠️ Error inesperado en la carga del comprobante: {ex.Message}");
        }
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SubirComprobante(int id, int proyectoId, IFormFile file)
    {
        try
        {
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("", "Debe seleccionar un archivo válido.");
                return View();
            }

            if (id <= 0 || proyectoId <= 0)
            {
                throw new Exception($"❌ ID inválido. PagoID: {id}, ProyectoID: {proyectoId}");
            }

            Console.WriteLine($"🔍 Buscando pago con ID {id} en Proyecto {proyectoId}");

            var pago = await _context.Pagos
                .Include(p => p.EstadoPago)
                .Include(p => p.Proyecto)
                .FirstOrDefaultAsync(p => p.Id == id && p.ProyectoId == proyectoId);

            if (pago == null)
            {
                var pagoSinProyecto = await _context.Pagos.FirstOrDefaultAsync(p => p.Id == id);
                if (pagoSinProyecto != null)
                {
                    throw new Exception($"❌ Pago {id} encontrado, pero no pertenece al proyecto {proyectoId}. Proyecto real: {pagoSinProyecto.ProyectoId}");
                }

                throw new Exception("❌ El pago no existe en la base de datos.");
            }

            Console.WriteLine($"✅ Subiendo archivo para el pago ID {id} en proyecto {proyectoId}");

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "comprobantes");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            pago.Comprobante = $"/comprobantes/{fileName}";
            pago.FechaPago = DateTime.Now;

            if (pago.EstadoPagoId != 3)
            {
                pago.ActualizarEstado();
            }

            _context.Update(pago);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { proyectoId = pago.ProyectoId });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ ERROR AL SUBIR COMPROBANTE: {ex.Message}");
            return BadRequest($"❌ Error inesperado: {ex.Message}");
        }
    }

}
