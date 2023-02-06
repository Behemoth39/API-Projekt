using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace westcoasteducation.api.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeacherModelId",
                table: "Qualifications",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Qualifications_TeacherModelId",
                table: "Qualifications",
                column: "TeacherModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Qualifications_Teachers_TeacherModelId",
                table: "Qualifications",
                column: "TeacherModelId",
                principalTable: "Teachers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Qualifications_Teachers_TeacherModelId",
                table: "Qualifications");

            migrationBuilder.DropIndex(
                name: "IX_Qualifications_TeacherModelId",
                table: "Qualifications");

            migrationBuilder.DropColumn(
                name: "TeacherModelId",
                table: "Qualifications");
        }
    }
}
