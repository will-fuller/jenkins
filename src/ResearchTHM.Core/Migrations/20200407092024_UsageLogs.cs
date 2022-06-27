using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ResearchTHM.Core.Migrations
{
    public partial class UsageLogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "download_type",
                table: "mkt_download_hist",
                unicode: false,
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "pages",
                table: "mkt_download_hist",
                unicode: false,
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "saved",
                table: "mkt_download_hist",
                unicode: false,
                maxLength: 150,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "mkt_usage_log",
                columns: table => new
                {
                    log_id = table.Column<Guid>(nullable: false),
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    log_type = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    related = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    brower_name = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    brower_version = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    location = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    device = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    ip = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    log_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    user_id = table.Column<string>(unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mkt_usage_log", x => x.log_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mkt_usage_log");

            migrationBuilder.DropColumn(
                name: "download_type",
                table: "mkt_download_hist");

            migrationBuilder.DropColumn(
                name: "pages",
                table: "mkt_download_hist");

            migrationBuilder.DropColumn(
                name: "saved",
                table: "mkt_download_hist");
        }
    }
}
