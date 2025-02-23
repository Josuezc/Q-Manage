using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;

namespace Q_Manage.Models
{
    public class QmanageDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public QmanageDbContext(DbContextOptions<QmanageDbContext> options)
            : base(options)
        {
        }

        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<EstadoProyecto> EstadoProyectos { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<EstadoPago> EstadoPagos {  get; set; }
        public DbSet<Equipo> Equipos { get; set; } 
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<EmpleadoPorEquipo> EmpleadoPorEquipos { get; set; }
        public DbSet<ProyectosPorEquipo> ProyectosPorEquipos { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Pago>()
                   .Property(p => p.Monto)
                   .HasPrecision(18, 2);

            builder.Entity<Comentario>()
                   .HasOne(c => c.Proyecto)
                   .WithMany(p => p.Comentarios)
                   .OnDelete(DeleteBehavior.Restrict);

            // 🔹 Definir roles por defecto
            string adminRoleId = Guid.NewGuid().ToString();
            string userRoleId = Guid.NewGuid().ToString();
            string adminUserId = Guid.NewGuid().ToString();

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = adminRoleId, Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = userRoleId, Name = "Usuario", NormalizedName = "USUARIO" }
            );

            // 🔹 Crear usuario administrador por defecto
            var adminUser = new ApplicationUser
            {
                Id = adminUserId,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var passwordHasher = new PasswordHasher<ApplicationUser>();
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "Admin123!");

            builder.Entity<ApplicationUser>().HasData(adminUser);

            // 🔹 Asignar el usuario al rol de administrador
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = adminUserId, RoleId = adminRoleId }
            );

        }
    }
}
