using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ResearchTHM.Core.Migrations
{
    public partial class Savesearchcolunsupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_acc_group_id",
                table: "mkt_group_access");


            migrationBuilder.DropColumn(
                name: "company_name",
                table: "mkt_user_activity");

            migrationBuilder.DropColumn(
                name: "contributor_id",
                table: "mkt_user_activity");

            migrationBuilder.DropColumn(
                name: "daterange",
                table: "mkt_user_activity");

            migrationBuilder.DropColumn(
                name: "isexcludedCtb",
                table: "mkt_user_activity");

            migrationBuilder.DropColumn(
                name: "keyword_con",
                table: "mkt_user_activity");

            migrationBuilder.DropColumn(
                name: "matchStrHdln",
                table: "mkt_user_activity");

            migrationBuilder.DropColumn(
                name: "matchStrText",
                table: "mkt_user_activity");

            migrationBuilder.DropColumn(
                name: "report_types",
                table: "mkt_user_activity");

            migrationBuilder.DropColumn(
                name: "search_id",
                table: "mkt_user_activity");

            migrationBuilder.DropColumn(
                name: "ticker",
                table: "mkt_user_activity");

            migrationBuilder.DropColumn(
                name: "company_name",
                table: "mkt_saved_search");

            migrationBuilder.DropColumn(
                name: "contributor_id",
                table: "mkt_saved_search");

            migrationBuilder.DropColumn(
                name: "daterange",
                table: "mkt_saved_search");

            migrationBuilder.DropColumn(
                name: "isexcludedCtb",
                table: "mkt_saved_search");

            migrationBuilder.DropColumn(
                name: "keyword_con",
                table: "mkt_saved_search");

            migrationBuilder.DropColumn(
                name: "matchStrHdln",
                table: "mkt_saved_search");

            migrationBuilder.DropColumn(
                name: "matchStrText",
                table: "mkt_saved_search");

            migrationBuilder.DropColumn(
                name: "report_types",
                table: "mkt_saved_search");

            migrationBuilder.DropColumn(
                name: "ticker",
                table: "mkt_saved_search");

            migrationBuilder.RenameColumn(
                name: "region",
                table: "mkt_user_activity",
                newName: "Region");

            migrationBuilder.RenameColumn(
                name: "industry",
                table: "mkt_user_activity",
                newName: "Industry");

            migrationBuilder.RenameColumn(
                name: "user_name",
                table: "mkt_user_activity",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "initiating_cov",
                table: "mkt_user_activity",
                newName: "initiatingcov");

            migrationBuilder.RenameColumn(
                name: "date_to",
                table: "mkt_user_activity",
                newName: "dateTo");

            migrationBuilder.RenameColumn(
                name: "date_from",
                table: "mkt_user_activity",
                newName: "dateFrom");

            migrationBuilder.RenameColumn(
                name: "region",
                table: "mkt_saved_search",
                newName: "Region");

            migrationBuilder.RenameColumn(
                name: "industry",
                table: "mkt_saved_search",
                newName: "Industry");

            migrationBuilder.RenameColumn(
                name: "initiating_cov",
                table: "mkt_saved_search",
                newName: "initiatingcov");

            migrationBuilder.RenameColumn(
                name: "date_to",
                table: "mkt_saved_search",
                newName: "dateTo");

            migrationBuilder.RenameColumn(
                name: "date_from",
                table: "mkt_saved_search",
                newName: "dateFrom");

            migrationBuilder.AlterColumn<bool>(
                name: "status",
                table: "mkt_user_activity",
                nullable: true,
                defaultValueSql: "((1))",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "search_name",
                table: "mkt_user_activity",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "search_date",
                table: "mkt_user_activity",
                type: "datetime",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Region",
                table: "mkt_user_activity",
                unicode: false,
                maxLength: 3000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldUnicode: false,
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Industry",
                table: "mkt_user_activity",
                unicode: false,
                maxLength: 3000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldUnicode: false,
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "country",
                table: "mkt_user_activity",
                unicode: false,
                maxLength: 3000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldUnicode: false,
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "analyst",
                table: "mkt_user_activity",
                unicode: false,
                maxLength: 3000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldUnicode: false,
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "mkt_user_activity",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldUnicode: false,
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "initiatingcov",
                table: "mkt_user_activity",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "dateTo",
                table: "mkt_user_activity",
                unicode: false,
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "dateFrom",
                table: "mkt_user_activity",
                unicode: false,
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ECountry",
                table: "mkt_user_activity",
                unicode: false,
                maxLength: 3000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ERegion",
                table: "mkt_user_activity",
                unicode: false,
                maxLength: 3000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "company",
                table: "mkt_user_activity",
                unicode: false,
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "contributors",
                table: "mkt_user_activity",
                unicode: false,
                maxLength: 3000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "docId",
                table: "mkt_user_activity",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "excludeCtb",
                table: "mkt_user_activity",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "headlineSearch",
                table: "mkt_user_activity",
                unicode: false,
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "pHintStr",
                table: "mkt_user_activity",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "reportNo",
                table: "mkt_user_activity",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "reportStyles",
                table: "mkt_user_activity",
                unicode: false,
                maxLength: 3000,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "searchJoinCondition",
                table: "mkt_user_activity",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "textSearch",
                table: "mkt_user_activity",
                unicode: false,
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tocSearch",
                table: "mkt_user_activity",
                unicode: false,
                maxLength: 500,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Region",
                table: "mkt_saved_search",
                unicode: false,
                maxLength: 3000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldUnicode: false,
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Industry",
                table: "mkt_saved_search",
                unicode: false,
                maxLength: 3000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldUnicode: false,
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "country",
                table: "mkt_saved_search",
                unicode: false,
                maxLength: 3000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldUnicode: false,
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "analyst",
                table: "mkt_saved_search",
                unicode: false,
                maxLength: 3000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldUnicode: false,
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "initiatingcov",
                table: "mkt_saved_search",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "dateTo",
                table: "mkt_saved_search",
                unicode: false,
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "dateFrom",
                table: "mkt_saved_search",
                unicode: false,
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ECountry",
                table: "mkt_saved_search",
                unicode: false,
                maxLength: 3000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ERegion",
                table: "mkt_saved_search",
                unicode: false,
                maxLength: 3000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "company",
                table: "mkt_saved_search",
                unicode: false,
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "contributors",
                table: "mkt_saved_search",
                unicode: false,
                maxLength: 3000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "docId",
                table: "mkt_saved_search",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "excludeCtb",
                table: "mkt_saved_search",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "headlineSearch",
                table: "mkt_saved_search",
                unicode: false,
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "pHintStr",
                table: "mkt_saved_search",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "reportNo",
                table: "mkt_saved_search",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "reportStyles",
                table: "mkt_saved_search",
                unicode: false,
                maxLength: 3000,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "searchJoinCondition",
                table: "mkt_saved_search",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "textSearch",
                table: "mkt_saved_search",
                unicode: false,
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tocSearch",
                table: "mkt_saved_search",
                unicode: false,
                maxLength: 500,
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "group_id",
                table: "mkt_group_access",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "contributor_id",
                table: "mkt_group_access",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_acc_group_id",
                table: "mkt_group_access",
                column: "group_id",
                principalTable: "mkt_group",
                principalColumn: "group_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_acc_group_id",
                table: "mkt_group_access");

            migrationBuilder.DropColumn(
                name: "ECountry",
                table: "mkt_user_activity");

            migrationBuilder.DropColumn(
                name: "ERegion",
                table: "mkt_user_activity");

            migrationBuilder.DropColumn(
                name: "company",
                table: "mkt_user_activity");

            migrationBuilder.DropColumn(
                name: "contributors",
                table: "mkt_user_activity");

            migrationBuilder.DropColumn(
                name: "docId",
                table: "mkt_user_activity");

            migrationBuilder.DropColumn(
                name: "excludeCtb",
                table: "mkt_user_activity");

            migrationBuilder.DropColumn(
                name: "headlineSearch",
                table: "mkt_user_activity");

            migrationBuilder.DropColumn(
                name: "pHintStr",
                table: "mkt_user_activity");

            migrationBuilder.DropColumn(
                name: "reportNo",
                table: "mkt_user_activity");

            migrationBuilder.DropColumn(
                name: "reportStyles",
                table: "mkt_user_activity");

            migrationBuilder.DropColumn(
                name: "searchJoinCondition",
                table: "mkt_user_activity");

            migrationBuilder.DropColumn(
                name: "textSearch",
                table: "mkt_user_activity");

            migrationBuilder.DropColumn(
                name: "tocSearch",
                table: "mkt_user_activity");

            migrationBuilder.DropColumn(
                name: "ECountry",
                table: "mkt_saved_search");

            migrationBuilder.DropColumn(
                name: "ERegion",
                table: "mkt_saved_search");

            migrationBuilder.DropColumn(
                name: "company",
                table: "mkt_saved_search");

            migrationBuilder.DropColumn(
                name: "contributors",
                table: "mkt_saved_search");

            migrationBuilder.DropColumn(
                name: "docId",
                table: "mkt_saved_search");

            migrationBuilder.DropColumn(
                name: "excludeCtb",
                table: "mkt_saved_search");

            migrationBuilder.DropColumn(
                name: "headlineSearch",
                table: "mkt_saved_search");

            migrationBuilder.DropColumn(
                name: "pHintStr",
                table: "mkt_saved_search");

            migrationBuilder.DropColumn(
                name: "reportNo",
                table: "mkt_saved_search");

            migrationBuilder.DropColumn(
                name: "reportStyles",
                table: "mkt_saved_search");

            migrationBuilder.DropColumn(
                name: "searchJoinCondition",
                table: "mkt_saved_search");

            migrationBuilder.DropColumn(
                name: "textSearch",
                table: "mkt_saved_search");

            migrationBuilder.DropColumn(
                name: "tocSearch",
                table: "mkt_saved_search");

            migrationBuilder.RenameColumn(
                name: "Region",
                table: "mkt_user_activity",
                newName: "region");

            migrationBuilder.RenameColumn(
                name: "Industry",
                table: "mkt_user_activity",
                newName: "industry");

            migrationBuilder.RenameColumn(
                name: "initiatingcov",
                table: "mkt_user_activity",
                newName: "initiating_cov");

            migrationBuilder.RenameColumn(
                name: "dateTo",
                table: "mkt_user_activity",
                newName: "date_to");

            migrationBuilder.RenameColumn(
                name: "dateFrom",
                table: "mkt_user_activity",
                newName: "date_from");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "mkt_user_activity",
                newName: "user_name");

            migrationBuilder.RenameColumn(
                name: "Region",
                table: "mkt_saved_search",
                newName: "region");

            migrationBuilder.RenameColumn(
                name: "Industry",
                table: "mkt_saved_search",
                newName: "industry");

            migrationBuilder.RenameColumn(
                name: "initiatingcov",
                table: "mkt_saved_search",
                newName: "initiating_cov");

            migrationBuilder.RenameColumn(
                name: "dateTo",
                table: "mkt_saved_search",
                newName: "date_to");

            migrationBuilder.RenameColumn(
                name: "dateFrom",
                table: "mkt_saved_search",
                newName: "date_from");

            migrationBuilder.AlterColumn<bool>(
                name: "status",
                table: "mkt_user_activity",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true,
                oldDefaultValueSql: "((1))");

            migrationBuilder.AlterColumn<string>(
                name: "search_name",
                table: "mkt_user_activity",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "search_date",
                table: "mkt_user_activity",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<string>(
                name: "region",
                table: "mkt_user_activity",
                type: "varchar(200)",
                unicode: false,
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 3000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "industry",
                table: "mkt_user_activity",
                type: "varchar(200)",
                unicode: false,
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 3000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "country",
                table: "mkt_user_activity",
                type: "varchar(200)",
                unicode: false,
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 3000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "analyst",
                table: "mkt_user_activity",
                type: "varchar(200)",
                unicode: false,
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 3000,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "initiating_cov",
                table: "mkt_user_activity",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_to",
                table: "mkt_user_activity",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_from",
                table: "mkt_user_activity",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "user_name",
                table: "mkt_user_activity",
                type: "varchar(250)",
                unicode: false,
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "company_name",
                table: "mkt_user_activity",
                type: "varchar(200)",
                unicode: false,
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "contributor_id",
                table: "mkt_user_activity",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "daterange",
                table: "mkt_user_activity",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isexcludedCtb",
                table: "mkt_user_activity",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "keyword_con",
                table: "mkt_user_activity",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "matchStrHdln",
                table: "mkt_user_activity",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "matchStrText",
                table: "mkt_user_activity",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "report_types",
                table: "mkt_user_activity",
                type: "varchar(200)",
                unicode: false,
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "search_id",
                table: "mkt_user_activity",
                type: "varchar(250)",
                unicode: false,
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ticker",
                table: "mkt_user_activity",
                type: "varchar(250)",
                unicode: false,
                maxLength: 250,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "region",
                table: "mkt_saved_search",
                type: "varchar(200)",
                unicode: false,
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 3000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "industry",
                table: "mkt_saved_search",
                type: "varchar(200)",
                unicode: false,
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 3000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "country",
                table: "mkt_saved_search",
                type: "varchar(200)",
                unicode: false,
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 3000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "analyst",
                table: "mkt_saved_search",
                type: "varchar(200)",
                unicode: false,
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 3000,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "initiating_cov",
                table: "mkt_saved_search",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool));

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_to",
                table: "mkt_saved_search",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "date_from",
                table: "mkt_saved_search",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(string),
                oldUnicode: false,
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "company_name",
                table: "mkt_saved_search",
                type: "varchar(200)",
                unicode: false,
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "contributor_id",
                table: "mkt_saved_search",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "daterange",
                table: "mkt_saved_search",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isexcludedCtb",
                table: "mkt_saved_search",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "keyword_con",
                table: "mkt_saved_search",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "matchStrHdln",
                table: "mkt_saved_search",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "matchStrText",
                table: "mkt_saved_search",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "report_types",
                table: "mkt_saved_search",
                type: "varchar(200)",
                unicode: false,
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ticker",
                table: "mkt_saved_search",
                type: "varchar(250)",
                unicode: false,
                maxLength: 250,
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "group_id",
                table: "mkt_group_access",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "contributor_id",
                table: "mkt_group_access",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "fk_acc_group_id",
                table: "mkt_group_access",
                column: "group_id",
                principalTable: "mkt_group",
                principalColumn: "group_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
