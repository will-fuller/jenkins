using Microsoft.EntityFrameworkCore.Migrations;

namespace ResearchTHM.Core.Migrations
{
    public partial class countryRegionView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "file_id",
                table: "mkt_download_hist");

            migrationBuilder.DropColumn(
                name: "report_no",
                table: "mkt_download_hist");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "file_id",
                table: "mkt_download_hist",
                type: "varchar(250)",
                unicode: false,
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "report_no",
                table: "mkt_download_hist",
                type: "int",
                nullable: true);
        }
    }
}
