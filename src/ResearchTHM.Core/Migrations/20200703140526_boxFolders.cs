using Microsoft.EntityFrameworkCore.Migrations;

namespace ResearchTHM.Core.Migrations
{
    public partial class boxFolders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BoxFolderInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    box_folder_id = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    folder_name = table.Column<string>(unicode: false, maxLength: 150, nullable: false),
                    parent_name = table.Column<string>(unicode: false, maxLength: 150, nullable: false),
                    parent_id = table.Column<string>(unicode: false, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoxFolderInfo", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoxFolderInfo_box_folder_id",
                table: "BoxFolderInfo",
                column: "box_folder_id");

            migrationBuilder.CreateIndex(
                name: "IX_BoxFolderInfo_folder_name",
                table: "BoxFolderInfo",
                column: "folder_name");

            migrationBuilder.CreateIndex(
                name: "IX_BoxFolderInfo_parent_name",
                table: "BoxFolderInfo",
                column: "parent_name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoxFolderInfo");
        }
    }
}
