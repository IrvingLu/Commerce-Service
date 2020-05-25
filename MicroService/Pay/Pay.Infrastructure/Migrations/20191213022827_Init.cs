using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pay.Infrastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PayRecord",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PayNum = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayRecord", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PayRecord");
        }
    }
}
