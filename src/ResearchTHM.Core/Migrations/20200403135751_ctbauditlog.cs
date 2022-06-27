using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ResearchTHM.Core.Migrations
{
    public partial class ctbauditlog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MktCtbAuditLog",
                columns: table => new
                {
                    audit_id = table.Column<Guid>(nullable: false),
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    included_ctb_ids = table.Column<string>(type: "text", unicode: false, nullable: true),
                    excluded_ctb_ids = table.Column<string>(type: "text", unicode: false, nullable: true),
                    isdeleted = table.Column<int>(nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MktCtbAuditLog", x => x.audit_id);
                    table.UniqueConstraint("AK_MktCtbAuditLog_ID", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MktCtbAuditLog");
        }
    }
}
