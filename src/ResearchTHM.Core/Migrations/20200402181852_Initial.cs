using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ResearchTHM.Core.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "mkt_api_config",
                columns: table => new
                {
                    app_config_id = table.Column<Guid>(nullable: false),
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    app_id = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    api_name = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    user_id = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    password = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    end_point = table.Column<string>(unicode: false, maxLength: 1000, nullable: true),
                    description = table.Column<string>(unicode: false, maxLength: 2000, nullable: true),
                    created_by_id = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    created_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    modified_by_id = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    modified_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by_id = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    deleted_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    isdeleted = table.Column<int>(nullable: true, defaultValueSql: "((0))"),
                    status = table.Column<bool>(nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mkt_api_config", x => x.app_config_id);
                });

            migrationBuilder.CreateTable(
                name: "mkt_category",
                columns: table => new
                {
                    category_id = table.Column<Guid>(nullable: false),
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category_Name = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    Global_Code = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    Local_Code = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    Set_Code = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    category_UID = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    Geographic_codes = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    created_by_id = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    created_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    modified_by_id = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    modified_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by_id = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    deleted_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_category_id", x => x.category_id);
                });

            migrationBuilder.CreateTable(
                name: "mkt_contributor",
                columns: table => new
                {
                    contributor_id = table.Column<Guid>(nullable: false),
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    contributor_name = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    contributor_alias = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    company_category = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    industry_id = table.Column<Guid>(nullable: true),
                    industry = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    profile_id = table.Column<Guid>(nullable: true),
                    profile_name = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    contributor_uid = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    embargo = table.Column<bool>(nullable: true),
                    setcode = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    setcodetype = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    author = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    author_id = table.Column<Guid>(nullable: true),
                    country = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    country_id = table.Column<Guid>(nullable: true),
                    currency = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    currency_id = table.Column<Guid>(nullable: true),
                    region = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    region_id = table.Column<Guid>(nullable: true),
                    subject = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    subject_id = table.Column<Guid>(nullable: true),
                    category = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    category_id = table.Column<Guid>(nullable: true),
                    is_excluded = table.Column<bool>(nullable: false),
                    created_by_id = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    created_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    modified_by_id = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    modified_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by_id = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    deleted_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    isdeleted = table.Column<int>(nullable: true),
                    status = table.Column<bool>(nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_contributor_id", x => x.contributor_id);
                    table.UniqueConstraint("AK_mkt_contributor_ID", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "mkt_country",
                columns: table => new
                {
                    country_id = table.Column<Guid>(nullable: false),
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    country_Name = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    Global_Code = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    Local_Code = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    Set_Code = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    country_UID = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    Geographic_codes = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    created_by_id = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    created_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    modified_by_id = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    modified_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by_id = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    deleted_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    isdeleted = table.Column<int>(nullable: true),
                    status = table.Column<bool>(nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_coutry_id", x => x.country_id);
                });

            migrationBuilder.CreateTable(
                name: "mkt_currency",
                columns: table => new
                {
                    Currency_id = table.Column<Guid>(nullable: false),
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Currency_Name = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    Global_Code = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    Local_Code = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    Set_Code = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    currency_UID = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    Geographic_codes = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    created_by_id = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    created_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    modified_by_id = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    modified_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by_id = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    deleted_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    isdeleted = table.Column<int>(nullable: true),
                    status = table.Column<bool>(nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_currency_id", x => x.Currency_id);
                });

            migrationBuilder.CreateTable(
                name: "mkt_download_hist",
                columns: table => new
                {
                    download_hist_id = table.Column<Guid>(nullable: false),
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    doc_id = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    doc_name = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    doc_title = table.Column<string>(unicode: false, maxLength: 2000, nullable: true),
                    doc_release_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    user_id = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    user_name = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    analyst = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    contributor_name = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    contributor_id = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    file_id = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    file_name = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    file_size = table.Column<int>(nullable: true),
                    file_type = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    page_no = table.Column<int>(nullable: true),
                    price = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    source = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    report_no = table.Column<int>(nullable: true),
                    created_by_id = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    created_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    modified_by_id = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    modified_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by_id = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    deleted_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    isdeleted = table.Column<int>(nullable: true, defaultValueSql: "((0))"),
                    status = table.Column<bool>(nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mkt_download_hist", x => x.download_hist_id);
                });

            migrationBuilder.CreateTable(
                name: "mkt_group",
                columns: table => new
                {
                    group_id = table.Column<Guid>(nullable: false),
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    group_name = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    group_description = table.Column<string>(unicode: false, maxLength: 1000, nullable: true),
                    created_by_id = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    created_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    modified_by_id = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    modified_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by_id = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    deleted_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    isdeleted = table.Column<int>(nullable: true),
                    status = table.Column<bool>(nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_group_id", x => x.group_id);
                });

            migrationBuilder.CreateTable(
                name: "mkt_industry",
                columns: table => new
                {
                    Industry_id = table.Column<Guid>(nullable: false),
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Industry_Name = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    Global_Code = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    Local_Code = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    Set_Code = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    industry_UID = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    Geographic_codes = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    created_by_id = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    created_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    modified_by_id = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    modified_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by_id = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    deleted_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    isdeleted = table.Column<int>(nullable: true),
                    status = table.Column<bool>(nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_industry_id", x => x.Industry_id);
                });

            migrationBuilder.CreateTable(
                name: "mkt_language",
                columns: table => new
                {
                    language_id = table.Column<Guid>(nullable: false),
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Language_Name = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    MX_Language_Map = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    RFC_1766 = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    MX_Existing_Language = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    Code_Page = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    Language_UID = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    created_by_id = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    created_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    modified_by_id = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    modified_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by_id = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    deleted_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    isdeleted = table.Column<int>(nullable: true),
                    status = table.Column<bool>(nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_language_id", x => x.language_id);
                });

            migrationBuilder.CreateTable(
                name: "mkt_permission",
                columns: table => new
                {
                    permission_id = table.Column<Guid>(nullable: false),
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    permission_name = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    permission_description = table.Column<string>(unicode: false, maxLength: 1000, nullable: true),
                    created_by_id = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    created_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    modified_by_id = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    modified_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by_id = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    deleted_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    isdeleted = table.Column<int>(nullable: true),
                    status = table.Column<bool>(nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_permission_id", x => x.permission_id);
                });

            migrationBuilder.CreateTable(
                name: "mkt_region",
                columns: table => new
                {
                    region_id = table.Column<Guid>(nullable: false),
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Region_Name = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    Global_Code = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    Local_Code = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    Set_Code = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    region_UID = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    Geographic_codes = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    created_by_id = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    created_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    modified_by_id = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    modified_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by_id = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    deleted_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    isdeleted = table.Column<int>(nullable: true),
                    status = table.Column<bool>(nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_region_id", x => x.region_id);
                });

            migrationBuilder.CreateTable(
                name: "mkt_role",
                columns: table => new
                {
                    role_id = table.Column<Guid>(nullable: false),
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role_name = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    role_description = table.Column<string>(unicode: false, maxLength: 1000, nullable: true),
                    created_by_id = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    created_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    modified_by_id = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    modified_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by_id = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    deleted_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    isdeleted = table.Column<int>(nullable: true),
                    status = table.Column<bool>(nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role_id", x => x.role_id);
                });

            migrationBuilder.CreateTable(
                name: "mkt_savedsearch",
                columns: table => new
                {
                    search_id = table.Column<Guid>(nullable: false),
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<string>(unicode: false, maxLength: 250, nullable: false),
                    search_name = table.Column<string>(maxLength: 100, nullable: false),
                    company_name = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    initiating_cov = table.Column<bool>(nullable: true),
                    date_from = table.Column<DateTime>(type: "datetime", nullable: true),
                    date_to = table.Column<DateTime>(type: "datetime", nullable: true),
                    daterange = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    analyst = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    region = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    industry = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    country = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    ticker = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    matchStrHdln = table.Column<string>(maxLength: 1000, nullable: true),
                    matchStrText = table.Column<string>(maxLength: 1000, nullable: true),
                    keyword_con = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    report_types = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    contributor_id = table.Column<string>(unicode: false, maxLength: 2000, nullable: true),
                    isexcludedCtb = table.Column<bool>(nullable: true),
                    created_by_id = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    created_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    modified_by_id = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    modified_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by_id = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    deleted_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    isdeleted = table.Column<int>(nullable: true),
                    status = table.Column<bool>(nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mkt_savedsearch", x => x.search_id);
                });

            migrationBuilder.CreateTable(
                name: "mkt_subject",
                columns: table => new
                {
                    subject_id = table.Column<Guid>(nullable: false),
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject_Name = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    Global_Code = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    Local_Code = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    Set_Code = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    subject_UID = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    Geographic_codes = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    created_by_id = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    created_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    modified_by_id = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    modified_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by_id = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    deleted_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    isdeleted = table.Column<int>(nullable: true),
                    status = table.Column<bool>(nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_subject_id", x => x.subject_id);
                });

            migrationBuilder.CreateTable(
                name: "mkt_user",
                columns: table => new
                {
                    user_id = table.Column<Guid>(nullable: false),
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    employeeid = table.Column<long>(nullable: true),
                    first_name = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    last_name = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    company = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    department_name = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    department_no = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    display_name = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    location = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    country = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    title = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    samaccountname = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    email_id = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    mobile_no = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    created_by_id = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    created_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    modified_by_id = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    modified_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by_id = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    deleted_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    isdeleted = table.Column<int>(nullable: true),
                    status = table.Column<bool>(nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_id", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "mkt_user_activity",
                columns: table => new
                {
                    user_activity_id = table.Column<Guid>(nullable: false),
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    search_id = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    user_id = table.Column<string>(unicode: false, maxLength: 250, nullable: false),
                    user_name = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    search_name = table.Column<string>(maxLength: 100, nullable: true),
                    company_name = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    initiating_cov = table.Column<bool>(nullable: true),
                    date_from = table.Column<DateTime>(type: "datetime", nullable: true),
                    date_to = table.Column<DateTime>(type: "datetime", nullable: true),
                    daterange = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    analyst = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    region = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    industry = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    country = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    ticker = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    matchStrHdln = table.Column<string>(maxLength: 1000, nullable: true),
                    matchStrText = table.Column<string>(maxLength: 1000, nullable: true),
                    keyword_con = table.Column<string>(unicode: false, maxLength: 20, nullable: true),
                    report_types = table.Column<string>(unicode: false, maxLength: 200, nullable: true),
                    contributor_id = table.Column<string>(unicode: false, maxLength: 2000, nullable: true),
                    isexcludedCtb = table.Column<bool>(nullable: true),
                    search_date = table.Column<DateTime>(type: "datetime", nullable: true),
                    created_by_id = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    created_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    modified_by_id = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    modified_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    deleted_by_id = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    deleted_on = table.Column<DateTime>(type: "datetime", nullable: true),
                    isdeleted = table.Column<int>(nullable: true),
                    status = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mkt_user_activity", x => x.user_activity_id);
                });

            migrationBuilder.CreateTable(
                name: "mkt_group_access",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    group_id = table.Column<Guid>(nullable: true),
                    contributor_id = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mkt_group_access", x => x.ID);
                    table.ForeignKey(
                        name: "fk_acc_group_id",
                        column: x => x.group_id,
                        principalTable: "mkt_group",
                        principalColumn: "group_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "mkt_role_access",
                columns: table => new
                {
                    role_id = table.Column<Guid>(nullable: false),
                    permission_id = table.Column<Guid>(nullable: false),
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mkt_role_access", x => new { x.permission_id, x.role_id });
                    table.ForeignKey(
                        name: "fk_acc_permission_id",
                        column: x => x.permission_id,
                        principalTable: "mkt_permission",
                        principalColumn: "permission_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_acc_role_id",
                        column: x => x.role_id,
                        principalTable: "mkt_role",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mkt_user_group_access",
                columns: table => new
                {
                    user_id = table.Column<Guid>(nullable: false),
                    group_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mkt_user_group_access", x => new { x.group_id, x.user_id });
                    table.ForeignKey(
                        name: "fk_ug_acc_group_id",
                        column: x => x.group_id,
                        principalTable: "mkt_group",
                        principalColumn: "group_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_ug_acc_user_id",
                        column: x => x.user_id,
                        principalTable: "mkt_user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mkt_user_preference",
                columns: table => new
                {
                    user_id = table.Column<Guid>(nullable: false),
                    preference_type = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    preference_id = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mkt_user_preference", x => x.user_id);
                    table.ForeignKey(
                        name: "fk_pref_user_id",
                        column: x => x.user_id,
                        principalTable: "mkt_user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mkt_user_role_access",
                columns: table => new
                {
                    user_id = table.Column<Guid>(nullable: false),
                    role_id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mkt_user_role_access", x => new { x.role_id, x.user_id });
                    table.ForeignKey(
                        name: "fk_ur_acc_role_id",
                        column: x => x.role_id,
                        principalTable: "mkt_role",
                        principalColumn: "role_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_ur_acc_user_id",
                        column: x => x.user_id,
                        principalTable: "mkt_user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_mkt_group_access_group_id",
                table: "mkt_group_access",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "IX_mkt_role_access_role_id",
                table: "mkt_role_access",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_mkt_user_group_access_user_id",
                table: "mkt_user_group_access",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_mkt_user_role_access_user_id",
                table: "mkt_user_role_access",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mkt_api_config");

            migrationBuilder.DropTable(
                name: "mkt_category");

            migrationBuilder.DropTable(
                name: "mkt_contributor");

            migrationBuilder.DropTable(
                name: "mkt_country");

            migrationBuilder.DropTable(
                name: "mkt_currency");

            migrationBuilder.DropTable(
                name: "mkt_download_hist");

            migrationBuilder.DropTable(
                name: "mkt_group_access");

            migrationBuilder.DropTable(
                name: "mkt_industry");

            migrationBuilder.DropTable(
                name: "mkt_language");

            migrationBuilder.DropTable(
                name: "mkt_region");

            migrationBuilder.DropTable(
                name: "mkt_role_access");

            migrationBuilder.DropTable(
                name: "mkt_savedsearch");

            migrationBuilder.DropTable(
                name: "mkt_subject");

            migrationBuilder.DropTable(
                name: "mkt_user_activity");

            migrationBuilder.DropTable(
                name: "mkt_user_group_access");

            migrationBuilder.DropTable(
                name: "mkt_user_preference");

            migrationBuilder.DropTable(
                name: "mkt_user_role_access");

            migrationBuilder.DropTable(
                name: "mkt_permission");

            migrationBuilder.DropTable(
                name: "mkt_group");

            migrationBuilder.DropTable(
                name: "mkt_role");

            migrationBuilder.DropTable(
                name: "mkt_user");
        }
    }
}
