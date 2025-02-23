using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Q_Manage.Data.Migrations
{
    /// <inheritdoc />
    public partial class usuarioproy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Proyectos_IdProyecto",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Proyectos_ProyectosID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_IdProyecto",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ProyectosID",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2b649d22-74c2-4987-ab39-300e53e1b2d7");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "bbaebbf9-b8fa-4744-a617-4c8e7da0be18", "d89e266c-8e7e-4455-9ef1-cab406e9082a" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bbaebbf9-b8fa-4744-a617-4c8e7da0be18");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d89e266c-8e7e-4455-9ef1-cab406e9082a");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IdProyecto",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProyectosID",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "UsuarioProyecto",
                columns: table => new
                {
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProyectoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioProyecto", x => new { x.UsuarioId, x.ProyectoId });
                    table.ForeignKey(
                        name: "FK_UsuarioProyecto_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioProyecto_Proyectos_ProyectoId",
                        column: x => x.ProyectoId,
                        principalTable: "Proyectos",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3ded7c7e-e29c-4a9f-baaa-7977d53efb1f", null, "Admin", "ADMIN" },
                    { "89a2c31b-31fb-401d-b7c0-31256aebcb13", null, "Usuario", "USUARIO" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "0b00167e-4a7b-4d9e-881e-e7e4313d4371", 0, "1e7c11ca-431f-4782-81f8-7f07b90bbb59", "admin@gmail.com", true, false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEIozJCovNU1FQGna2Q2TDS6pz7+l9mQpiahh7GOvTNctM//RC5Ury/4fhBj3+E1FiQ==", null, false, "c5c92033-69e3-415c-aa98-6dc2570f85ef", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "3ded7c7e-e29c-4a9f-baaa-7977d53efb1f", "0b00167e-4a7b-4d9e-881e-e7e4313d4371" });

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioProyecto_ProyectoId",
                table: "UsuarioProyecto",
                column: "ProyectoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuarioProyecto");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "89a2c31b-31fb-401d-b7c0-31256aebcb13");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3ded7c7e-e29c-4a9f-baaa-7977d53efb1f", "0b00167e-4a7b-4d9e-881e-e7e4313d4371" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3ded7c7e-e29c-4a9f-baaa-7977d53efb1f");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "0b00167e-4a7b-4d9e-881e-e7e4313d4371");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "IdProyecto",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProyectosID",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2b649d22-74c2-4987-ab39-300e53e1b2d7", null, "Usuario", "USUARIO" },
                    { "bbaebbf9-b8fa-4744-a617-4c8e7da0be18", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProyectosID", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "d89e266c-8e7e-4455-9ef1-cab406e9082a", 0, "37f3c08c-49a8-4f28-b080-e96a5eb7b0e1", "IdentityUser", "admin@gmail.com", true, false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEIAT0XiPq+G5h+9fsg0D0eCS7KznuJbmk3cWsZMvqXxHRJTVD07l5Lg43M0mw+CJxw==", null, false, null, "d420bc05-44c6-4839-87bc-6bda4f29c614", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "bbaebbf9-b8fa-4744-a617-4c8e7da0be18", "d89e266c-8e7e-4455-9ef1-cab406e9082a" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_IdProyecto",
                table: "AspNetUsers",
                column: "IdProyecto");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProyectosID",
                table: "AspNetUsers",
                column: "ProyectosID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Proyectos_IdProyecto",
                table: "AspNetUsers",
                column: "IdProyecto",
                principalTable: "Proyectos",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Proyectos_ProyectosID",
                table: "AspNetUsers",
                column: "ProyectosID",
                principalTable: "Proyectos",
                principalColumn: "ID");
        }
    }
}
