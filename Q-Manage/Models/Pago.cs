using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Q_Manage.Models
{
    public class Pago
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public decimal Monto { get; set; }

        [Required]
        public DateTime FechaLimite { get; set; }

        [Required]
        public DateTime FechaPago { get; set; }

        public int EstadoPagoId { get; set; }

        [ForeignKey("EstadoPagoId")]
        public EstadoPago EstadoPago { get; set; }
    }
}
