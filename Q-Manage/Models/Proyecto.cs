using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using Q_Manage.Models;

namespace Q_Manage.Models
{
    public class Proyecto
    {

        [NotMapped]
        public int Progreso
        {
            get
            {
                if (FechaFinalizacion == null || FechaInicio >= FechaFinalizacion)
                    return 0;

                var totalDias = (FechaFinalizacion - FechaInicio)?.TotalDays ?? 0;
                var diasTranscurridos = (DateTime.Now - FechaInicio).TotalDays;

                if (diasTranscurridos <= 0)
                    return 0;

                if (diasTranscurridos >= totalDias)
                    return 100;

                var progreso = (int)((diasTranscurridos / totalDias) * 100);

                return Math.Clamp(progreso, 0, 100);
            }
        }


        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public string Descripcion { get; set; } = string.Empty;

        [Required]
        public DateTime FechaInicio { get; set; }

        public DateTime? FechaFinalizacion { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un cliente para el proyecto.")]
        public string UsuarioId { get; set; } = string.Empty;

        [ForeignKey("UsuarioId")]
        public ApplicationUser? Usuario { get; set; }

        [Required]
        public int EstadoPagoId { get; set; }

        [ForeignKey("EstadoPagoId")]
        public EstadoPago? EstadoPago { get; set; }

        [Required]
        public int EstadoProyectoId { get; set; }

        [ForeignKey("EstadoProyectoId")]
        public EstadoProyecto? EstadoProyecto { get; set; }

        public List<Comentario>? Comentarios { get; set; } = new List<Comentario>();

        [NotMapped]
        public string EstadoPagoProyecto
        {
            get
            {
                if (Pagos == null || !Pagos.Any()) return "Sin pagos asignados";

                if (Pagos.All(p => p.EstadoPagoId == 3))
                    return "Pagado";

                if (Pagos.Any(p => p.EstadoPagoId == 2))
                    return "Atrasado";

                return "Pendiente";
            }
        }

        public List<Pago> Pagos { get; set; } = new List<Pago>();
    }


public static class ProyectoEndpoints
{
	public static void MapProyectoEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Proyecto").WithTags(nameof(Proyecto));

        group.MapGet("/", async (QmanageDbContext db) =>
        {
            return await db.Proyectos.ToListAsync();
        })
        .WithName("GetAllProyectos")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Proyecto>, NotFound>> (int id, QmanageDbContext db) =>
        {
            return await db.Proyectos.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Proyecto model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetProyectoById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, Proyecto proyecto, QmanageDbContext db) =>
        {
            var affected = await db.Proyectos
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Id, proyecto.Id)
                  .SetProperty(m => m.Nombre, proyecto.Nombre)
                  .SetProperty(m => m.Descripcion, proyecto.Descripcion)
                  .SetProperty(m => m.FechaInicio, proyecto.FechaInicio)
                  .SetProperty(m => m.FechaFinalizacion, proyecto.FechaFinalizacion)
                  .SetProperty(m => m.UsuarioId, proyecto.UsuarioId)
                  .SetProperty(m => m.EstadoPagoId, proyecto.EstadoPagoId)
                  .SetProperty(m => m.EstadoProyectoId, proyecto.EstadoProyectoId)
                  );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateProyecto")
        .WithOpenApi();

        group.MapPost("/", async (Proyecto proyecto, QmanageDbContext db) =>
        {
            db.Proyectos.Add(proyecto);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Proyecto/{proyecto.Id}",proyecto);
        })
        .WithName("CreateProyecto")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, QmanageDbContext db) =>
        {
            var affected = await db.Proyectos
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteProyecto")
        .WithOpenApi();
    }
}}
