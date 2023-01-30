using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace westcoasteducation.api.Data.Migrations
{
    /// <inheritdoc />
    public partial class Key : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "courseTitle",
                table: "Courses",
                newName: "CourseTitle");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Teachers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Students",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "CourseTitle",
                table: "Courses",
                newName: "courseTitle");
        }
    }
}
