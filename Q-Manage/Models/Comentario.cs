using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Q_Manage.Models
{
    public class Comentario
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Contenido { get; set; } = string.Empty;

        [Required]
        public DateTime FechaCreacion { get; set; }

        [Required]
        public int ProyectoId { get; set; }

        [ForeignKey("ProyectoId")]
        public Proyecto Proyecto { get; set; } = null!;

        [Required]
        public string UsuarioId { get; set; } = string.Empty;

        [ForeignKey("UsuarioId")]
        public ApplicationUser Usuario { get; set; } = null!;
    }
}
