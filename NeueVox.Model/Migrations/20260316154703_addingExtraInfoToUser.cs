using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeueVox.Model.Migrations
{
    /// <inheritdoc />
    public partial class addingExtraInfoToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Departement",
                table: "Professors",
                newName: "Department");

            migrationBuilder.AddColumn<string>(
                name: "Coordination",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureUrl",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Biography",
                table: "Professors",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Coordination",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProfilePictureUrl",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Biography",
                table: "Professors");

            migrationBuilder.RenameColumn(
                name: "Department",
                table: "Professors",
                newName: "Departement");
        }
    }
}
