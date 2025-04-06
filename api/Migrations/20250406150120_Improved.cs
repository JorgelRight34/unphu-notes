using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class Improved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ScheduleText",
                table: "SubjectGroups",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TeacherName",
                table: "SubjectGroups",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "Notes",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastPeriodLoginId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LastPeriodLoginYear",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScheduleText",
                table: "SubjectGroups");

            migrationBuilder.DropColumn(
                name: "TeacherName",
                table: "SubjectGroups");

            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "LastPeriodLoginId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastPeriodLoginYear",
                table: "AspNetUsers");
        }
    }
}
