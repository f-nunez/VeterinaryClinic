using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fnunez.VeterinaryClinic.Scheduling.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Auditable_Entities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Appointments",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedOn",
                table: "Appointments",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Appointments",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedOn",
                table: "Appointments",
                type: "datetimeoffset",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Appointments");
        }
    }
}
