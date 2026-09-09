using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroServizi.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixTableTipologiaServizio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrezziServizi_TipologieServizio_TipologiaServizioId",
                table: "PrezziServizi");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TipologieServizio",
                table: "TipologieServizio");

            migrationBuilder.RenameTable(
                name: "TipologieServizio",
                newName: "TipologieServizi");

            migrationBuilder.AlterColumn<string>(
                name: "TipoServizio",
                table: "TipologieServizi",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TipologieServizi",
                table: "TipologieServizi",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PrezziServizi_TipologieServizi_TipologiaServizioId",
                table: "PrezziServizi",
                column: "TipologiaServizioId",
                principalTable: "TipologieServizi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrezziServizi_TipologieServizi_TipologiaServizioId",
                table: "PrezziServizi");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TipologieServizi",
                table: "TipologieServizi");

            migrationBuilder.RenameTable(
                name: "TipologieServizi",
                newName: "TipologieServizio");

            migrationBuilder.AlterColumn<string>(
                name: "TipoServizio",
                table: "TipologieServizio",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TipologieServizio",
                table: "TipologieServizio",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PrezziServizi_TipologieServizio_TipologiaServizioId",
                table: "PrezziServizi",
                column: "TipologiaServizioId",
                principalTable: "TipologieServizio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
