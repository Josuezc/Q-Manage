using System.ComponentModel.DataAnnotations;
namespace Q_Manage.Models
{
    public class EstadoPago
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        public List<Pago> Pagos { get; set; } = new List<Pago>();

        public List<Proyecto> Proyectos { get; set; } = new List<Proyecto>();
    }
}
