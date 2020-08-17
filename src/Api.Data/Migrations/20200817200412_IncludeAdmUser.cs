using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class IncludeAdmUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("ef256aac-2b6e-48e5-a598-814a1e468b65"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedAt", "Email", "ModifiedAt", "Name" },
                values: new object[] { new Guid("ef256aac-2b6e-48e5-a598-814a1e468b65"), null, "wakanda@forever.com", null, "Administrador" });
        }
    }
}
