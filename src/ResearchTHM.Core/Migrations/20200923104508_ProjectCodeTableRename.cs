using Microsoft.EntityFrameworkCore.Migrations;

namespace ResearchTHM.Core.Migrations
{
    public partial class ProjectCodeTableRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectCode",
                table: "ProjectCode");

            migrationBuilder.RenameTable(
                name: "ProjectCode",
                newName: "MktProjectCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MktProjectCode",
                table: "MktProjectCode",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MktProjectCode",
                table: "MktProjectCode");

            migrationBuilder.RenameTable(
                name: "MktProjectCode",
                newName: "ProjectCode");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectCode",
                table: "ProjectCode",
                column: "Id");
        }
    }
}
