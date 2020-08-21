using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BogusApp.Migrations
{
    public partial class Internal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    Age = table.Column<int>(nullable: false),
                    EyeColor = table.Column<int>(nullable: false),
                    HairColor = table.Column<string>(nullable: true),
                    PersonId = table.Column<int>(nullable: false),
                    Country = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    ZipCode = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    Company = table.Column<string>(nullable: true),
                    Position = table.Column<string>(nullable: true),
                    Salary = table.Column<decimal>(nullable: false),
                    CompanyEmail = table.Column<string>(nullable: true),
                    ComapnyPhone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
