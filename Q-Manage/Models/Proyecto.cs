using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
}
