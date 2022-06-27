using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ResearchTHM.Core.Migrations
{
    public partial class usertablecolumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "msds_consistencyguid",
                table: "mkt_user",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "userprincipalname",
                table: "mkt_user",
                unicode: false,
                maxLength: 100,
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "mkt_download_hist",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "msds_consistencyguid",
                table: "mkt_user");

            migrationBuilder.DropColumn(
                name: "userprincipalname",
                table: "mkt_user");

            migrationBuilder.AlterColumn<Guid>(
                name: "user_id",
                table: "mkt_download_hist",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);
        }
    }
}
