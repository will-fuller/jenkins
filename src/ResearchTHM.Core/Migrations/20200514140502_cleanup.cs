using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ResearchTHM.Core.Migrations
{
    public partial class cleanup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mkt_category");

            migrationBuilder.DropTable(
                name: "mkt_currency");

            migrationBuilder.DropTable(
                name: "mkt_language");

            migrationBuilder.DropTable(
                name: "mkt_subject");

            migrationBuilder.DropColumn(
                name: "keyword3Search",
                table: "mkt_saved_search");

            migrationBuilder.DropColumn(
                name: "keyword3Type",
                table: "mkt_saved_search");

            migrationBuilder.AlterColumn<string>(
                name: "reportStyles",
                table: "mkt_saved_search",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(3000)",
                oldUnicode: false,
                oldMaxLength: 3000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "contributors",
                table: "mkt_saved_search",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(3000)",
                oldUnicode: false,
                oldMaxLength: 3000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "company",
                table: "mkt_saved_search",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldUnicode: false,
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Region",
                table: "mkt_saved_search",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(3000)",
                oldUnicode: false,
                oldMaxLength: 3000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Industry",
                table: "mkt_saved_search",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(3000)",
                oldUnicode: false,
                oldMaxLength: 3000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ERegion",
                table: "mkt_saved_search",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(3000)",
                oldUnicode: false,
                oldMaxLength: 3000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ECountry",
                table: "mkt_saved_search",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(3000)",
                oldUnicode: false,
                oldMaxLength: 3000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "country",
                table: "mkt_saved_search",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(3000)",
                oldUnicode: false,
                oldMaxLength: 3000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "analyst",
                table: "mkt_saved_search",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(3000)",
                oldUnicode: false,
                oldMaxLength: 3000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "saved",
                table: "mkt_download_hist",
                unicode: false,
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldUnicode: false,
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "price",
                table: "mkt_download_hist",
                unicode: false,
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldUnicode: false,
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "download_type",
                table: "mkt_download_hist",
                unicode: false,
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldUnicode: false,
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "analyst",
                table: "mkt_download_hist",
                unicode: false,
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldUnicode: false,
                oldMaxLength: 250,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "reportStyles",
                table: "mkt_saved_search",
                type: "varchar(3000)",
                unicode: false,
                maxLength: 3000,
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "contributors",
                table: "mkt_saved_search",
                type: "varchar(3000)",
                unicode: false,
                maxLength: 3000,
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "company",
                table: "mkt_saved_search",
                type: "varchar(200)",
                unicode: false,
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Region",
                table: "mkt_saved_search",
                type: "varchar(3000)",
                unicode: false,
                maxLength: 3000,
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Industry",
                table: "mkt_saved_search",
                type: "varchar(3000)",
                unicode: false,
                maxLength: 3000,
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ERegion",
                table: "mkt_saved_search",
                type: "varchar(3000)",
                unicode: false,
                maxLength: 3000,
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ECountry",
                table: "mkt_saved_search",
                type: "varchar(3000)",
                unicode: false,
                maxLength: 3000,
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "country",
                table: "mkt_saved_search",
                type: "varchar(3000)",
                unicode: false,
                maxLength: 3000,
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "analyst",
                table: "mkt_saved_search",
                type: "varchar(3000)",
                unicode: false,
                maxLength: 3000,
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "keyword3Search",
                table: "mkt_saved_search",
                type: "varchar(500)",
                unicode: false,
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "keyword3Type",
                table: "mkt_saved_search",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "saved",
                table: "mkt_download_hist",
                type: "varchar(150)",
                unicode: false,
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "price",
                table: "mkt_download_hist",
                type: "varchar(250)",
                unicode: false,
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "download_type",
                table: "mkt_download_hist",
                type: "varchar(250)",
                unicode: false,
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "analyst",
                table: "mkt_download_hist",
                type: "varchar(250)",
                unicode: false,
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 2000,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "mkt_category",
                columns: table => new
                {
                    category_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Category_Name = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    category_UID = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    created_by_id = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    created_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by_id = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    deleted_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    Geographic_codes = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    Global_Code = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<int>(type: "int", nullable: true),
                    Local_Code = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    modified_by_id = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    modified_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    Set_Code = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_category_id", x => x.category_id);
                });

            migrationBuilder.CreateTable(
                name: "mkt_currency",
                columns: table => new
                {
                    Currency_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_by_id = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    created_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    Currency_Name = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    currency_UID = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    deleted_by_id = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    deleted_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    Geographic_codes = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    Global_Code = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    isdeleted = table.Column<int>(type: "int", nullable: true),
                    Local_Code = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    modified_by_id = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    modified_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    Set_Code = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_currency_id", x => x.Currency_id);
                });

            migrationBuilder.CreateTable(
                name: "mkt_language",
                columns: table => new
                {
                    language_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code_Page = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    created_by_id = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    created_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by_id = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    deleted_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    isdeleted = table.Column<int>(type: "int", nullable: true),
                    Language_Name = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    Language_UID = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    modified_by_id = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    modified_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    MX_Existing_Language = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    MX_Language_Map = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    RFC_1766 = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_language_id", x => x.language_id);
                });

            migrationBuilder.CreateTable(
                name: "mkt_subject",
                columns: table => new
                {
                    subject_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    created_by_id = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    created_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by_id = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    deleted_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    Geographic_codes = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    Global_Code = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    isdeleted = table.Column<int>(type: "int", nullable: true),
                    Local_Code = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    modified_by_id = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    modified_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    Set_Code = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    Subject_Name = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    subject_UID = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_subject_id", x => x.subject_id);
                });
        }
    }
}
