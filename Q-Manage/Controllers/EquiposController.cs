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
    public class EquiposController : Controller
    {
        private readonly QmanageDbContext _context;

        public EquiposController(QmanageDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Equipos.ToListAsync());
        }

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



        public IActionResult Create()
        {
            ViewBag.Usuarios = _context.Users.ToList();
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

            ViewBag.Usuarios = _context.Users.ToList();
            return View(equipo);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var equipo = await _context.Equipos
               .Include(e => e.empleadorPorEquipos)
               .FirstOrDefaultAsync(e => e.Id == id);

            if (equipo == null)
            {
                return NotFound();
            }

            ViewBag.Usuarios = _context.Users.ToList();
            return View(equipo);
        }

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

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var equipo = await _context.Equipos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipo == null)
            {
                return NotFound();
            }

            return View(equipo);
        }

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
