using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ResearchTHM.Core.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_acc_permission_id",
                table: "mkt_role_access");

            migrationBuilder.DropForeignKey(
                name: "fk_acc_role_id",
                table: "mkt_role_access");

            migrationBuilder.DropPrimaryKey(
                name: "PK_mkt_role_access",
                table: "mkt_role_access");

            migrationBuilder.RenameTable(
                name: "mkt_savedsearch",
                newName: "mkt_saved_search");

            migrationBuilder.AlterColumn<Guid>(
                name: "role_id",
                table: "mkt_role_access",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "permission_id",
                table: "mkt_role_access",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_mkt_role_access",
                table: "mkt_role_access",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_mkt_role_access_permission_id",
                table: "mkt_role_access",
                column: "permission_id");

            migrationBuilder.AddForeignKey(
                name: "fk_acc_permission_id",
                table: "mkt_role_access",
                column: "permission_id",
                principalTable: "mkt_permission",
                principalColumn: "permission_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_acc_role_id",
                table: "mkt_role_access",
                column: "role_id",
                principalTable: "mkt_role",
                principalColumn: "role_id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_acc_permission_id",
                table: "mkt_role_access");

            migrationBuilder.DropForeignKey(
                name: "fk_acc_role_id",
                table: "mkt_role_access");

            migrationBuilder.DropPrimaryKey(
                name: "PK_mkt_role_access",
                table: "mkt_role_access");

            migrationBuilder.DropIndex(
                name: "IX_mkt_role_access_permission_id",
                table: "mkt_role_access");

            migrationBuilder.RenameTable(
                name: "mkt_saved_search",
                newName: "mkt_savedsearch");

            migrationBuilder.AlterColumn<Guid>(
                name: "role_id",
                table: "mkt_role_access",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "permission_id",
                table: "mkt_role_access",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_mkt_role_access",
                table: "mkt_role_access",
                columns: new[] { "permission_id", "role_id" });

            migrationBuilder.AddForeignKey(
                name: "fk_acc_permission_id",
                table: "mkt_role_access",
                column: "permission_id",
                principalTable: "mkt_permission",
                principalColumn: "permission_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_acc_role_id",
                table: "mkt_role_access",
                column: "role_id",
                principalTable: "mkt_role",
                principalColumn: "role_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
