using Microsoft.EntityFrameworkCore.Migrations;

namespace ResearchTHM.Core.Migrations
{
    public partial class savedTextSearchColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "headlineSearch",
                table: "mkt_saved_search");

            migrationBuilder.DropColumn(
                name: "textSearch",
                table: "mkt_saved_search");

            migrationBuilder.DropColumn(
                name: "tocSearch",
                table: "mkt_saved_search");

            migrationBuilder.AlterColumn<string>(
                name: "dateRange",
                table: "mkt_saved_search",
                unicode: false,
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldUnicode: false,
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "keyword1Search",
                table: "mkt_saved_search",
                unicode: false,
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "keyword1Type",
                table: "mkt_saved_search",
                unicode: false,
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "keyword2Search",
                table: "mkt_saved_search",
                unicode: false,
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "keyword2Type",
                table: "mkt_saved_search",
                unicode: false,
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "keyword3Search",
                table: "mkt_saved_search",
                unicode: false,
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "keyword3Type",
                table: "mkt_saved_search",
                unicode: false,
                maxLength: 20,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "keyword1Search",
                table: "mkt_saved_search");

            migrationBuilder.DropColumn(
                name: "keyword1Type",
                table: "mkt_saved_search");

            migrationBuilder.DropColumn(
                name: "keyword2Search",
                table: "mkt_saved_search");

            migrationBuilder.DropColumn(
                name: "keyword2Type",
                table: "mkt_saved_search");

            migrationBuilder.DropColumn(
                name: "keyword3Search",
                table: "mkt_saved_search");

            migrationBuilder.DropColumn(
                name: "keyword3Type",
                table: "mkt_saved_search");

            migrationBuilder.AlterColumn<string>(
                name: "dateRange",
                table: "mkt_saved_search",
                type: "varchar(150)",
                unicode: false,
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "headlineSearch",
                table: "mkt_saved_search",
                type: "varchar(500)",
                unicode: false,
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "textSearch",
                table: "mkt_saved_search",
                type: "varchar(500)",
                unicode: false,
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tocSearch",
                table: "mkt_saved_search",
                type: "varchar(500)",
                unicode: false,
                maxLength: 500,
                nullable: true);
        }
    }
}
