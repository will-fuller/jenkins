using Microsoft.EntityFrameworkCore.Migrations;

namespace ResearchTHM.Core.Migrations
{
    public partial class adddaterangetosavedsearch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "search_name",
                table: "mkt_user_activity");

            migrationBuilder.AddColumn<string>(
                name: "dateRange",
                table: "mkt_saved_search",
                unicode: false,
                maxLength: 150,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "dateRange",
                table: "mkt_saved_search");

            migrationBuilder.AddColumn<string>(
                name: "search_name",
                table: "mkt_user_activity",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
