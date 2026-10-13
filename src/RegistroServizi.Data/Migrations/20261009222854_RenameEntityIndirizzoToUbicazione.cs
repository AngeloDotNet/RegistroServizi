using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroServizi.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameEntityIndirizzoToUbicazione : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Indirizzo_Provincia",
                table: "Ospedali",
                newName: "Ubicazione_Provincia");

            migrationBuilder.RenameColumn(
                name: "Indirizzo_Cap",
                table: "Ospedali",
                newName: "Ubicazione_Cap");

            migrationBuilder.RenameColumn(
                name: "Indirizzo_Strada",
                table: "Ospedali",
                newName: "Ubicazione_Indirizzo");

            migrationBuilder.RenameColumn(
                name: "Indirizzo_Citta",
                table: "Ospedali",
                newName: "Ubicazione_Comune");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ubicazione_Provincia",
                table: "Ospedali",
                newName: "Indirizzo_Provincia");

            migrationBuilder.RenameColumn(
                name: "Ubicazione_Cap",
                table: "Ospedali",
                newName: "Indirizzo_Cap");

            migrationBuilder.RenameColumn(
                name: "Ubicazione_Indirizzo",
                table: "Ospedali",
                newName: "Indirizzo_Strada");

            migrationBuilder.RenameColumn(
                name: "Ubicazione_Comune",
                table: "Ospedali",
                newName: "Indirizzo_Citta");
        }
    }
}
