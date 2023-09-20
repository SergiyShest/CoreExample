using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddStyles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Font",
                table: "Vjsf");

            migrationBuilder.DropColumn(
                name: "ForeColor",
                table: "Vjsf");

            migrationBuilder.AddColumn<string>(
                name: "CssStyle",
                table: "Questionnaire",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CssStyle",
                table: "Questionnaire");

            migrationBuilder.AddColumn<string>(
                name: "Font",
                table: "Vjsf",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ForeColor",
                table: "Vjsf",
                type: "text",
                nullable: true);
        }
    }
}
