using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ResearchTHM.Core.Migrations
{
    public partial class jobtableandlogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "docId",
                table: "mkt_user_activity",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "project_code",
                table: "mkt_user_activity",
                unicode: false,
                maxLength: 20,
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "doc_id",
                table: "mkt_download_hist",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldUnicode: false,
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "boxfile_id",
                table: "mkt_download_hist",
                unicode: false,
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "project_code",
                table: "mkt_download_hist",
                unicode: false,
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_mkt_permission_ID",
                table: "mkt_permission",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "MktAuditLog",
                columns: table => new
                {
                    audit_id = table.Column<Guid>(nullable: false),
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    run_id = table.Column<Guid>(nullable: false),
                    process_id = table.Column<Guid>(nullable: false),
                    process_name = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    user_id = table.Column<Guid>(nullable: false),
                    user_name = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    included_ids = table.Column<string>(type: "text", unicode: false, nullable: true),
                    excluded_ids = table.Column<string>(type: "text", unicode: false, nullable: true),
                    deleted_ids = table.Column<string>(type: "text", unicode: false, nullable: true),
                    updated_on = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MktAuditLog", x => x.audit_id);
                    table.UniqueConstraint("AK_MktAuditLog_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "MktBoxdocInfo",
                columns: table => new
                {
                    doc_id = table.Column<long>(nullable: false),
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocType = table.Column<string>(nullable: true),
                    box_id = table.Column<long>(nullable: false),
                    sequence_id = table.Column<int>(nullable: false),
                    etag = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    file_name = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    description = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    file_size = table.Column<int>(nullable: false),
                    created_on = table.Column<DateTime>(type: "datetime", nullable: false),
                    modified_on = table.Column<DateTime>(type: "datetime", nullable: false),
                    content_created_on = table.Column<DateTime>(type: "datetime", nullable: false),
                    content_modified_on = table.Column<DateTime>(type: "datetime", nullable: false),
                    created_by_type = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    created_by_id = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    created_by_name = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    modified_by_type = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    modified_by_id = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    modified_by_name = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    owned_by_type = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    owned_by_id = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    owned_by_name = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    parent_type = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    parent_id = table.Column<long>(nullable: false),
                    parent_sequence_id = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    parent_etag = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    parent_name = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    item_status = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MktBoxdocInfo", x => x.doc_id);
                    table.UniqueConstraint("AK_MktBoxdocInfo_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "MktProcessErrorlog",
                columns: table => new
                {
                    run_id = table.Column<Guid>(nullable: false),
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    process_id = table.Column<Guid>(nullable: false),
                    error_code = table.Column<string>(unicode: false, maxLength: 500, nullable: true),
                    error_log = table.Column<string>(unicode: false, maxLength: 4000, nullable: true),
                    created_by_id = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    created_by = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    created_on = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MktProcessErrorlog", x => x.run_id);
                    table.UniqueConstraint("AK_MktProcessErrorlog_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "MktProcessList",
                columns: table => new
                {
                    process_id = table.Column<Guid>(nullable: false),
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    process_name = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    process_type = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    sch_flag = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    notify_flag = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    created_by_id = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    created_by = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    created_on = table.Column<DateTime>(type: "datetime", nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MktProcessList", x => x.process_id);
                    table.UniqueConstraint("AK_MktProcessList_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "MktProcessRunlog",
                columns: table => new
                {
                    run_id = table.Column<Guid>(nullable: false),
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    process_id = table.Column<Guid>(nullable: false),
                    start_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    end_date = table.Column<DateTime>(type: "datetime", nullable: false),
                    notify_flag = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    msg_log = table.Column<string>(unicode: false, maxLength: 4000, nullable: true),
                    user_id = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    user_name = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    run_status = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    created_by_id = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    created_by = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    created_on = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MktProcessRunlog", x => x.run_id);
                    table.UniqueConstraint("AK_MktProcessRunlog_id", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MktAuditLog");

            migrationBuilder.DropTable(
                name: "MktBoxdocInfo");

            migrationBuilder.DropTable(
                name: "MktProcessErrorlog");

            migrationBuilder.DropTable(
                name: "MktProcessList");

            migrationBuilder.DropTable(
                name: "MktProcessRunlog");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_mkt_permission_ID",
                table: "mkt_permission");

            migrationBuilder.DropColumn(
                name: "project_code",
                table: "mkt_user_activity");

            migrationBuilder.DropColumn(
                name: "boxfile_id",
                table: "mkt_download_hist");

            migrationBuilder.DropColumn(
                name: "project_code",
                table: "mkt_download_hist");

            migrationBuilder.AlterColumn<string>(
                name: "docId",
                table: "mkt_user_activity",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<string>(
                name: "doc_id",
                table: "mkt_download_hist",
                type: "varchar(250)",
                unicode: false,
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(long));
        }
    }
}
