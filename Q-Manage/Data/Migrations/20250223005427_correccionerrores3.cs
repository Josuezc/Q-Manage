using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Q_Manage.Data.Migrations
{
    /// <inheritdoc />
    public partial class correccionerrores3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0b75d348-a8f1-4a02-acd6-2bce873f0277");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3fc4cd5f-874a-4ccc-b570-9e134e0a2d14", "91c61ed2-a818-4f62-996b-df8f7125b0e8" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3fc4cd5f-874a-4ccc-b570-9e134e0a2d14");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "91c61ed2-a818-4f62-996b-df8f7125b0e8");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "12c0b3f0-3e3b-4b47-be6a-cd1cb1dc0599", null, "Usuario", "USUARIO" },
                    { "22a8c429-42fb-4ca1-b66c-c99dddd8cc4c", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "058730bc-9f00-4175-b60d-055d67befebb", 0, "d6dccd4f-afa8-4729-a314-7f9a1ea96227", "admin@gmail.com", true, false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEOmVdsuvPFZrcBfynm4gnpe8SLg5uzJ0BMUHvv/MSMiNkVcLXvDWEZYeAV+fpgdPdA==", null, false, "25b41224-e9ec-47d2-b716-2212fa8fed3a", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "22a8c429-42fb-4ca1-b66c-c99dddd8cc4c", "058730bc-9f00-4175-b60d-055d67befebb" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "12c0b3f0-3e3b-4b47-be6a-cd1cb1dc0599");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "22a8c429-42fb-4ca1-b66c-c99dddd8cc4c", "058730bc-9f00-4175-b60d-055d67befebb" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "22a8c429-42fb-4ca1-b66c-c99dddd8cc4c");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "058730bc-9f00-4175-b60d-055d67befebb");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0b75d348-a8f1-4a02-acd6-2bce873f0277", null, "Usuario", "USUARIO" },
                    { "3fc4cd5f-874a-4ccc-b570-9e134e0a2d14", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "91c61ed2-a818-4f62-996b-df8f7125b0e8", 0, "d486aa7e-180a-4708-80f5-8b32ea6c6c7f", "ApplicationUser", "admin@gmail.com", true, false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEIG2fTcEuUHe8zn4as/NSFfj4E8zbWgujD9PIC92oIVEEwO0/9BPPCwHwtSL/qCssQ==", null, false, "8eff659b-d2bc-41d0-ba8f-8d7d22730df1", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "3fc4cd5f-874a-4ccc-b570-9e134e0a2d14", "91c61ed2-a818-4f62-996b-df8f7125b0e8" });
        }
    }
}
