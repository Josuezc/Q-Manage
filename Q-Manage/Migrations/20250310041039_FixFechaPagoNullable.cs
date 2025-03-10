using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Q_Manage.Migrations
{
    /// <inheritdoc />
    public partial class FixFechaPagoNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3d0aa2c0-e4d8-44ac-9a72-48866c039f48");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "daad7b8d-d18c-4a0c-9d13-03396aec8047");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "ef18bffc-4341-4e57-8e05-a65843ad415e", "bf261b50-f864-44ee-ad0b-5e005203d192" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ef18bffc-4341-4e57-8e05-a65843ad415e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bf261b50-f864-44ee-ad0b-5e005203d192");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaPago",
                table: "Pagos",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "dd155200-db85-4b0c-8d29-da689171d87e", null, "Admin", "ADMIN" },
                    { "e605c7f3-5a09-45de-8bcf-79b965c2a92b", null, "User", "User" },
                    { "f033df82-36f7-4670-b8eb-af08cd557467", null, "Client", "CLIENT" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "96e35982-4148-4fe8-bcda-ff7fa9d37970", 0, "5f2d10c1-3b7c-49e4-bad7-29ee14b4c941", "admin@gmail.com", true, false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEFyIwS4pKgjW1D9ST+gbSjLHFedH0Vb/7sBRuPkl/lVaZhTt7YCECehBroptTDtqoQ==", null, false, "8664db93-4c17-43e7-bbbd-d3b8c63699b6", false, "admin@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "dd155200-db85-4b0c-8d29-da689171d87e", "96e35982-4148-4fe8-bcda-ff7fa9d37970" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e605c7f3-5a09-45de-8bcf-79b965c2a92b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f033df82-36f7-4670-b8eb-af08cd557467");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "dd155200-db85-4b0c-8d29-da689171d87e", "96e35982-4148-4fe8-bcda-ff7fa9d37970" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dd155200-db85-4b0c-8d29-da689171d87e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "96e35982-4148-4fe8-bcda-ff7fa9d37970");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaPago",
                table: "Pagos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3d0aa2c0-e4d8-44ac-9a72-48866c039f48", null, "Client", "CLIENT" },
                    { "daad7b8d-d18c-4a0c-9d13-03396aec8047", null, "User", "User" },
                    { "ef18bffc-4341-4e57-8e05-a65843ad415e", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "bf261b50-f864-44ee-ad0b-5e005203d192", 0, "1a611129-f6a1-4b4d-8b84-0470a0a21078", "admin@gmail.com", true, false, null, "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAEJoMWvdjcxzF0qZPUbR3a0kTsXb9pmZdyTPoT6GzTZZDu34D8/Xl4nvpk2nPluyCEg==", null, false, "053af163-30b9-4324-9667-b31cff8e1d8b", false, "admin@gmail.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "ef18bffc-4341-4e57-8e05-a65843ad415e", "bf261b50-f864-44ee-ad0b-5e005203d192" });
        }
    }
}
