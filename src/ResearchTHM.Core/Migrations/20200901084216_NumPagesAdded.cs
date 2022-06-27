using Microsoft.EntityFrameworkCore.Migrations;

namespace ResearchTHM.Core.Migrations
{
    public partial class NumPagesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "pageFrom",
                table: "mkt_user_activity",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "pageTo",
                table: "mkt_user_activity",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "pageFrom",
                table: "mkt_saved_search",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "pageTo",
                table: "mkt_saved_search",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "pageFrom",
                table: "mkt_user_activity");

            migrationBuilder.DropColumn(
                name: "pageTo",
                table: "mkt_user_activity");

            migrationBuilder.DropColumn(
                name: "pageFrom",
                table: "mkt_saved_search");

            migrationBuilder.DropColumn(
                name: "pageTo",
                table: "mkt_saved_search");
        }
    }
}
