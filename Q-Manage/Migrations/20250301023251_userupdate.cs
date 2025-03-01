using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Q_Manage.Migrations
{
    /// <inheritdoc />
    public partial class userupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "060b0758-c42f-480e-9840-440e3d3c97ca");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "e4b901d1-8086-4688-97ae-6b9737378200", "1a94fcb8-49c5-484e-b1dc-eb4a6e7fb642" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e4b901d1-8086-4688-97ae-6b9737378200");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1a94fcb8-49c5-484e-b1dc-eb4a6e7fb642");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "060b0758-c42f-480e-9840-440e3d3c97ca", null, "Usuario", "USUARIO" },
                    { "e4b901d1-8086-4688-97ae-6b9737378200", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1a94fcb8-49c5-484e-b1dc-eb4a6e7fb642", 0, "3eb85098-f72f-439a-abaf-e4770a846999", "admin@gmail.com", true, false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEB4m/o3kVSR7tDeLGBFMYYmeNbZV8BSlU8/A/VzR6tDdnhR2BQ8vJhFy5oBQDra+GA==", null, false, "250a60e8-bfc9-4be4-863a-939bb3190d90", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "e4b901d1-8086-4688-97ae-6b9737378200", "1a94fcb8-49c5-484e-b1dc-eb4a6e7fb642" });
        }
    }
}
