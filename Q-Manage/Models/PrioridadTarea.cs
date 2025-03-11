using System.ComponentModel.DataAnnotations;
namespace Q_Manage.Models;
public class PrioridadTarea
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Nombre { get; set; } = string.Empty;
}
