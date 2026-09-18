using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroServizi.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveEntityPersonalizzazioni : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Personalizzazioni");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Personalizzazioni",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeAssociazione = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SiglaAssociazione = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personalizzazioni", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Personalizzazioni_NomeAssociazione",
                table: "Personalizzazioni",
                column: "NomeAssociazione");
        }
    }
}
