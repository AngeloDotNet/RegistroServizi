using Microsoft.EntityFrameworkCore.Migrations;

namespace RegistroServizi.Migrations
{
    public partial class OwnedTypeSociRinnovi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropColumn(
                name: "Quota",
                table: "SocioRinnovo");

            migrationBuilder.AddColumn<float>(
                name: "Quota_Amount",
                table: "SocioRinnovo",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Quota_Currency",
                table: "SocioRinnovo",
                nullable: true);*/
            
            migrationBuilder.Sql(@"PRAGMA foreign_keys = 0;

                DROP TABLE SocioRinnovo;

                CREATE TABLE SocioRinnovo (
                    Id  INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    SocioId        INTEGER NOT NULL,
                    Anno           TEXT,
                    Quota_Amount   REAL,
                    Quota_Currency TEXT,
                    DataRinnovo    TEXT,
                    CONSTRAINT FK_SocioRinnovo_Socio_SocioId FOREIGN KEY (
                        SocioId
                    )
                    REFERENCES Socio (Id) ON DELETE CASCADE
                );

                PRAGMA foreign_keys = 1;", suppressTransaction: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropColumn(
                name: "Quota_Amount",
                table: "SocioRinnovo");

            migrationBuilder.DropColumn(
                name: "Quota_Currency",
                table: "SocioRinnovo");

            migrationBuilder.AddColumn<string>(
                name: "Quota",
                table: "SocioRinnovo",
                type: "TEXT",
                nullable: true);*/
            
            migrationBuilder.Sql(@"PRAGMA foreign_keys = 0;

                DROP TABLE SocioRinnovo;

                CREATE TABLE SocioRinnovo (
                    Id  INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    SocioId        INTEGER NOT NULL,
                    Anno           TEXT,
                    Quota          TEXT,
                    DataRinnovo    TEXT,
                    CONSTRAINT FK_SocioRinnovo_Socio_SocioId FOREIGN KEY (
                        SocioId
                    )
                    REFERENCES Socio (Id) ON DELETE CASCADE
                );

                PRAGMA foreign_keys = 1;", suppressTransaction: true);
        }
    }
}
