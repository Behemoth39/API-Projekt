using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace westcoasteducation.api.Data.Migrations
{
    /// <inheritdoc />
    public partial class qualiRel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "Qualifications",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Qualifications_TeacherId",
                table: "Qualifications",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Qualifications_Teachers_TeacherId",
                table: "Qualifications",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Qualifications_Teachers_TeacherId",
                table: "Qualifications");

            migrationBuilder.DropIndex(
                name: "IX_Qualifications_TeacherId",
                table: "Qualifications");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Qualifications");
        }
    }
}
