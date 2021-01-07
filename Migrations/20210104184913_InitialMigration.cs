using Microsoft.EntityFrameworkCore.Migrations;

namespace RegistroServizi.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Associazione",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Denominazione = table.Column<string>(nullable: true),
                    Sigla = table.Column<string>(nullable: true),
                    Indirizzo = table.Column<string>(nullable: true),
                    Cap = table.Column<string>(nullable: true),
                    Comune = table.Column<string>(nullable: true),
                    Provincia = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Associazione", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Associazione");
        }
    }
}
