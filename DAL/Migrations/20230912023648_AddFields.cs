using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable 

namespace DAL.Migrations 
{
    /// <inheritdoc />
    public partial class AddFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vjsf_Name",
                table: "Vjsf");

            migrationBuilder.DropIndex(
                name: "IX_Questionnaire_Name",
                table: "Questionnaire");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.AddColumn<string>(
                name: "BackGroundColor",
                table: "Vjsf",
                type: "text",
                nullable: true);

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

            migrationBuilder.AddColumn<string>(
                name: "NextButtonText",
                table: "Vjsf",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrevButtonText",
                table: "Vjsf",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Salt",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BackGroundColor",
                table: "Vjsf");

            migrationBuilder.DropColumn(
                name: "Font",
                table: "Vjsf");

            migrationBuilder.DropColumn(
                name: "ForeColor",
                table: "Vjsf");

            migrationBuilder.DropColumn(
                name: "NextButtonText",
                table: "Vjsf");

            migrationBuilder.DropColumn(
                name: "PrevButtonText",
                table: "Vjsf");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.AlterColumn<string>(
                name: "Salt",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "id");

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
    }
}
