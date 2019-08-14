using Microsoft.EntityFrameworkCore.Migrations;

namespace Agendamento.API.Migrations
{
    public partial class agendaTitulo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Titulo",
                table: "Agenda",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Titulo",
                table: "Agenda");
        }
    }
}
