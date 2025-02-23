using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Q_Manage.Models
{
    public class Proyecto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public string Descripcion { get; set; } = string.Empty;

        [Required]
        public DateTime FechaInicio { get; set; }

        public DateTime? FechaFinalizacion { get; set; }

        [Required]
        public string UsuarioId { get; set; } = string.Empty;

        [ForeignKey("UsuarioId")]
        public ApplicationUser Usuario { get; set; }

        [Required]
        public int EstadoPagoId { get; set; }

        [ForeignKey("EstadoPagoId")]
        public EstadoPago EstadoPago { get; set; }

        [Required]
        public int EstadoProyectoId { get; set; }

        [ForeignKey("EstadoProyectoId")]
        public EstadoProyecto EstadoProyecto { get; set; }

        public List<Comentario> Comentarios { get; set; } = new List<Comentario>();
    }
}
