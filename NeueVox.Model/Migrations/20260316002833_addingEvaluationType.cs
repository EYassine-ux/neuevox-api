using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeueVox.Model.Migrations
{
    /// <inheritdoc />
    public partial class addingEvaluationType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EvaluationType",
                table: "Evaluations",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EvaluationType",
                table: "Evaluations");
        }
    }
}
