using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fnunez.VeterinaryClinic.SchedulingEmailSender.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_RetryCount_Email : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RetryCount",
                table: "Emails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RetryCount",
                table: "Emails");
        }
    }
}
