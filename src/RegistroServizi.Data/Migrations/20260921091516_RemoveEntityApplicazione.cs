using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroServizi.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveEntityApplicazione : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applicazioni");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Applicazioni",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeApplicazione = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TimeZone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Versione = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applicazioni", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applicazioni_NomeApplicazione",
                table: "Applicazioni",
                column: "NomeApplicazione");
        }
    }
}
