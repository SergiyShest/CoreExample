using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class simpleAnsver : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnsverHeader");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "dateTime",
                table: "Answer",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "time with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SessionId",
                table: "Answer",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character(100)",
                oldFixedLength: true,
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Salt",
                table: "Users",
                type: "text",
                nullable: true,
                defaultValueSql: "''::text",
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValueSql: "''::text");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "Users",
                type: "text",
                nullable: true,
                defaultValueSql: "''::text",
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValueSql: "''::text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "id");

            migrationBuilder.CreateTable(
                name: "HitCounter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SessionId = table.Column<string>(type: "text", nullable: true),
                    QuestionnaireId = table.Column<int>(type: "integer", nullable: true),
                    ScreenSize = table.Column<string>(type: "text", nullable: false),
                    Browser = table.Column<string>(type: "text", nullable: false),
                    BrowserVersion = table.Column<string>(type: "text", nullable: false),
                    Mobile = table.Column<string>(type: "text", nullable: false),
                    Os = table.Column<string>(type: "text", nullable: false),
                    OsVersion = table.Column<string>(type: "text", nullable: false),
                    Cookies = table.Column<string>(type: "text", nullable: false),
                    FlashVersion = table.Column<string>(type: "text", nullable: false),
                    Area = table.Column<string>(type: "text", nullable: false),
                    Cdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HitCounter", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SimpleAnsver",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuestionnarieId = table.Column<int>(type: "integer", nullable: true),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    UserEmail = table.Column<string>(type: "text", nullable: true),
                    UserPhone = table.Column<string>(type: "text", nullable: true),
                    SessionId = table.Column<string>(type: "text", nullable: true),
                    Cdate = table.Column<DateOnly>(type: "date", nullable: true),
                    dateTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimpleAnsver", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "v_answer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuestionnarieId = table.Column<int>(type: "integer", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<string>(type: "text", nullable: true),
                    QuestionId = table.Column<int>(type: "integer", nullable: true),
                    Cdate = table.Column<DateOnly>(type: "date", nullable: true),
                    SessionId = table.Column<string>(type: "text", nullable: true),
                    Time = table.Column<string>(type: "text", nullable: true),
                    Os = table.Column<string>(type: "text", nullable: true),
                    Area = table.Column<string>(type: "text", nullable: true),
                    Browser = table.Column<string>(type: "text", nullable: true),
                    ScreenSize = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_v_answer", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HitCounter");

            migrationBuilder.DropTable(
                name: "SimpleAnsver");

            migrationBuilder.DropTable(
                name: "v_answer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "dateTime",
                table: "Answer",
                type: "time with time zone",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SessionId",
                table: "Answer",
                type: "character(100)",
                fixedLength: true,
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Salt",
                table: "User",
                type: "text",
                nullable: false,
                defaultValueSql: "''::text",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldDefaultValueSql: "''::text");

            migrationBuilder.AlterColumn<string>(
                name: "PasswordHash",
                table: "User",
                type: "text",
                nullable: false,
                defaultValueSql: "''::text",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true,
                oldDefaultValueSql: "''::text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "id");

            migrationBuilder.CreateTable(
                name: "AnsverHeader",
                columns: table => new
                {
                    Area = table.Column<string>(type: "text", nullable: false),
                    Browser = table.Column<string>(type: "text", nullable: false),
                    BrowserVersion = table.Column<string>(type: "text", nullable: false),
                    Cdate = table.Column<DateOnly>(type: "date", nullable: true),
                    Cookies = table.Column<string>(type: "text", nullable: false),
                    FlashVersion = table.Column<string>(type: "text", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: true),
                    Mobile = table.Column<string>(type: "text", nullable: false),
                    Os = table.Column<string>(type: "text", nullable: false),
                    OsVersion = table.Column<string>(type: "text", nullable: false),
                    QuestionnaireId = table.Column<int>(type: "integer", nullable: true),
                    ScreenSize = table.Column<string>(type: "text", nullable: false),
                    SessionId = table.Column<string>(type: "text", nullable: true),
                    Time = table.Column<TimeOnly>(type: "time without time zone", nullable: true)
                },
                constraints: table =>
                {
                });
        }
    }
}
