using Microsoft.EntityFrameworkCore.Migrations;

namespace ResearchTHM.Core.Migrations
{
    public partial class updatedoctype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DocType",
                table: "MktBoxdocInfo",
                newName: "doc_type");

            migrationBuilder.AlterColumn<string>(
                name: "doc_type",
                table: "MktBoxdocInfo",
                unicode: false,
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "doc_type",
                table: "MktBoxdocInfo",
                newName: "DocType");

            migrationBuilder.AlterColumn<string>(
                name: "DocType",
                table: "MktBoxdocInfo",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 100,
                oldNullable: true);
        }
    }
}
