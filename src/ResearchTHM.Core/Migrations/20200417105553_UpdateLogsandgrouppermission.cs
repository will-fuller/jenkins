using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ResearchTHM.Core.Migrations
{
    public partial class UpdateLogsandgrouppermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "entityid",
                table: "MktCtbAuditLog",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "entityname",
                table: "MktCtbAuditLog",
                unicode: false,
                maxLength: 150,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "mkt_group_permission",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    group_id = table.Column<Guid>(nullable: false),
                    permission_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mkt_group_permission", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mkt_group_permission");

            migrationBuilder.DropColumn(
                name: "entityid",
                table: "MktCtbAuditLog");

            migrationBuilder.DropColumn(
                name: "entityname",
                table: "MktCtbAuditLog");
        }
    }
}
