using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Q_Manage.Models
{
    public class QmanageDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public QmanageDbContext(DbContextOptions<QmanageDbContext> options)
            : base(options)
        {
        }


        public DbSet<Proyectos> Proyectos { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // 🔹 Definir roles por defecto
            string adminRoleId = Guid.NewGuid().ToString();
            string userRoleId = Guid.NewGuid().ToString();
            string adminUserId = Guid.NewGuid().ToString();

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = adminRoleId, Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = userRoleId, Name = "Usuario", NormalizedName = "USUARIO" }
            );

            // 🔹 Crear usuario administrador por defecto
            var adminUser = new IdentityUser
            {
                Id = adminUserId,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            // 🔹 Establecer la contraseña de forma segura
            var passwordHasher = new PasswordHasher<IdentityUser>();
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "Admin123!");

            builder.Entity<IdentityUser>().HasData(adminUser);

            // 🔹 Asignar el usuario al rol de administrador
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = adminUserId, RoleId = adminRoleId }
            );

            builder.Entity<UsuarioProyecto>()
               .HasKey(up => new { up.UsuarioId, up.ProyectoId });

            builder.Entity<UsuarioProyecto>()
                .HasOne(up => up.Usuario)
                .WithMany()
                .HasForeignKey(up => up.UsuarioId);

            builder.Entity<UsuarioProyecto>()
                .HasOne(up => up.Proyecto)
                .WithMany(p => p.UsuarioProyectos)
                .HasForeignKey(up => up.ProyectoId);

        }

        
       

     

    }
}
