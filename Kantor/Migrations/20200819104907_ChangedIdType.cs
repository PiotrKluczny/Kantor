using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kantor.Migrations
{
    public partial class ChangedIdType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "NbpCurrencyDictionares",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "NbpCurrencyDictionares",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid))
                .Annotation("Sqlite:Autoincrement", true);
        }
    }
}
