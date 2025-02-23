using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Q_Manage.Data.Migrations
{
    /// <inheritdoc />
    public partial class correccionerrores2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "70df9acf-1614-499c-9aff-1fb03934e01b");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "54bfc70f-650b-4ec5-a4ed-4d8912414694", "62410475-8743-4164-a1db-306035360b73" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "54bfc70f-650b-4ec5-a4ed-4d8912414694");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "62410475-8743-4164-a1db-306035360b73");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "54bfc70f-650b-4ec5-a4ed-4d8912414694", null, "Admin", "ADMIN" },
                    { "70df9acf-1614-499c-9aff-1fb03934e01b", null, "Usuario", "USUARIO" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "62410475-8743-4164-a1db-306035360b73", 0, "9fe861dc-3f19-487a-bde6-a713b676ebf6", "ApplicationUser", "admin@gmail.com", true, false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEDyh6UzI4LoKMNA0BmS4jTjLgTL00OL1GJ7yfgRG8gmGkJPis9IfrQdaN7zHP5SpNw==", null, false, "1fc7692b-c59f-470c-b5bf-388710d4f9d6", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "54bfc70f-650b-4ec5-a4ed-4d8912414694", "62410475-8743-4164-a1db-306035360b73" });
        }
    }
}
