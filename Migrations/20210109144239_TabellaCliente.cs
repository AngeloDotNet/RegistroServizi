using Microsoft.EntityFrameworkCore.Migrations;

namespace RegistroServizi.Migrations
{
    public partial class TabellaCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RagioneSociale = table.Column<string>(nullable: true),
                    Indirizzo = table.Column<string>(nullable: true),
                    Comune = table.Column<string>(nullable: true),
                    Cap = table.Column<string>(nullable: true),
                    Provincia = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    PartitaIva = table.Column<string>(nullable: true),
                    CodiceFiscale = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    Spese = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cliente");
        }
    }
}
