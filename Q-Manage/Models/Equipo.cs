using System.ComponentModel.DataAnnotations;

namespace Q_Manage.Models
{
    public class Equipo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        public List<EmpleadoPorEquipo> empleadorPorEquipos { get; set; } = new List<EmpleadoPorEquipo>();
    }
}
