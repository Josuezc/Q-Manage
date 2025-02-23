using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Q_Manage.Data.Migrations
{
    /// <inheritdoc />
    public partial class correccionerrores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

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
        }
    }
}
