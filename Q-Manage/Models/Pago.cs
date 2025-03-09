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
        public DateTime? FechaPago { get; set; }

        [Required]
        public int ProyectoId { get; set; }

        [ForeignKey("ProyectoId")]
        public Proyecto Proyecto { get; set; }

        public string? Comprobante { get; set; }

        [Required]
        public int EstadoPagoId { get; set; }

        [ForeignKey("EstadoPagoId")]
        public EstadoPago EstadoPago { get; set; }

        public void ActualizarEstado()
        {
            if (EstadoPagoId == 3) return;

            if (!string.IsNullOrEmpty(Comprobante) && FechaPago != null)
            {
                EstadoPagoId = 3;
            }
            else if (DateTime.Now > FechaLimite)
            {
                EstadoPagoId = 2;
            }
            else
            {
                EstadoPagoId = 1;
            }
        }
    }
}
