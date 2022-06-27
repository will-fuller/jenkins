using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ResearchTHM.Core.Migrations
{
    public partial class AddDefaultvalueandDeleteTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MktCtbAuditLog");

            migrationBuilder.DropColumn(
                name: "modified_by_id",
                table: "mkt_user_activity");

            migrationBuilder.DropColumn(
                name: "modified_on",
                table: "mkt_user_activity");

            migrationBuilder.DropColumn(
                name: "status",
                table: "mkt_user_activity");

            migrationBuilder.DropColumn(
                name: "status",
                table: "mkt_saved_search");

            migrationBuilder.DropColumn(
                name: "contributor_id",
                table: "mkt_download_hist");

            migrationBuilder.DropColumn(
                name: "created_by_id",
                table: "mkt_download_hist");

            migrationBuilder.DropColumn(
                name: "modified_by_id",
                table: "mkt_download_hist");

            migrationBuilder.DropColumn(
                name: "modified_on",
                table: "mkt_download_hist");

            migrationBuilder.DropColumn(
                name: "status",
                table: "mkt_download_hist");

            migrationBuilder.DropColumn(
                name: "user_name",
                table: "mkt_download_hist");

            migrationBuilder.AlterColumn<bool>(
                name: "isdeleted",
                table: "mkt_user_activity",
                nullable: false,
                defaultValueSql: "((0))",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "status",
                table: "mkt_user",
                nullable: false,
                defaultValueSql: "((1))",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValueSql: "((1))");

            migrationBuilder.AlterColumn<bool>(
                name: "isdeleted",
                table: "mkt_user",
                nullable: false,
                defaultValueSql: "((0))",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "isdeleted",
                table: "mkt_saved_search",
                nullable: false,
                defaultValueSql: "((0))",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "status",
                table: "mkt_role",
                nullable: false,
                defaultValueSql: "((1))",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValueSql: "((1))");

            migrationBuilder.AlterColumn<bool>(
                name: "isdeleted",
                table: "mkt_role",
                nullable: false,
                defaultValueSql: "((0))",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "status",
                table: "mkt_region",
                nullable: false,
                defaultValueSql: "((1))",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValueSql: "((1))");

            migrationBuilder.AlterColumn<bool>(
                name: "isdeleted",
                table: "mkt_region",
                nullable: false,
                defaultValueSql: "((0))",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "mkt_process_list",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<bool>(
                name: "status",
                table: "mkt_permission",
                nullable: false,
                defaultValueSql: "((1))",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValueSql: "((1))");

            migrationBuilder.AlterColumn<bool>(
                name: "isdeleted",
                table: "mkt_permission",
                nullable: false,
                defaultValueSql: "((0))",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "status",
                table: "mkt_industry",
                nullable: false,
                defaultValueSql: "((1))",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValueSql: "((1))");

            migrationBuilder.AlterColumn<bool>(
                name: "isdeleted",
                table: "mkt_industry",
                nullable: false,
                defaultValueSql: "((0))",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "status",
                table: "mkt_group",
                nullable: false,
                defaultValueSql: "((1))",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValueSql: "((1))");

            migrationBuilder.AlterColumn<bool>(
                name: "isdeleted",
                table: "mkt_group",
                nullable: false,
                defaultValueSql: "((0))",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "isdeleted",
                table: "mkt_download_hist",
                nullable: false,
                defaultValueSql: "((0))",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldDefaultValueSql: "((0))");

            migrationBuilder.AlterColumn<bool>(
                name: "status",
                table: "mkt_country",
                nullable: false,
                defaultValueSql: "((1))",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValueSql: "((1))");

            migrationBuilder.AlterColumn<bool>(
                name: "isdeleted",
                table: "mkt_country",
                nullable: false,
                defaultValueSql: "((0))",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "is_excluded",
                table: "mkt_contributor",
                nullable: false,
                defaultValueSql: "((0))",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "isdeleted",
                table: "mkt_contributor",
                nullable: false,
                defaultValueSql: "((0))",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "status",
                table: "mkt_api_config",
                nullable: false,
                defaultValueSql: "((1))",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValueSql: "((1))");

            migrationBuilder.AlterColumn<bool>(
                name: "isdeleted",
                table: "mkt_api_config",
                nullable: false,
                defaultValueSql: "((0))",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldDefaultValueSql: "((0))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "isdeleted",
                table: "mkt_user_activity",
                type: "int",
                nullable: true,
                oldClrType: typeof(bool),
                oldDefaultValueSql: "((0))");

            migrationBuilder.AddColumn<string>(
                name: "modified_by_id",
                table: "mkt_user_activity",
                type: "varchar(250)",
                unicode: false,
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "modified_on",
                table: "mkt_user_activity",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "status",
                table: "mkt_user_activity",
                type: "bit",
                nullable: true,
                defaultValueSql: "((1))");

            migrationBuilder.AlterColumn<bool>(
                name: "status",
                table: "mkt_user",
                type: "bit",
                nullable: true,
                defaultValueSql: "((1))",
                oldClrType: typeof(bool),
                oldDefaultValueSql: "((1))");

            migrationBuilder.AlterColumn<int>(
                name: "isdeleted",
                table: "mkt_user",
                type: "int",
                nullable: true,
                oldClrType: typeof(bool),
                oldDefaultValueSql: "((0))");

            migrationBuilder.AlterColumn<int>(
                name: "isdeleted",
                table: "mkt_saved_search",
                type: "int",
                nullable: true,
                oldClrType: typeof(bool),
                oldDefaultValueSql: "((0))");

            migrationBuilder.AddColumn<bool>(
                name: "status",
                table: "mkt_saved_search",
                type: "bit",
                nullable: true,
                defaultValueSql: "((1))");

            migrationBuilder.AlterColumn<bool>(
                name: "status",
                table: "mkt_role",
                type: "bit",
                nullable: true,
                defaultValueSql: "((1))",
                oldClrType: typeof(bool),
                oldDefaultValueSql: "((1))");

            migrationBuilder.AlterColumn<int>(
                name: "isdeleted",
                table: "mkt_role",
                type: "int",
                nullable: true,
                oldClrType: typeof(bool),
                oldDefaultValueSql: "((0))");

            migrationBuilder.AlterColumn<bool>(
                name: "status",
                table: "mkt_region",
                type: "bit",
                nullable: true,
                defaultValueSql: "((1))",
                oldClrType: typeof(bool),
                oldDefaultValueSql: "((1))");

            migrationBuilder.AlterColumn<int>(
                name: "isdeleted",
                table: "mkt_region",
                type: "int",
                nullable: true,
                oldClrType: typeof(bool),
                oldDefaultValueSql: "((0))");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "mkt_process_list",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<bool>(
                name: "status",
                table: "mkt_permission",
                type: "bit",
                nullable: true,
                defaultValueSql: "((1))",
                oldClrType: typeof(bool),
                oldDefaultValueSql: "((1))");

            migrationBuilder.AlterColumn<int>(
                name: "isdeleted",
                table: "mkt_permission",
                type: "int",
                nullable: true,
                oldClrType: typeof(bool),
                oldDefaultValueSql: "((0))");

            migrationBuilder.AlterColumn<bool>(
                name: "status",
                table: "mkt_industry",
                type: "bit",
                nullable: true,
                defaultValueSql: "((1))",
                oldClrType: typeof(bool),
                oldDefaultValueSql: "((1))");

            migrationBuilder.AlterColumn<int>(
                name: "isdeleted",
                table: "mkt_industry",
                type: "int",
                nullable: true,
                oldClrType: typeof(bool),
                oldDefaultValueSql: "((0))");

            migrationBuilder.AlterColumn<bool>(
                name: "status",
                table: "mkt_group",
                type: "bit",
                nullable: true,
                defaultValueSql: "((1))",
                oldClrType: typeof(bool),
                oldDefaultValueSql: "((1))");

            migrationBuilder.AlterColumn<int>(
                name: "isdeleted",
                table: "mkt_group",
                type: "int",
                nullable: true,
                oldClrType: typeof(bool),
                oldDefaultValueSql: "((0))");

            migrationBuilder.AlterColumn<int>(
                name: "isdeleted",
                table: "mkt_download_hist",
                type: "int",
                nullable: true,
                defaultValueSql: "((0))",
                oldClrType: typeof(bool),
                oldDefaultValueSql: "((0))");

            migrationBuilder.AddColumn<Guid>(
                name: "contributor_id",
                table: "mkt_download_hist",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "created_by_id",
                table: "mkt_download_hist",
                type: "varchar(150)",
                unicode: false,
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "modified_by_id",
                table: "mkt_download_hist",
                type: "varchar(150)",
                unicode: false,
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "modified_on",
                table: "mkt_download_hist",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "status",
                table: "mkt_download_hist",
                type: "bit",
                nullable: true,
                defaultValueSql: "((1))");

            migrationBuilder.AddColumn<string>(
                name: "user_name",
                table: "mkt_download_hist",
                type: "varchar(250)",
                unicode: false,
                maxLength: 250,
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "status",
                table: "mkt_country",
                type: "bit",
                nullable: true,
                defaultValueSql: "((1))",
                oldClrType: typeof(bool),
                oldDefaultValueSql: "((1))");

            migrationBuilder.AlterColumn<int>(
                name: "isdeleted",
                table: "mkt_country",
                type: "int",
                nullable: true,
                oldClrType: typeof(bool),
                oldDefaultValueSql: "((0))");

            migrationBuilder.AlterColumn<bool>(
                name: "is_excluded",
                table: "mkt_contributor",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldDefaultValueSql: "((0))");

            migrationBuilder.AlterColumn<int>(
                name: "isdeleted",
                table: "mkt_contributor",
                type: "int",
                nullable: true,
                oldClrType: typeof(bool),
                oldDefaultValueSql: "((0))");

            migrationBuilder.AlterColumn<bool>(
                name: "status",
                table: "mkt_api_config",
                type: "bit",
                nullable: true,
                defaultValueSql: "((1))",
                oldClrType: typeof(bool),
                oldDefaultValueSql: "((1))");

            migrationBuilder.AlterColumn<int>(
                name: "isdeleted",
                table: "mkt_api_config",
                type: "int",
                nullable: true,
                defaultValueSql: "((0))",
                oldClrType: typeof(bool),
                oldDefaultValueSql: "((0))");

            migrationBuilder.CreateTable(
                name: "MktCtbAuditLog",
                columns: table => new
                {
                    audit_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    entityid = table.Column<int>(type: "int", nullable: true),
                    entityname = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    excluded_ctb_ids = table.Column<string>(type: "text", unicode: false, nullable: true),
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    included_ctb_ids = table.Column<string>(type: "text", unicode: false, nullable: true),
                    isdeleted = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    last_updated_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    user_id = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    username = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MktCtbAuditLog", x => x.audit_id);
                    table.UniqueConstraint("AK_MktCtbAuditLog_ID", x => x.ID);
                });
        }
    }
}
