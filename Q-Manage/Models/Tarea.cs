namespace Q_Manage.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Tarea
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Titulo { get; set; } = string.Empty;

    public string? Descripcion { get; set; }

    [Required]
    public int KanbanId { get; set; }

    [ForeignKey("KanbanId")]
    public Kanban? Kanban { get; set; }

    [Required]
    public int EstadoTareaId { get; set; }

    [ForeignKey("EstadoTareaId")]
    public EstadoTarea? EstadoTarea { get; set; }

    [Required]
    public int PrioridadTareaId { get; set; }

    [ForeignKey("PrioridadTareaId")]
    public PrioridadTarea? PrioridadTarea { get; set; }

    public DateTime FechaCreacion { get; set; } = DateTime.Now;
    public DateTime? FechaLimite { get; set; }
}
