using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ResearchTHM.Core.Migrations
{
    public partial class AddGroupPriority1 : Migration
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


            migrationBuilder.AddColumn<int>(
                name: "priority",
                table: "mkt_group",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "mkt_audit_log",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "notify_list",
                table: "mkt_process_run_log");

            migrationBuilder.DropColumn(
              name: "notify_list",
              table: "mkt_process_list");

            migrationBuilder.DropColumn(
                name: "priority",
                table: "mkt_group");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "mkt_audit_log",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);
        }
    }
}
