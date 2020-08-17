using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class IncludeAdmUserWakanda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "CreatedAt", "Email", "ModifiedAt", "Name" },
                values: new object[] { new Guid("4079e0b8-d593-4a13-b252-6ec220eb2452"), new DateTime(2020, 8, 17, 17, 18, 22, 118, DateTimeKind.Local).AddTicks(9533), "wakanda@forever.com", new DateTime(2020, 8, 17, 17, 18, 22, 120, DateTimeKind.Local).AddTicks(9205), "Administrador" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("4079e0b8-d593-4a13-b252-6ec220eb2452"));
        }
    }
}
