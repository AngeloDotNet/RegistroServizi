using Microsoft.EntityFrameworkCore.Migrations;

namespace RegistroServizi.Migrations
{
    public partial class TabellaOspedale : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ospedale",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Clinica = table.Column<string>(type: "TEXT", nullable: true),
                    Comune = table.Column<string>(type: "TEXT", nullable: true),
                    Latitudine = table.Column<string>(type: "TEXT", nullable: true),
                    Longitudine = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ospedale", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SocioRinnovo_SocioId",
                table: "SocioRinnovo",
                column: "SocioId");

            migrationBuilder.CreateIndex(
                name: "IX_SocioFamiliare_SocioId",
                table: "SocioFamiliare",
                column: "SocioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ospedale");

            migrationBuilder.DropIndex(
                name: "IX_SocioRinnovo_SocioId",
                table: "SocioRinnovo");

            migrationBuilder.DropIndex(
                name: "IX_SocioFamiliare_SocioId",
                table: "SocioFamiliare");
        }
    }
}
