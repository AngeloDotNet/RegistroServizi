using Microsoft.EntityFrameworkCore.Migrations;

namespace RegistroServizi.Migrations
{
    public partial class TabelleSoci : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Socio",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Tessera = table.Column<string>(nullable: true),
                    Nominativo = table.Column<string>(nullable: true),
                    Indirizzo = table.Column<string>(nullable: true),
                    Cap = table.Column<string>(nullable: true),
                    Comune = table.Column<string>(nullable: true),
                    Provincia = table.Column<string>(nullable: true),
                    LuogoNascita = table.Column<string>(nullable: true),
                    DataNascita = table.Column<string>(nullable: true),
                    CodiceFiscale = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    DataTesseramento = table.Column<string>(nullable: true),
                    TrattamentoDati = table.Column<string>(nullable: true),
                    Professione = table.Column<string>(nullable: true),
                    Zona = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Socio", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SocioFamiliare",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SocioId = table.Column<int>(nullable: false),
                    Familiare = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocioFamiliare", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SocioFamiliare_Socio_SocioId",
                        column: x => x.SocioId,
                        principalTable: "Socio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SocioRinnovo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SocioId = table.Column<int>(nullable: false),
                    Anno = table.Column<string>(nullable: true),
                    Quota = table.Column<string>(nullable: true),
                    DataRinnovo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocioRinnovo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SocioRinnovo_Socio_SocioId",
                        column: x => x.SocioId,
                        principalTable: "Socio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SocioFamiliare_SocioId",
                table: "SocioFamiliare",
                column: "SocioId");

            migrationBuilder.CreateIndex(
                name: "IX_SocioRinnovo_SocioId",
                table: "SocioRinnovo",
                column: "SocioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SocioFamiliare");

            migrationBuilder.DropTable(
                name: "SocioRinnovo");

            migrationBuilder.DropTable(
                name: "Socio");
        }
    }
}
