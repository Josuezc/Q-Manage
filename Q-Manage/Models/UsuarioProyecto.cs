using Microsoft.AspNetCore.Identity;

namespace Q_Manage.Models
{
    public class UsuarioProyecto
    {
        public string UsuarioId { get; set; }
        public IdentityUser Usuario { get; set; }

        public int ProyectoId { get; set; }
        public Proyectos Proyecto { get; set; }
    }
}
