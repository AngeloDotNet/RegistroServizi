using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroServizi.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTableSquadreFeriali : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SquadreFeriali",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Squadra = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SquadreFeriali", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SquadreFeriali");
        }
    }
}
