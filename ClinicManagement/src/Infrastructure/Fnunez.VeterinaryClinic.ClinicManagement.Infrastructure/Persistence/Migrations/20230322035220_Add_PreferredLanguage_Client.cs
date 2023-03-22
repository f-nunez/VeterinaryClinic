using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fnunez.VeterinaryClinic.ClinicManagement.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_PreferredLanguage_Client : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PreferredLanguage",
                table: "Clients",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreferredLanguage",
                table: "Clients");
        }
    }
}
