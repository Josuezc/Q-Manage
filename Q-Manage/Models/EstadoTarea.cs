using System.ComponentModel.DataAnnotations;

public class EstadoTarea
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Nombre { get; set; } = string.Empty;
}
