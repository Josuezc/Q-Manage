using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Q_Manage.Models
{
    public class ProyectosPorEquipo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int EquipoId { get; set; }
        [Required]
        public int ProyectoId { get; set; }

        [ForeignKey("EquipoId")]
        public Equipo Equipo { get; set; }
        [ForeignKey("ProyectoId")]
        public Proyecto Proyecto { get; set; }
    }
}
