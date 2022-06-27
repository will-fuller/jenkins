using Microsoft.EntityFrameworkCore.Migrations;

namespace ResearchTHM.Core.Migrations
{
    public partial class Grouptbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "brower_version",
                table: "mkt_usage_log",
                newName: "browser_version");

            migrationBuilder.RenameColumn(
                name: "brower_name",
                table: "mkt_usage_log",
                newName: "browser_name");

            migrationBuilder.AddColumn<string>(
                name: "group_adname",
                table: "mkt_group",
                unicode: false,
                maxLength: 250,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "group_adname",
                table: "mkt_group");

            migrationBuilder.RenameColumn(
                name: "browser_version",
                table: "mkt_usage_log",
                newName: "brower_version");

            migrationBuilder.RenameColumn(
                name: "browser_name",
                table: "mkt_usage_log",
                newName: "brower_name");
        }
    }
}
