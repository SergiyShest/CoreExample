using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddMain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Questionnaire",
                newName: "Text");

            migrationBuilder.AddColumn<bool>(
                name: "Main",
                table: "Questionnaire",
                type: "boolean",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vjsf_Name",
                table: "Vjsf",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questionnaire_Name",
                table: "Questionnaire",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vjsf_Name",
                table: "Vjsf");

            migrationBuilder.DropIndex(
                name: "IX_Questionnaire_Name",
                table: "Questionnaire");

            migrationBuilder.DropColumn(
                name: "Main",
                table: "Questionnaire");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Questionnaire",
                newName: "Code");
        }
    }
}
