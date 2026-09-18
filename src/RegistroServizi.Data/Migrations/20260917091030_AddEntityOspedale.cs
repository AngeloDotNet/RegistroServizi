using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroServizi.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddEntityOspedale : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ospedali",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeOspedale = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Coordinate_Latitudine = table.Column<double>(type: "float", nullable: false),
                    Coordinate_Longitudine = table.Column<double>(type: "float", nullable: false),
                    Indirizzo_Cap = table.Column<int>(type: "int", nullable: false),
                    Indirizzo_Citta = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Indirizzo_Provincia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Indirizzo_Strada = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ospedali", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ospedali_NomeOspedale",
                table: "Ospedali",
                column: "NomeOspedale",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ospedali");
        }
    }
}
