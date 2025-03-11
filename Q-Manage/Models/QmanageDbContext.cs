using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
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
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<EstadoTarea> EstadosTarea { get; set; }
        public DbSet<PrioridadTarea> PrioridadesTarea { get; set; }
        public DbSet<Kanban> Kanbans { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Pago>()
                   .Property(p => p.Monto)
                   .HasPrecision(18, 2);

            builder.Entity<Pago>()
                   .HasOne(p => p.Proyecto)
                   .WithMany(proj => proj.Pagos)
                   .HasForeignKey(p => p.ProyectoId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Comentario>()
                   .HasOne(c => c.Proyecto)
                   .WithMany(p => p.Comentarios)
                   .OnDelete(DeleteBehavior.Restrict);

            // 🔹 Definir roles por defecto
            string adminRoleId = Guid.NewGuid().ToString();
            string userRoleId = Guid.NewGuid().ToString();
            string clientRoleId = Guid.NewGuid().ToString();
            string clienteUserId = Guid.NewGuid().ToString();
            string empleadoUserId = Guid.NewGuid().ToString();
            string adminUserId = Guid.NewGuid().ToString();

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = adminRoleId, Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = userRoleId, Name = "User", NormalizedName = "User" },
                new IdentityRole { Id = clientRoleId, Name = "Client", NormalizedName = "CLIENT" }
            );

           builder.Entity<EstadoTarea>().HasData(
                new EstadoTarea { Id = 1, Nombre = "Pendiente" },
                new EstadoTarea { Id = 2, Nombre = "En progreso" },
                new EstadoTarea { Id = 3, Nombre = "Completado" }
            );

            builder.Entity<PrioridadTarea>().HasData(
                new PrioridadTarea { Id = 1, Nombre = "Baja" },
                new PrioridadTarea { Id = 2, Nombre = "Media" },
                new PrioridadTarea { Id = 3, Nombre = "Alta" },
                new PrioridadTarea { Id = 4, Nombre = "Urgente" }
            );

            builder.Entity<EstadoPago>().HasData(
                new EstadoPago { Id = 1, Nombre = "Pendiente" },
                new EstadoPago { Id = 2, Nombre = "Pagado" },
                new EstadoPago { Id = 3, Nombre = "Atrasado" }
            );

            builder.Entity<EstadoProyecto>().HasData(
                new EstadoProyecto { Id = 1, Nombre = "Planificación" },
                new EstadoProyecto { Id = 2, Nombre = "Desarollo" },
                new EstadoProyecto { Id = 3, Nombre = "QA" },
                new EstadoProyecto { Id = 4, Nombre = "Producción" }
            );

            var adminUser = new ApplicationUser
            {
                Id = adminUserId,
                UserName = "admin@gmail.com",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var passwordHasher = new PasswordHasher<ApplicationUser>();
            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "Admin123!");

            builder.Entity<ApplicationUser>().HasData(adminUser);

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = adminUserId, RoleId = adminRoleId }
            );


            var clienteUser = new ApplicationUser
            {
                Id = clienteUserId,
                UserName = "cliente@gmail.com",
                NormalizedUserName = "CLIENTE@GMAIL.COM",
                Email = "cliente@gmail.com",
                NormalizedEmail = "CLIENTE@GMAIL.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            clienteUser.PasswordHash = passwordHasher.HashPassword(clienteUser, "Password!2");
            builder.Entity<ApplicationUser>().HasData(clienteUser);
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = clienteUserId, RoleId = clientRoleId }
            );

            var empleadoUser = new ApplicationUser
            {
                Id = empleadoUserId,
                UserName = "empleado@gmail.com",
                NormalizedUserName = "EMPLEADO@GMAIL.COM",
                Email = "empleado@gmail.com",
                NormalizedEmail = "EMPLEADO@GMAIL.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            empleadoUser.PasswordHash = passwordHasher.HashPassword(empleadoUser, "Password!2");
            builder.Entity<ApplicationUser>().HasData(empleadoUser);
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> { UserId = empleadoUserId, RoleId = userRoleId }
            );
        }
    }
}
