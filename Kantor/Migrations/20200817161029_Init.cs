using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kantor.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NbpCurrencyDictionares",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Currency = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NbpCurrencyDictionares", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NbpCurrencys",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Currency = table.Column<string>(nullable: true),
                    FromDate = table.Column<DateTime>(nullable: false),
                    ToDate = table.Column<DateTime>(nullable: false),
                    Average = table.Column<double>(nullable: false),
                    Deviation = table.Column<double>(nullable: false),
                    TimeStape = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NbpCurrencys", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NbpCurrencyDictionares");

            migrationBuilder.DropTable(
                name: "NbpCurrencys");
        }
    }
}
