using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ResearchTHM.Core.Migrations
{
    public partial class residualMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LanguagePrefences",
                table: "mkt_user");

            migrationBuilder.AddColumn<string>(
                name: "LanguagePreferences",
                table: "mkt_user_activity",
                unicode: false,
                maxLength: 3000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LanguagePreferences",
                table: "mkt_user",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "mkt_language",
                columns: table => new
                {
                    language_id = table.Column<Guid>(nullable: false),
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Language_Name = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    MX_Language_Map = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    RFC_1766 = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    MX_Existing_Language = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    Code_Page = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    Language_UID = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    created_by_id = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    created_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    modified_by_id = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    modified_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by_id = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    deleted_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    isdeleted = table.Column<int>(nullable: false, defaultValueSql: "((0))"),
                    Status = table.Column<bool>(nullable: false, defaultValueSql: "((1))"),
                    Sort_Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_language_id", x => x.language_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mkt_language");

            migrationBuilder.DropColumn(
                name: "LanguagePreferences",
                table: "mkt_user_activity");

            migrationBuilder.DropColumn(
                name: "LanguagePreferences",
                table: "mkt_user");

            migrationBuilder.AddColumn<string>(
                name: "LanguagePrefences",
                table: "mkt_user",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
