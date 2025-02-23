using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Q_Manage.Models
{
    public class EmpleadoPorEquipo
    {
       [Key]
       public int Id { get; set; }

       [Required]
       public string UsuarioId { get; set; }

       [Required]
       public int EquipoId { get; set; }

       [ForeignKey("UsuarioId")]
       public ApplicationUser Usuario { get; set; }

       [ForeignKey("EquipoId")]
       public Equipo Equipo { get; set; }

       public List<EmpleadoPorEquipo> EmpleadoPorEquipos { get; set; } = new List<EmpleadoPorEquipo>();
    }
}
