using Microsoft.AspNetCore.Identity;

namespace Q_Manage.Models
{
    public class Proyectos
    {
        public int ID { get; set; }

        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public List<UsuarioProyecto> UsuarioProyectos { get; set; }
    }
}
