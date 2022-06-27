using Microsoft.EntityFrameworkCore.Migrations;

namespace ResearchTHM.Core.Migrations
{
    public partial class addnotificationList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "notify_list",
                table: "mkt_process_run_log",
                unicode: false,
                maxLength: 4000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "notify_list",
                table: "mkt_process_list",
                unicode: false,
                maxLength: 4000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "notify_list",
                table: "mkt_process_run_log");

            migrationBuilder.DropColumn(
                name: "notify_list",
                table: "mkt_process_list");
        }
    }
}
