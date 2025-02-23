﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Q_Manage.Models
{
    public class ApplicationUser : IdentityUser
    {
        public List<UsuarioProyecto> UsuarioProyectos { get; set; }
    }
}