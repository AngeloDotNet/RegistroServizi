#nullable disable

namespace RegistroServizi.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultTimeZoneApplicazione : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.Sql("UPDATE Applicazioni SET TimeZone = 'Europe/Rome' WHERE TimeZone IS NULL");
            //migrationBuilder.Sql("UPDATE Applicazioni SET TimeZone = 'Europe/Rome' WHERE TimeZone = ''");
            migrationBuilder.Sql("UPDATE Applicazioni SET TimeZone = 'Europe/Rome' WHERE TimeZone IS NULL OR TimeZone = ''");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}