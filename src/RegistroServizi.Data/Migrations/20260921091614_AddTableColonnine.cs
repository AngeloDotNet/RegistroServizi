using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroServizi.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTableColonnine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Colonnine",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeColonnina = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Comune = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Provincia = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Coordinate_Latitudine = table.Column<double>(type: "float", nullable: true, defaultValue: 0.0),
                    Coordinate_Longitudine = table.Column<double>(type: "float", nullable: true, defaultValue: 0.0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colonnine", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Colonnine_NomeColonnina",
                table: "Colonnine",
                column: "NomeColonnina");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Colonnine");
        }
    }
}
