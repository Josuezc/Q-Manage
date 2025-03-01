using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Q_Manage.Migrations
{
    /// <inheritdoc />
    public partial class Role : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "70ddf93b-c7fa-4944-8139-b3a9ec0377a7");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6f8d88c6-7dac-4f96-be1a-25d90c87dfe5", "c11e832c-79a8-48ed-878d-19b6cc6dfecf" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6f8d88c6-7dac-4f96-be1a-25d90c87dfe5");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c11e832c-79a8-48ed-878d-19b6cc6dfecf");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "165681a6-f708-41c0-9cd8-dab69b66434a", null, "Client", "CLIENT" },
                    { "7f64e1f5-70d3-405e-95c6-425fa3df4395", null, "User", "User" },
                    { "ce555a86-9908-4585-ba27-99726305ddac", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "250807ea-f99f-43c5-a99e-f28356132c9d", 0, "b12457e9-dd0a-4bcb-8970-07828342c08d", "admin@gmail.com", true, false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEMYrNPqxcDzxKhsJjLz4KhV40zL6rUgoKGJrZqMVlBVwFV7TG+NNWoGxNy2pGgjqvw==", null, false, "0fc1b381-fd5e-4efe-bba9-008014c269a8", false, "admin@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "ce555a86-9908-4585-ba27-99726305ddac", "250807ea-f99f-43c5-a99e-f28356132c9d" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "165681a6-f708-41c0-9cd8-dab69b66434a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7f64e1f5-70d3-405e-95c6-425fa3df4395");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "ce555a86-9908-4585-ba27-99726305ddac", "250807ea-f99f-43c5-a99e-f28356132c9d" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ce555a86-9908-4585-ba27-99726305ddac");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "250807ea-f99f-43c5-a99e-f28356132c9d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6f8d88c6-7dac-4f96-be1a-25d90c87dfe5", null, "Admin", "ADMIN" },
                    { "70ddf93b-c7fa-4944-8139-b3a9ec0377a7", null, "Usuario", "USUARIO" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "c11e832c-79a8-48ed-878d-19b6cc6dfecf", 0, "eb7f011c-a2ef-4583-86f0-f5ee8a6546bd", "admin@gmail.com", true, false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEA6/fNGofSMo0m3H2QlZ0lV9VBqQzTMHSL8BdSlGGOS3VXcj9rZWutHX68wdLQr0bg==", null, false, "e6021059-8cec-49c9-9938-3c6deb4b4b8e", false, "admin@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "6f8d88c6-7dac-4f96-be1a-25d90c87dfe5", "c11e832c-79a8-48ed-878d-19b6cc6dfecf" });
        }
    }
}
