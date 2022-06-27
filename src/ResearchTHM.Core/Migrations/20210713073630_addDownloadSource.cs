using Microsoft.EntityFrameworkCore.Migrations;

namespace ResearchTHM.Core.Migrations
{
    public partial class addDownloadSource : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "source",
                table: "mkt_user_activity",
                type: "varchar(30)",
                unicode: false,
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "requestSource",
                table: "mkt_download_hist",
                type: "varchar(30)",
                unicode: false,
                maxLength: 30,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "source",
                table: "mkt_user_activity");

            migrationBuilder.DropColumn(
                name: "requestSource",
                table: "mkt_download_hist");
        }
    }
}
