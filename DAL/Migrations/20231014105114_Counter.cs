using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class Counter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Area",
                table: "AnsverHeader",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Browser",
                table: "AnsverHeader",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BrowserVersion",
                table: "AnsverHeader",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Cookies",
                table: "AnsverHeader",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FlashVersion",
                table: "AnsverHeader",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Mobile",
                table: "AnsverHeader",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Os",
                table: "AnsverHeader",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OsVersion",
                table: "AnsverHeader",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ScreenSize",
                table: "AnsverHeader",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Area",
                table: "AnsverHeader");

            migrationBuilder.DropColumn(
                name: "Browser",
                table: "AnsverHeader");

            migrationBuilder.DropColumn(
                name: "BrowserVersion",
                table: "AnsverHeader");

            migrationBuilder.DropColumn(
                name: "Cookies",
                table: "AnsverHeader");

            migrationBuilder.DropColumn(
                name: "FlashVersion",
                table: "AnsverHeader");

            migrationBuilder.DropColumn(
                name: "Mobile",
                table: "AnsverHeader");

            migrationBuilder.DropColumn(
                name: "Os",
                table: "AnsverHeader");

            migrationBuilder.DropColumn(
                name: "OsVersion",
                table: "AnsverHeader");

            migrationBuilder.DropColumn(
                name: "ScreenSize",
                table: "AnsverHeader");
        }
    }
}
