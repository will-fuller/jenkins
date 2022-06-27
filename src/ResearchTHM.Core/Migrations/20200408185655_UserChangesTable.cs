using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ResearchTHM.Core.Migrations
{
    public partial class UserChangesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mkt_user_preference");

            migrationBuilder.AddColumn<DateTime>(
                name: "last_updated_on",
                table: "MktCtbAuditLog",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "user_id",
                table: "MktCtbAuditLog",
                unicode: false,
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "prefer_contributor_id",
                table: "mkt_user",
                unicode: false,
                maxLength: 3000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "last_updated_on",
                table: "MktCtbAuditLog");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "MktCtbAuditLog");

            migrationBuilder.DropColumn(
                name: "prefer_contributor_id",
                table: "mkt_user");

            migrationBuilder.CreateTable(
                name: "mkt_user_preference",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    preference_id = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    preference_type = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mkt_user_preference", x => x.user_id);
                    table.ForeignKey(
                        name: "fk_pref_user_id",
                        column: x => x.user_id,
                        principalTable: "mkt_user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
