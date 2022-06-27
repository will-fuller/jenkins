using Microsoft.EntityFrameworkCore.Migrations;

namespace ResearchTHM.Core.Migrations
{
    public partial class changeTableNameandboxid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_MktProcessRunlog_id",
                table: "MktProcessRunlog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MktProcessRunlog",
                table: "MktProcessRunlog");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_MktProcessList_id",
                table: "MktProcessList");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MktProcessList",
                table: "MktProcessList");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_MktProcessErrorlog_id",
                table: "MktProcessErrorlog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MktProcessErrorlog",
                table: "MktProcessErrorlog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MktBoxdocInfo",
                table: "MktBoxdocInfo");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_MktBoxdocInfo_id",
                table: "MktBoxdocInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MktAuditLog",
                table: "MktAuditLog");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_MktAuditLog_id",
                table: "MktAuditLog");

            migrationBuilder.RenameTable(
                name: "MktProcessRunlog",
                newName: "mkt_process_run_log");

            migrationBuilder.RenameTable(
                name: "MktProcessList",
                newName: "mkt_process_list");

            migrationBuilder.RenameTable(
                name: "MktProcessErrorlog",
                newName: "mkt_process_error_log");

            migrationBuilder.RenameTable(
                name: "MktBoxdocInfo",
                newName: "mkt_box_doc_info");

            migrationBuilder.RenameTable(
                name: "MktAuditLog",
                newName: "mkt_audit_log");

            migrationBuilder.AlterColumn<long>(
                name: "boxfile_id",
                table: "mkt_download_hist",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_mkt_process_run_log_id",
                table: "mkt_process_run_log",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_mkt_process_run_log",
                table: "mkt_process_run_log",
                column: "run_id");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_mkt_process_list_id",
                table: "mkt_process_list",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_mkt_process_list",
                table: "mkt_process_list",
                column: "process_id");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_mkt_process_error_log_id",
                table: "mkt_process_error_log",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_mkt_process_error_log",
                table: "mkt_process_error_log",
                column: "run_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_mkt_box_doc_info",
                table: "mkt_box_doc_info",
                column: "doc_id");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_mkt_box_doc_info_id",
                table: "mkt_box_doc_info",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_mkt_audit_log",
                table: "mkt_audit_log",
                column: "audit_id");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_mkt_audit_log_id",
                table: "mkt_audit_log",
                column: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_mkt_process_run_log_id",
                table: "mkt_process_run_log");

            migrationBuilder.DropPrimaryKey(
                name: "PK_mkt_process_run_log",
                table: "mkt_process_run_log");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_mkt_process_list_id",
                table: "mkt_process_list");

            migrationBuilder.DropPrimaryKey(
                name: "PK_mkt_process_list",
                table: "mkt_process_list");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_mkt_process_error_log_id",
                table: "mkt_process_error_log");

            migrationBuilder.DropPrimaryKey(
                name: "PK_mkt_process_error_log",
                table: "mkt_process_error_log");

            migrationBuilder.DropPrimaryKey(
                name: "PK_mkt_box_doc_info",
                table: "mkt_box_doc_info");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_mkt_box_doc_info_id",
                table: "mkt_box_doc_info");

            migrationBuilder.DropPrimaryKey(
                name: "PK_mkt_audit_log",
                table: "mkt_audit_log");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_mkt_audit_log_id",
                table: "mkt_audit_log");

            migrationBuilder.RenameTable(
                name: "mkt_process_run_log",
                newName: "MktProcessRunlog");

            migrationBuilder.RenameTable(
                name: "mkt_process_list",
                newName: "MktProcessList");

            migrationBuilder.RenameTable(
                name: "mkt_process_error_log",
                newName: "MktProcessErrorlog");

            migrationBuilder.RenameTable(
                name: "mkt_box_doc_info",
                newName: "MktBoxdocInfo");

            migrationBuilder.RenameTable(
                name: "mkt_audit_log",
                newName: "MktAuditLog");

            migrationBuilder.AlterColumn<string>(
                name: "boxfile_id",
                table: "mkt_download_hist",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_MktProcessRunlog_id",
                table: "MktProcessRunlog",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MktProcessRunlog",
                table: "MktProcessRunlog",
                column: "run_id");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_MktProcessList_id",
                table: "MktProcessList",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MktProcessList",
                table: "MktProcessList",
                column: "process_id");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_MktProcessErrorlog_id",
                table: "MktProcessErrorlog",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MktProcessErrorlog",
                table: "MktProcessErrorlog",
                column: "run_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MktBoxdocInfo",
                table: "MktBoxdocInfo",
                column: "doc_id");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_MktBoxdocInfo_id",
                table: "MktBoxdocInfo",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MktAuditLog",
                table: "MktAuditLog",
                column: "audit_id");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_MktAuditLog_id",
                table: "MktAuditLog",
                column: "id");
        }
    }
}
