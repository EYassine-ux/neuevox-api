using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NeueVox.Model.Migrations
{
  /// <inheritdoc />
  public partial class creatingSchedule : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "Schedules",
          columns: table => new
          {
            ScheduleId = table.Column<Guid>(type: "uuid", nullable: false),
            ClassId = table.Column<Guid>(type: "uuid", nullable: false),
            StartTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
            EndTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
            DayOfWeek = table.Column<int>(type: "integer", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Schedules", x => x.ScheduleId);
            table.ForeignKey(
                      name: "FK_Schedules_Classes_ClassId",
                      column: x => x.ClassId,
                      principalTable: "Classes",
                      principalColumn: "ClassId",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateIndex(
          name: "IX_Schedules_ClassId",
          table: "Schedules",
          column: "ClassId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "Schedules");
    }
  }
}
