using Microsoft.EntityFrameworkCore.Migrations;

namespace RegistroServizi.Migrations
{
    public partial class TabellaCosto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Costo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CostoFisso_Amount = table.Column<float>(nullable: true),
                    CostoFisso_Currency = table.Column<string>(nullable: true),
                    CostoKm_Amount = table.Column<float>(nullable: true),
                    CostoKm_Currency = table.Column<string>(nullable: true),
                    SecondoTrasportato_Amount = table.Column<float>(nullable: true),
                    SecondoTrasportato_Currency = table.Column<string>(nullable: true),
                    FermoMacchina_Amount = table.Column<float>(nullable: true),
                    FermoMacchina_Currency = table.Column<string>(nullable: true),
                    Accompagnatore_Amount = table.Column<float>(nullable: true),
                    Accompagnatore_Currency = table.Column<string>(nullable: true),
                    ScontoSoci = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Costo", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Costo");
        }
    }
}
