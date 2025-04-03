namespace Q_Manage.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Q_Manage.Models;

public class Kanban
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Nombre { get; set; } = string.Empty;

    [Required]
    public int ProyectoId { get; set; }

    [ForeignKey("ProyectoId")]
    public Proyecto Proyecto { get; set; }

    public ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();
}
