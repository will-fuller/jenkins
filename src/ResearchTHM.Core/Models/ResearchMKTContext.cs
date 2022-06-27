using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ResearchTHM.Core.Models
{
    public partial class ResearchMktContext : DbContext
    {
        public ResearchMktContext()
        {
        }

        public ResearchMktContext(DbContextOptions<ResearchMktContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MktApiConfig> MktApiConfig { get; set; }
        public virtual DbSet<MktContributor> MktContributor { get; set; }
        public virtual DbSet<MktCountry> MktCountry { get; set; }
        public virtual DbSet<MktDownloadHist> MktDownloadHist { get; set; }
        public virtual DbSet<MktGroup> MktGroup { get; set; }
        public virtual DbSet<MktGroupAccess> MktGroupAccess { get; set; }
        public virtual DbSet<MktIndustry> MktIndustry { get; set; }
        public virtual DbSet<MktPermission> MktPermission { get; set; }
        public virtual DbSet<MktRegion> MktRegion { get; set; }
        public virtual DbSet<MktRole> MktRole { get; set; }
        public virtual DbSet<MktRoleAccess> MktRoleAccess { get; set; }
        public virtual DbSet<MktSavedSearch> MktSavedSearch { get; set; }
        public virtual DbSet<MktUser> MktUser { get; set; }
        public virtual DbSet<MktUserActivity> MktUserActivity { get; set; }
        public virtual DbSet<MktUserGroupAccess> MktUserGroupAccess { get; set; }
        public virtual DbSet<MktUserRoleAccess> MktUserRoleAccess { get; set; }
        public virtual DbSet<MktUsageLog> MktUsageLogs { get; set; }
        public virtual DbSet<MktGroupPermission> MktGroupPermission { get; set; }
        //Add additional Table
        public virtual DbSet<MktAuditLog> MktAuditLog { get; set; }
        public virtual DbSet<MktBoxdocInfo> MktBoxdocInfo { get; set; }
        public virtual DbSet<MktProcessList> MktProcessList { get; set; }
        public virtual DbSet<MktProcessRunlog> MktProcessRunlog { get; set; }
        public virtual DbSet<MktProcessErrorlog> MktProcessErrorlog { get; set; }
        public virtual DbSet<VwCountyRegion> VwCountyRegions { get; set; }
        public virtual DbSet<VwUserInfo> VwUserInfo { get; set; }
        public virtual DbSet<VwUserInfoDeveloper> VwUserInfoDeveloper { get; set; }
        public virtual DbSet<VwContributorGroup> VwContributorGroup { get; set; }
        public virtual DbSet<VwContributorGroupDeveloper> VwContributorGroupDeveloper { get; set; }
        public virtual DbSet<BoxFolderInfo> BoxFolderInfo { get; set; }
        public virtual DbSet<MktProjectCode> ProjectCode { get; set; }
        public virtual DbSet<MktCompanyMaster> CompanyMaster { get; set; }
        public virtual DbSet<MktLanguages> Languages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {


            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MktApiConfig>(entity =>
            {
                entity.HasKey(e => e.AppConfigId);

                entity.ToTable("mkt_api_config");

                entity.Property(e => e.AppConfigId)
                    .HasColumnName("app_config_id")
                    .ValueGeneratedNever();

                entity.ToTable("mkt_api_config");

                entity.Property(e => e.ApiName)
                    .HasColumnName("api_name")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.AppId)
                    .HasColumnName("app_id")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedById)
                    .HasColumnName("created_by_id")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedById)
                    .HasColumnName("deleted_by_id")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DeletedOn)
                    .HasColumnName("deleted_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.EndPoint)
                    .HasColumnName("end_point")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isdeleted")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedById)
                    .HasColumnName("modified_by_id")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modified_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MktContributor>(entity =>
            {
                entity.HasKey(e => e.ContributorId)
                    .HasName("pk_contributor_id");

                entity.HasAlternateKey(e => e.Id);

                entity.ToTable("mkt_contributor");

                entity.Property(e => e.ContributorId)
                    .HasColumnName("contributor_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Author)
                    .HasColumnName("author")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.AuthorId).HasColumnName("author_id");

                entity.Property(e => e.Category)
                    .HasColumnName("category")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.CompanyCategory)
                    .HasColumnName("company_category")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ContributorAlias)
                    .HasColumnName("contributor_alias")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ContributorName)
                    .HasColumnName("contributor_name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ContributorUid)
                    .HasColumnName("contributor_uid")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CountryId).HasColumnName("country_id");

                entity.Property(e => e.CreatedById)
                    .HasColumnName("created_by_id")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.Currency)
                    .HasColumnName("currency")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CurrencyId).HasColumnName("currency_id");

                entity.Property(e => e.DeletedById)
                    .HasColumnName("deleted_by_id")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DeletedOn)
                    .HasColumnName("deleted_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.Embargo).HasColumnName("embargo");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Industry)
                    .HasColumnName("industry")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.IndustryId).HasColumnName("industry_id");

                entity.Property(e => e.IsExcluded).HasColumnName("is_excluded").HasDefaultValueSql("((0))"); 

                entity.Property(e => e.IsDeleted).HasColumnName("isdeleted").HasDefaultValueSql("((0))"); 

                entity.Property(e => e.ModifiedById)
                    .HasColumnName("modified_by_id")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modified_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.ProfileId).HasColumnName("profile_id");

                entity.Property(e => e.ProfileName)
                    .HasColumnName("profile_name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Region)
                    .HasColumnName("region")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.RegionId).HasColumnName("region_id");

                entity.Property(e => e.Setcode)
                    .HasColumnName("setcode")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Setcodetype)
                    .HasColumnName("setcodetype")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Subject)
                    .HasColumnName("subject")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.SubjectId).HasColumnName("subject_id");
            });

            modelBuilder.Entity<MktCountry>(entity =>
            {
                entity.HasKey(e => e.CountryId)
                    .HasName("pk_coutry_id");

                entity.ToTable("mkt_country");

                entity.Property(e => e.CountryId)
                    .HasColumnName("country_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CountryName)
                    .HasColumnName("country_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CountryUid)
                    .HasColumnName("country_UID")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedById)
                    .HasColumnName("created_by_id")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedById)
                    .HasColumnName("deleted_by_id")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.DeletedOn)
                    .HasColumnName("deleted_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.GeographicCodes)
                    .HasColumnName("Geographic_codes")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.GlobalCode)
                    .HasColumnName("Global_Code")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IsDeleted).HasColumnName("isdeleted").HasDefaultValueSql("((0))");

                entity.Property(e => e.LocalCode)
                    .HasColumnName("Local_Code")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedById)
                    .HasColumnName("modified_by_id")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modified_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.SetCode)
                    .HasColumnName("Set_Code")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<MktDownloadHist>(entity =>
            {
                entity.HasKey(e => e.DownloadHistId);

                entity.ToTable("mkt_download_hist");

                entity.Property(e => e.DownloadHistId)
                    .HasColumnName("download_hist_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Analyst)
                    .HasColumnName("analyst")
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.ContributorName)
                    .HasColumnName("contributor_name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedById)
                    .HasColumnName("deleted_by_id")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.DeletedOn)
                    .HasColumnName("deleted_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.DocId)
                    .HasColumnName("doc_id");

                entity.Property(e => e.ProjectCode)
                    .HasColumnName("project_code")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.BoxFileId)
                    .HasColumnName("boxfile_id");

                entity.Property(e => e.DocName)
                    .HasColumnName("doc_name")
                    .HasMaxLength(500)
                    .IsUnicode(true);

                entity.Property(e => e.DocReleaseDate)
                    .HasColumnName("doc_release_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DocTitle)
                    .HasColumnName("doc_title")
                    .HasMaxLength(2000) 
                    .IsUnicode(true);

                entity.Property(e => e.FileName)
                    .HasColumnName("file_name")
                    .HasMaxLength(500)
                    .IsUnicode(true);

                entity.Property(e => e.FileSize).HasColumnName("file_size");

                entity.Property(e => e.FileType)
                    .HasColumnName("file_type")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IsDeleted)
                    .HasColumnName("isdeleted")
                    .HasDefaultValueSql("((0))");

          
                entity.Property(e => e.PageNo).HasColumnName("page_no");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.RequestSource)
                    .HasColumnName("requestSource")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Source)
                    .HasColumnName("source")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id");

                entity.Property(e => e.Saved)
                    .HasColumnName("saved")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Pages)
                    .HasColumnName("pages")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.DownloadType)
                   .HasColumnName("download_type")
                   .HasMaxLength(50)
                   .IsUnicode(false);

                entity.Property(e => e.RequestId).HasColumnName("request_id").HasMaxLength(30).IsUnicode(false);

            });

            modelBuilder.Entity<MktGroup>(entity =>
            {
                entity.HasKey(e => e.GroupId)
                    .HasName("pk_group_id");

                entity.ToTable("mkt_group");

                entity.Property(e => e.GroupId)
                    .HasColumnName("group_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedById)
                    .HasColumnName("created_by_id")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedById)
                    .HasColumnName("deleted_by_id")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DeletedOn)
                    .HasColumnName("deleted_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.GroupDescription)
                    .HasColumnName("group_description")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.GroupName)
                    .HasColumnName("group_name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IsDeleted).HasColumnName("isdeleted").HasDefaultValueSql("((0))"); ;

                entity.Property(e => e.ModifiedById)
                    .HasColumnName("modified_by_id")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modified_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.GroupAdName)
                   .HasColumnName("group_adname")
                   .HasMaxLength(250)
                   .IsUnicode(false);

                entity.Property(e => e.Priority).HasColumnName("priority");
            });

            modelBuilder.Entity<MktGroupAccess>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("mkt_group_access");

                entity.Property(e => e.ContributorId).HasColumnName("contributor_id");

                entity.Property(e => e.GroupId).HasColumnName("group_id");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.MktGroupAccess)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("fk_acc_group_id");
            });

            modelBuilder.Entity<MktIndustry>(entity =>
            {
                entity.HasKey(e => e.IndustryId)
                    .HasName("pk_industry_id");

                entity.ToTable("mkt_industry");

                entity.Property(e => e.IndustryId)
                    .HasColumnName("Industry_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedById)
                    .HasColumnName("created_by_id")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedById)
                    .HasColumnName("deleted_by_id")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.DeletedOn)
                    .HasColumnName("deleted_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.GeographicCodes)
                    .HasColumnName("Geographic_codes")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.GlobalCode)
                    .HasColumnName("Global_Code")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IndustryName)
                    .HasColumnName("Industry_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.IndustryUid)
                    .HasColumnName("industry_UID")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.IsDeleted).HasColumnName("isdeleted").HasDefaultValueSql("((0))"); ;

                entity.Property(e => e.LocalCode)
                    .HasColumnName("Local_Code")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedById)
                    .HasColumnName("modified_by_id")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modified_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.SetCode)
                    .HasColumnName("Set_Code")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<MktPermission>(entity =>
            {
                entity.HasKey(e => e.PermissionId)
                    .HasName("pk_permission_id");

                entity.ToTable("mkt_permission");

                entity.Property(e => e.PermissionId)
                    .HasColumnName("permission_id")
                    .ValueGeneratedNever();

                entity.HasAlternateKey(e => e.Id);

                entity.Property(e => e.CreatedById)
                    .HasColumnName("created_by_id")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedById)
                    .HasColumnName("deleted_by_id")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DeletedOn)
                    .HasColumnName("deleted_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IsDeleted).HasColumnName("isdeleted").HasDefaultValueSql("((0))"); 

                entity.Property(e => e.ModifiedById)
                    .HasColumnName("modified_by_id")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modified_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.PermissionDescription)
                    .HasColumnName("permission_description")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.PermissionName)
                    .HasColumnName("permission_name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<MktRegion>(entity =>
            {
                entity.HasKey(e => e.RegionId)
                    .HasName("pk_region_id");

                entity.ToTable("mkt_region");

                entity.Property(e => e.RegionId)
                    .HasColumnName("region_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedById)
                    .HasColumnName("created_by_id")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedById)
                    .HasColumnName("deleted_by_id")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.DeletedOn)
                    .HasColumnName("deleted_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.GeographicCodes)
                    .HasColumnName("Geographic_codes")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.GlobalCode)
                    .HasColumnName("Global_Code")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IsDeleted).HasColumnName("isdeleted").HasDefaultValueSql("((0))");

                entity.Property(e => e.LocalCode)
                    .HasColumnName("Local_Code")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedById)
                    .HasColumnName("modified_by_id")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modified_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.RegionName)
                    .HasColumnName("Region_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.RegionUid)
                    .HasColumnName("region_UID")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.SetCode)
                    .HasColumnName("Set_Code")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<MktRole>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("pk_role_id");

                entity.ToTable("mkt_role");

                entity.Property(e => e.RoleId)
                    .HasColumnName("role_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedById)
                    .HasColumnName("created_by_id")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedById)
                    .HasColumnName("deleted_by_id")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DeletedOn)
                    .HasColumnName("deleted_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IsDeleted).HasColumnName("isdeleted").HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedById)
                    .HasColumnName("modified_by_id")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modified_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.RoleDescription)
                    .HasColumnName("role_description")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.RoleName)
                    .HasColumnName("role_name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<MktRoleAccess>(entity =>
            {
                //entity.HasNoKey();
                //entity.HasKey(e => new { e.PermissionId, e.RoleId });
                entity.HasKey(e => e.Id);

                entity.ToTable("mkt_role_access");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.PermissionId).HasColumnName("permission_id");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.MktRoleAccess)
                    .HasForeignKey(d => d.PermissionId)
                    .HasConstraintName("fk_acc_permission_id");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.MktRoleAccess)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("fk_acc_role_id");
            });

            modelBuilder.Entity<MktSavedSearch>(entity =>
            {
                entity.HasKey(e => e.SearchId)
                    .HasName("PK_mkt_savedsearch");

                entity.ToTable("mkt_saved_search");

                entity.HasAlternateKey(e => e.Id);

                entity.Property(e => e.SearchId)
                    .HasColumnName("search_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("user_id");

                entity.Property(e => e.SearchName)
                   .IsRequired()
                   .HasColumnName("search_name")
                   .HasMaxLength(100);

                entity.Property(e => e.company)
                   .HasColumnName("company")
                   .IsUnicode(false);

                entity.Property(e => e.initiatingCov).HasColumnName("initiatingcov");

                entity.Property(e => e.dateRange)
                    .HasColumnName("dateRange")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.dateFrom)
                    .HasColumnName("dateFrom")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.dateTo)
                    .HasColumnName("dateTo")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.keyword1Search)
                    .HasColumnName("keyword1Search")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.keyword2Search)
                    .HasColumnName("keyword2Search")
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.keyword1Type)
                    .HasColumnName("keyword1Type")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.keyword2Type)
                    .HasColumnName("keyword2Type")
                    .HasMaxLength(20)
                    .IsUnicode(false);
                
                entity.Property(e => e.searchJoinCondition).HasColumnName("searchJoinCondition");

                entity.Property(e => e.reportNo).HasColumnName("reportNo").HasMaxLength(15);

                entity.Property(e => e.reportStyles)
                   .HasColumnName("reportStyles")
                   .IsUnicode(false);

                entity.Property(e => e.Industry)
                    .HasColumnName("Industry")
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                   .HasColumnName("country")
                   .IsUnicode(false);

                entity.Property(e => e.ECountry)
                   .HasColumnName("ECountry")
                   .IsUnicode(false);

                entity.Property(e => e.Region)
                   .HasColumnName("Region")
                   .IsUnicode(false);

                entity.Property(e => e.ERegion)
                   .HasColumnName("ERegion")
                   .IsUnicode(false);

                entity.Property(e => e.contributors)
                   .HasColumnName("contributors")
                   .IsUnicode(false);

                entity.Property(e => e.excludeCtb).HasColumnName("excludeCtb");

                entity.Property(e => e.Analyst)
                    .HasColumnName("analyst")
                    .IsUnicode(false);

                entity.Property(e => e.pHintStr).HasColumnName("pHintStr");

                entity.Property(e => e.docId).HasColumnName("docId");

                entity.Property(e => e.CreatedById)
                    .HasColumnName("created_by_id")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedById)
                    .HasColumnName("deleted_by_id")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DeletedOn)
                    .HasColumnName("deleted_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedById)
                    .HasColumnName("modified_by_id")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modified_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.IsDeleted).HasColumnName("isdeleted").HasDefaultValueSql("((0))");

                entity.Property(e => e.PagesFrom).HasColumnName("pageFrom").IsUnicode(false).HasMaxLength(10);
                entity.Property(e => e.PagesTo).HasColumnName("pageTo").IsUnicode(false).HasMaxLength(10);
            });

            modelBuilder.Entity<MktUser>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("pk_user_id");

                entity.ToTable("mkt_user");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Company)
                    .HasColumnName("company")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedById)
                    .HasColumnName("created_by_id")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedById)
                    .HasColumnName("deleted_by_id")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DeletedOn)
                    .HasColumnName("deleted_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.DepartmentName)
                    .HasColumnName("department_name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DepartmentNo)
                    .HasColumnName("department_no")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DisplayName)
                    .HasColumnName("display_name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.EmailId)
                    .HasColumnName("email_id")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Employeeid).HasColumnName("employeeid");

                entity.Property(e => e.FirstName)
                    .HasColumnName("first_name")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IsDeleted).HasColumnName("isdeleted").HasDefaultValueSql("((0))"); 

                entity.Property(e => e.LastName)
                    .HasColumnName("last_name")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Location)
                    .HasColumnName("location")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.MobileNo)
                    .HasColumnName("mobile_no")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedById)
                    .HasColumnName("modified_by_id")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modified_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.Samaccountname)
                    .HasColumnName("samaccountname")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PreferContributorId)
                    .HasColumnName("prefer_contributor_id")
                    .HasMaxLength(3000)
                    .IsUnicode(false);

                entity.Property(e => e.UserPrincipalName).HasColumnName("userprincipalname").HasMaxLength(100).IsUnicode(false);
                entity.Property(e => e.MsdsConsitencyGuid).HasColumnName("msds_consistencyguid");
            });

            modelBuilder.Entity<MktUserActivity>(entity =>
            {
                entity.HasKey(e => e.UserActivityId);

                entity.ToTable("mkt_user_activity");

                entity.Property(e => e.UserActivityId)
                    .HasColumnName("user_activity_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasColumnName("user_id");

                entity.Property(e => e.company)
                   .HasColumnName("company")
                   .HasMaxLength(3000)
                   .IsUnicode(false);

                entity.Property(e => e.initiatingCov).HasColumnName("initiatingcov");

                entity.Property(e => e.dateFrom)
                    .HasColumnName("dateFrom")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.dateTo)
                    .HasColumnName("dateTo")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.headlineSearch)
                   .HasColumnName("headlineSearch")
                   .HasMaxLength(500)
                   .IsUnicode(false);

                entity.Property(e => e.textSearch)
                  .HasColumnName("textSearch")
                  .HasMaxLength(500)
                  .IsUnicode(false);

                entity.Property(e => e.tocSearch)
                  .HasColumnName("tocSearch")
                  .HasMaxLength(500)
                  .IsUnicode(false);

                entity.Property(e => e.searchJoinCondition).HasColumnName("searchJoinCondition");

                entity.Property(e => e.reportNo).HasColumnName("reportNo").HasMaxLength(15);

                entity.Property(e => e.reportStyles)
                   .HasColumnName("reportStyles")
                   .HasMaxLength(3000)
                   .IsUnicode(false);

                entity.Property(e => e.Industry)
                    .HasColumnName("Industry")
                    .HasMaxLength(3000)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                   .HasColumnName("country")
                   .HasMaxLength(3000)
                   .IsUnicode(false);

                entity.Property(e => e.ECountry)
                   .HasColumnName("ECountry")
                   .HasMaxLength(3000)
                   .IsUnicode(false);

                entity.Property(e => e.Region)
                   .HasColumnName("Region")
                   .HasMaxLength(3000)
                   .IsUnicode(false);

                entity.Property(e => e.ERegion)
                   .HasColumnName("ERegion")
                   .HasMaxLength(3000)
                   .IsUnicode(false);

                entity.Property(e => e.contributors)
                   .HasColumnName("contributors")
                   .HasMaxLength(3000)
                   .IsUnicode(false);

                entity.Property(e => e.excludeCtb).HasColumnName("excludeCtb");

                entity.Property(e => e.Analyst)
                    .HasColumnName("analyst")
                    .HasMaxLength(3000)
                    .IsUnicode(false);

                entity.Property(e => e.pHintStr).HasColumnName("pHintStr");

                entity.Property(e => e.docId).HasColumnName("docId");

                entity.Property(e => e.SearchDate)
                   .HasColumnName("search_date")
                   .HasColumnType("datetime");


                entity.Property(e => e.CreatedById)
                    .HasColumnName("created_by_id")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedById)
                    .HasColumnName("deleted_by_id")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DeletedOn)
                    .HasColumnName("deleted_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.IsDeleted).HasColumnName("isdeleted").HasDefaultValueSql("((0))"); 

                entity.Property(e => e.ProjectCode)
                    .HasColumnName("project_code")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PagesFrom).HasColumnName("pageFrom").IsUnicode(false).HasMaxLength(10);
                entity.Property(e => e.PagesTo).HasColumnName("pageTo").IsUnicode(false).HasMaxLength(10);

                entity.Property(e => e.LanguagePreferences)
                   .HasColumnName("LanguagePreferences")
                   .HasMaxLength(3000)
                   .IsUnicode(false);

                entity.Property(e => e.Source)
                    .HasColumnName("source")
                    .HasMaxLength(30)
                    .IsUnicode(false);

            });

            modelBuilder.Entity<MktUserGroupAccess>(entity =>
            {
                //entity.HasNoKey();
                entity.HasKey(e => new { e.GroupId, e.UserId });

                entity.ToTable("mkt_user_group_access");

                entity.Property(e => e.GroupId).HasColumnName("group_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.MktUserGroupAccess)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("fk_ug_acc_group_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MktUserGroupAccess)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_ug_acc_user_id");
            });

            modelBuilder.Entity<MktGroupPermission>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("mkt_group_permission");
                entity.Property(e => e.GroupId).HasColumnName("group_id");
                entity.Property(e => e.PermissionId).HasColumnName("permission_id");
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<MktUserRoleAccess>(entity =>
            {
                //entity.HasNoKey();
                //#warning The Key on MktUserRoleAccess does not exist in DB but has been mentioned here so that ef doesn't throw error
                entity.HasKey(e => new { e.RoleId, e.UserId });

                entity.ToTable("mkt_user_role_access");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.MktUserRoleAccess)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("fk_ur_acc_role_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MktUserRoleAccess)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_ur_acc_user_id");
            });

            modelBuilder.Entity<MktUsageLog>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.ToTable("mkt_usage_log");

                entity.Property(e => e.LogId)
                    .HasColumnName("log_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Id)
                  .HasColumnName("ID")
                  .ValueGeneratedOnAdd();

                entity.Property(e => e.LogType)
                    .HasColumnName("log_type")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Related)
                    .HasColumnName("related")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.BrowserName)
                    .HasColumnName("browser_name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.BrowserVersion)
                    .HasColumnName("browser_version")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Location)
                    .HasColumnName("location")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Device)
                   .HasColumnName("device")
                   .HasMaxLength(250)
                   .IsUnicode(false);

                entity.Property(e => e.IP)
                  .HasColumnName("ip")
                  .HasMaxLength(250)
                  .IsUnicode(false);

                entity.Property(e => e.LogDate)
                   .HasColumnName("log_date")
                   .HasColumnType("datetime");

                entity.Property(e => e.UserId)
                  .HasColumnName("user_id");

                entity.Property(e => e.LoginSource)
                    .HasColumnName("login_source")
                    .HasMaxLength(30)
                    .IsUnicode(false);

            });

            modelBuilder.Entity<MktAuditLog>(entity =>
            {
                entity.HasKey(e => e.AuditId);

                entity.ToTable("mkt_audit_log");

                entity.HasAlternateKey(e => e.ID);

                entity.Property(e => e.AuditId)
                    .HasColumnName("audit_id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ID)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ProcessId)
               .HasColumnName("process_id");

                entity.Property(e => e.ProcessName)
                .HasColumnName("process_name")
                .HasMaxLength(150)
                .IsUnicode(false);

                entity.Property(e => e.RunId)
                .HasColumnName("run_id");

                entity.Property(e => e.UserId)
                 .HasColumnName("user_id");

                entity.Property(e => e.UserName)
                 .HasColumnName("user_name")
                 .HasMaxLength(150)
                 .IsUnicode(false);

                entity.Property(e => e.IncludedIds)
                 .HasColumnName("included_ids")
                 .HasColumnType("text")
                 .IsUnicode(false);


                entity.Property(e => e.ExcludedIds)
                    .HasColumnName("excluded_ids")
                    .HasColumnType("text")
                    .IsUnicode(false);

                entity.Property(e => e.DeletedIds)
                   .HasColumnName("deleted_ids")
                   .HasColumnType("text")
                   .IsUnicode(false);

                entity.Property(e => e.UpdatedOn)
                   .HasColumnName("updated_on")
                   .HasColumnType("datetime");

            });

            modelBuilder.Entity<MktBoxdocInfo>(entity =>
            {
                entity.HasKey(e => e.DocId);
                entity.ToTable("mkt_box_doc_info");

                entity.HasAlternateKey(e => e.ID);

                entity.Property(e => e.DocId)
                    .HasColumnName("doc_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.ID)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Boxid)
               .HasColumnName("box_id");

                entity.Property(e => e.SequenceId)
                .HasColumnName("sequence_id");

                entity.Property(e => e.Etag)
                .HasColumnName("etag")
                .HasMaxLength(500)
                .IsUnicode(false);

               entity.Property(e => e.DocType)
                  .HasColumnName("doc_type")
                  .HasMaxLength(100)
                  .IsUnicode(false);

                entity.Property(e => e.FileName)
               .HasColumnName("file_name")
               .HasMaxLength(500)
               .IsUnicode(true);

                entity.Property(e => e.Description)
               .HasColumnName("description")
               .HasMaxLength(500)
               .IsUnicode(false);

                entity.Property(e => e.FileSize)
               .HasColumnName("file_size");

                entity.Property(e => e.CreatedOn)
                   .HasColumnName("created_on")
                   .HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn)
                   .HasColumnName("modified_on")
                   .HasColumnType("datetime");

                entity.Property(e => e.ContentCreatedOn)
                   .HasColumnName("content_created_on")
                   .HasColumnType("datetime");

                entity.Property(e => e.ContentModifiedOn)
                  .HasColumnName("content_modified_on")
                  .HasColumnType("datetime");

                entity.Property(e => e.CreatedByType)
                .HasColumnName("created_by_type")
                .HasMaxLength(100)
                .IsUnicode(false);

                entity.Property(e => e.CreatedById)
                .HasColumnName("created_by_id")
                .HasMaxLength(150)
                .IsUnicode(false);

                entity.Property(e => e.CreatedByName)
               .HasColumnName("created_by_name")
               .HasMaxLength(150)
               .IsUnicode(false);

                entity.Property(e => e.ModifiedByType)
               .HasColumnName("modified_by_type")
               .HasMaxLength(100)
               .IsUnicode(false);

                entity.Property(e => e.ModifiedById)
               .HasColumnName("modified_by_id")
               .HasMaxLength(150)
               .IsUnicode(false);

                entity.Property(e => e.ModifiedByName)
               .HasColumnName("modified_by_name")
               .HasMaxLength(150)
               .IsUnicode(false);

                entity.Property(e => e.OwnedByType)
               .HasColumnName("owned_by_type")
               .HasMaxLength(100)
               .IsUnicode(false);

                entity.Property(e => e.OwnedById)
                .HasColumnName("owned_by_id")
                .HasMaxLength(100)
                .IsUnicode(false);

                entity.Property(e => e.OwnedByName)
               .HasColumnName("owned_by_name")
               .HasMaxLength(150)
               .IsUnicode(false);

                entity.Property(e => e.ParentType)
               .HasColumnName("parent_type")
               .HasMaxLength(100)
               .IsUnicode(false);


                entity.Property(e => e.ParentId)
               .HasColumnName("parent_id");

                entity.Property(e => e.ParentSequenceId)
               .HasColumnName("parent_sequence_id")
               .HasMaxLength(100)
               .IsUnicode(false);

                entity.Property(e => e.ParentEtag)
               .HasColumnName("parent_etag")
               .HasMaxLength(500)
               .IsUnicode(false);


                entity.Property(e => e.ParentName)
               .HasColumnName("parent_name")
               .HasMaxLength(150)
               .IsUnicode(true);

                entity.Property(e => e.ItemStatus)
               .HasColumnName("item_status");

            });

            modelBuilder.Entity<MktProcessList>(entity =>
            {
                entity.HasKey(e => e.ProcessId);

                entity.ToTable("mkt_process_list");

                entity.HasAlternateKey(e => e.ID);

                entity.Property(e => e.ProcessId)
                    .HasColumnName("process_id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ID)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();


                entity.Property(e => e.ProcessName)
                .HasColumnName("process_name")
                .HasMaxLength(250)
                .IsUnicode(false);

                entity.Property(e => e.ProcessType)
               .HasColumnName("process_type")
               .HasMaxLength(250)
               .IsUnicode(false);

                entity.Property(e => e.ProcessType)
               .HasColumnName("process_type")
               .HasMaxLength(250)
               .IsUnicode(false);

                entity.Property(e => e.schFlag)
               .HasColumnName("sch_flag")
               .HasMaxLength(10)
               .IsUnicode(false);

                entity.Property(e => e.notifyFlag)
              .HasColumnName("notify_flag")
              .HasMaxLength(10)
              .IsUnicode(false);

                entity.Property(e => e.notifyList)
               .HasColumnName("notify_list")
               .HasMaxLength(4000)
               .IsUnicode(false);

                entity.Property(e => e.CreatedById)
                .HasColumnName("created_by_id")
                .HasMaxLength(150)
                .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
               .HasColumnName("created_by")
               .HasMaxLength(250)
               .IsUnicode(false);

                entity.Property(e => e.CreatedOn)
                   .HasColumnName("created_on")
                   .HasColumnType("datetime");

            });

            modelBuilder.Entity<MktProcessRunlog>(entity =>
            {
                entity.HasKey(e => e.RunId);

                entity.ToTable("mkt_process_run_log");

                entity.HasAlternateKey(e => e.ID);

                entity.Property(e => e.RunId)
                    .HasColumnName("run_id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ID)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();


                entity.Property(e => e.ProcessId)
                .HasColumnName("process_id");

                entity.Property(e => e.StartDate)
                 .HasColumnName("start_date")
                 .HasColumnType("datetime");

                entity.Property(e => e.EndDate)
                .HasColumnName("end_date")
                .HasColumnType("datetime");


                entity.Property(e => e.notifyFlag)
                  .HasColumnName("notify_flag")
                  .HasMaxLength(10)
                  .IsUnicode(false);

                entity.Property(e => e.notifyList)
                 .HasColumnName("notify_list")
                 .HasMaxLength(4000)
                 .IsUnicode(false);

                entity.Property(e => e.msgLog)
                    .HasColumnName("msg_log")
                    .HasMaxLength(4000)
                    .IsUnicode(false);

                entity.Property(e => e.userId)
                 .HasColumnName("user_id")
                 .HasMaxLength(150)
                 .IsUnicode(false);

                entity.Property(e => e.userName)
                    .HasColumnName("user_name")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.runStatus)
                   .HasColumnName("run_status")
                   .HasMaxLength(50)
                   .IsUnicode(false);

                entity.Property(e => e.CreatedById)
                    .HasColumnName("created_by_id")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                   .HasColumnName("created_by")
                   .HasMaxLength(250)
                   .IsUnicode(false);

                entity.Property(e => e.CreatedOn)
                   .HasColumnName("created_on")
                   .HasColumnType("datetime");

            });

            modelBuilder.Entity<MktProcessErrorlog>(entity =>
            {
                entity.HasKey(e => e.RunId);

                entity.ToTable("mkt_process_error_log");

                entity.HasAlternateKey(e => e.ID);

                entity.Property(e => e.RunId)
                    .HasColumnName("run_id")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ID)
                    .HasColumnName("id")
                    .ValueGeneratedOnAdd();


                entity.Property(e => e.ProcessId)
                .HasColumnName("process_id");

                entity.Property(e => e.errorCode)
                  .HasColumnName("error_code")
                  .HasMaxLength(500)
                  .IsUnicode(false);

                entity.Property(e => e.errorLog)
                .HasColumnName("error_log")
                .HasMaxLength(4000)
                .IsUnicode(false);

                entity.Property(e => e.CreatedById)
                    .HasColumnName("created_by_id")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                   .HasColumnName("created_by")
                   .HasMaxLength(250)
                   .IsUnicode(false);

                entity.Property(e => e.CreatedOn)
                   .HasColumnName("created_on")
                   .HasColumnType("datetime");

            });

            #region VIEWS
            modelBuilder.Entity<VwCountyRegion>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("vw_country_region");
                entity.Property(v => v.CountryName).HasColumnName("CountryName");
                entity.Property(v => v.CountryGlobalCode).HasColumnName("CountryGlobalCode");
                entity.Property(v => v.RegionName).HasColumnName("RegionName");
                entity.Property(v => v.RegionGlobalCode).HasColumnName("RegionGlobalCode");
                entity.Property(v => v.IsCountry).HasColumnName("IsCountry");
                entity.Property(v => v.SelectedCode).HasColumnName("SelectedCode");
            });

            modelBuilder.Entity<VwUserInfo>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("vw_user_info");
                entity.Property(v => v.UserId).HasColumnName("User_Id");
                entity.Property(v => v.Employeeid).HasColumnName("Employeeid");
                entity.Property(v => v.FirstName).HasColumnName("First_Name");
                entity.Property(v => v.LastName).HasColumnName("Last_Name");
                entity.Property(v => v.Company).HasColumnName("Company");
                entity.Property(v => v.DepartmentName).HasColumnName("Department_Name");
                entity.Property(v => v.DepartmentNo).HasColumnName("Department_No");
                entity.Property(v => v.DisplayName).HasColumnName("Display_Name");
                entity.Property(v => v.PreferContributorId).HasColumnName("prefer_contributor_id");
                entity.Property(v => v.Location).HasColumnName("Location");
                entity.Property(v => v.Country).HasColumnName("Country");
                entity.Property(v => v.Title).HasColumnName("Title");
                entity.Property(v => v.Samaccountname).HasColumnName("Samaccountname");
                entity.Property(v => v.userprincipalname).HasColumnName("userprincipalname");
                entity.Property(v => v.EmailId).HasColumnName("Email_Id");
                entity.Property(v => v.MobileNo).HasColumnName("Mobile_No");
                entity.Property(v => v.Status).HasColumnName("Status");
                entity.Property(v => v.GroupId).HasColumnName("Group_Id");
                entity.Property(v => v.GroupName).HasColumnName("Group_Name");
                entity.Property(v => v.Priority).HasColumnName("Priority");
                entity.Property(v => v.CreatedOn).HasColumnName("Created_On");
                entity.Property(v => v.LogDate).HasColumnName("Log_Date");
            });

            modelBuilder.Entity<VwUserInfoDeveloper>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("vw_user_info_dvlpr");
                entity.Property(v => v.UserId).HasColumnName("User_Id");
                entity.Property(v => v.Employeeid).HasColumnName("Employeeid");
                entity.Property(v => v.FirstName).HasColumnName("First_Name");
                entity.Property(v => v.LastName).HasColumnName("Last_Name");
                entity.Property(v => v.Company).HasColumnName("Company");
                entity.Property(v => v.DepartmentName).HasColumnName("Department_Name");
                entity.Property(v => v.DepartmentNo).HasColumnName("Department_No");
                entity.Property(v => v.DisplayName).HasColumnName("Display_Name");
                entity.Property(v => v.PreferContributorId).HasColumnName("prefer_contributor_id");
                entity.Property(v => v.Location).HasColumnName("Location");
                entity.Property(v => v.Country).HasColumnName("Country");
                entity.Property(v => v.Title).HasColumnName("Title");
                entity.Property(v => v.Samaccountname).HasColumnName("Samaccountname");
                entity.Property(v => v.userprincipalname).HasColumnName("userprincipalname");
                entity.Property(v => v.EmailId).HasColumnName("Email_Id");
                entity.Property(v => v.MobileNo).HasColumnName("Mobile_No");
                entity.Property(v => v.Status).HasColumnName("Status");
                entity.Property(v => v.GroupId).HasColumnName("Group_Id");
                entity.Property(v => v.GroupName).HasColumnName("Group_Name");
                entity.Property(v => v.Priority).HasColumnName("Priority");
                entity.Property(v => v.CreatedOn).HasColumnName("Created_On");
                entity.Property(v => v.LogDate).HasColumnName("Log_Date");
            });

            modelBuilder.Entity<VwContributorGroup>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("vw_contributor_group_access");
                entity.Property(v => v.ContributorId).HasColumnName("ctbid");
                entity.Property(v => v.ContributorName).HasColumnName("ctb_name");
                entity.Property(v => v.Groups).HasColumnName("groups");              
            });

            modelBuilder.Entity<VwContributorGroupDeveloper>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("vw_contributor_group_access_dvlpr");
                entity.Property(v => v.ContributorId).HasColumnName("ctbid");
                entity.Property(v => v.ContributorName).HasColumnName("ctb_name");
                entity.Property(v => v.Groups).HasColumnName("groups");
            });
            #endregion

            modelBuilder.Entity<BoxFolderInfo>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("Id").UseIdentityColumn();
                entity.Property(e => e.BoxFolderId).IsRequired(true).HasMaxLength(30).IsUnicode(false).HasColumnName("box_folder_id");
                entity.Property(e => e.ParentId).IsRequired(true).HasMaxLength(30).IsUnicode(false).HasColumnName("parent_id");
                entity.Property(e => e.FolderName).IsRequired(true).HasColumnName("folder_name").HasMaxLength(150).IsUnicode(false);
                entity.Property(e => e.Parent).IsRequired(true).HasColumnName("parent_name").HasMaxLength(150).IsUnicode(false);

                entity.HasIndex(e => e.BoxFolderId);
                entity.HasIndex(e => e.Parent);
                entity.HasIndex(e => e.FolderName);

            });

            modelBuilder.Entity<MktProjectCode>(entity =>
            {
                entity.ToTable("MktProjectCode");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("Id").UseIdentityColumn();
                entity.Property(e => e.Code).IsRequired(true).HasMaxLength(100).IsUnicode(false).HasColumnName("projectcode_name");
                entity.Property(e => e.Description).IsRequired(true).HasMaxLength(1000).IsUnicode(false).HasColumnName("description");
                entity.Property(e => e.IsDeleted).IsRequired(true).HasColumnName("isDeleted");
                entity.Property(e => e.Status).IsRequired(true).HasColumnName("status");

            });

            modelBuilder.Entity<MktCompanyMaster>().HasNoKey().ToView(null); 

            modelBuilder.Entity<MktLanguages>(entity =>
            {
                entity.HasKey(e => e.LanguageId)
                    .HasName("pk_language_id");

                entity.ToTable("mkt_language");

                entity.Property(e => e.LanguageId)
                    .HasColumnName("language_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CreatedById)
                    .HasColumnName("created_by_id")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn)
                    .HasColumnName("created_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedById)
                    .HasColumnName("deleted_by_id")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.DeletedOn)
                    .HasColumnName("deleted_on")
                    .HasColumnType("datetime");
               
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.LanguageName)
                    .HasColumnName("Language_Name")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.LanguageUid)
                    .HasColumnName("Language_UID")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.IsDeleted).HasColumnName("isdeleted").HasDefaultValueSql("((0))"); ;

                entity.Property(e => e.MXLanguageMap)
                    .HasColumnName("MX_Language_Map")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.RFC_1766)
                    .HasColumnName("RFC_1766")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedById)
                    .HasColumnName("modified_by_id")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedOn)
                    .HasColumnName("modified_on")
                    .HasColumnType("datetime");

                entity.Property(e => e.MXExistingLanguage)
                    .HasColumnName("MX_Existing_Language")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CodePage)
                    .HasColumnName("Code_Page")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.SortOrder)
                .HasColumnName("Sort_Order");

                entity.Property(v => v.Status).HasColumnName("Status").HasDefaultValueSql("((1))"); ;
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
