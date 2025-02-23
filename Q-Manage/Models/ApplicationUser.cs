using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Q_Manage.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<Comentario> Comentarios { get; set; } = new List<Comentario>();

        public List<Proyecto> Proyectos { get; set; } = new List<Proyecto>();
    }
}