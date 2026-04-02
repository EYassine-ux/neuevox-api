using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeueVox.Model.Migrations
{
    /// <inheritdoc />
    public partial class addingDescriptionStartEndToEval : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Evaluations",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Evaluations",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Evaluations",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Evaluations");
        }
    }
}
