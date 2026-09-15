using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroServizi.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTableTitoliStudio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TitoliStudio",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descrizione = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TitoliStudio", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TitoliStudio_Descrizione",
                table: "TitoliStudio",
                column: "Descrizione",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TitoliStudio");
        }
    }
}
