using System;
using System.Linq;
using System.Text;
using System.Net;
using System.Data;
using System.Net.Http;
using System.Collections.ObjectModel;

using Microsoft.AspNet.OData.Extensions;
using Microsoft.OData.Edm;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

using ResearchTHM.Core.Services;
using ResearchTHM.Services;
using ResearchTHM.Core.Models;
using Microsoft.IdentityModel.Tokens;

using Serilog;
using Serilog.Sinks.MSSqlServer;
using SumoLogic.Logging.Serilog.Extensions;
using Serilog.Sinks.Email;

namespace ResearchTHM.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Log.Logger.Information("Starting to configure services");
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecretKey"])),
                    ClockSkew = TimeSpan.Zero // Override the default clock skew of 5 mins
                };
                // services.AddCors();
            });

            Log.Logger = ConfigureLogger(Configuration, 
                environment: Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT").ToLower(),
                dbConnectionString: Configuration.GetConnectionString("ResearchMKT"),
                sumoLogicEndpoint: Configuration.GetValue<string>("sumoLogicEndpoint")
                );

            services.AddSingleton(Configuration);
            services.AddCors();
            services.AddAutoMapper(typeof(Startup));
            services.AddMemoryCache();
            services.AddSingleton(Log.Logger);

            services.AddDbContext<ResearchMktContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ResearchMKT")));
            
            services.AddHttpClient("trkd", c =>
            {
                c.DefaultRequestHeaders.Add("X-Trkd-Auth-ApplicationID", Configuration.GetValue<string>("trkdCredentials:ApplicationID"));
                
            });
            //.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler{AllowAutoRedirect = false});
            //AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip

            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings
                                .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddScoped<IBoxUtilService, BoxUtilService>();
            services.AddScoped<IMktGroupService, GroupService>();
            services.AddScoped<IMktGroupAccessService, GroupAccessService>();
            services.AddScoped<IMktUserService, UserService>();
            services.AddScoped<IMktContributorService, ContributorService>();
            services.AddScoped<IMktUserGroupAccessService, UserGroupAccessService>();
            services.AddScoped<IMktSavedSearchService, SavedSearchService>();
            services.AddScoped<IMktApiConfigService, ApiConfigService>();
            services.AddScoped<IMktDownloadHistService, DownloadHistService>();
            services.AddScoped<IMktUserActivityService, MktUserActivityService>();
            services.AddScoped<IMktUsageLogService, UsageLogService>();
            services.AddScoped<IMktCtbAuditLogService, CtbAuditLogService>();
            services.AddScoped<IMktBoxDocInfoService, BoxDocInfoService>();
            services.AddScoped<ISearchService, SearchService>();
            services.AddScoped<IMktProcessListService, ProcessListService>();

            //services.AddControllers();
            services.AddOData();

            Log.Logger.Information("Service config complete");

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.EnvironmentName.ToLower() == "other")
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().WithExposedHeaders("Content-Disposition"));

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.EnableDependencyInjection();
                endpoints.Select().Filter().OrderBy().Count().MaxTop(100);
            });

        }

        private ILogger ConfigureLogger(IConfiguration configuration, string environment, string dbConnectionString = null, string sumoLogicEndpoint = null)
        {
            var config = new LoggerConfiguration()
                            .ReadFrom.Configuration(configuration)
                            //.WriteTo.File("", restrictedToMinimumLevel:Serilog.Events.LogEventLevel.Warning)
                            .MinimumLevel.Information();

            if (dbConnectionString != null)
            {
                var sinkOptions = new MSSqlServerSinkOptions()
                {
                    TableName = "Logs"
                };

                var columnOption = new ColumnOptions();
                columnOption.AdditionalColumns = new Collection<SqlColumn> { new SqlColumn { DataType = SqlDbType.VarChar, ColumnName = "ControllerName" } };
                config = config.WriteTo.MSSqlServer(dbConnectionString, sinkOptions: sinkOptions, columnOptions: columnOption);
            }

            

            if (!String.IsNullOrEmpty(sumoLogicEndpoint))
            {
                config = config.WriteTo.BufferedSumoLogic(
                        new Uri(sumoLogicEndpoint),
                        sourceName: "ResearchAPI",
                        sourceCategory: "APILogs",
                        sourceHost: "SerilogBufferedSink",
                        connectionTimeout: 30000
                        //retryInterval: 5000,
                        //messagesPerRequest: 1,
                        //maxFlushInterval: 10000,
                        //flushingAccuracy: 250,
                        //maxQueueSizeBytes: 500000
                        );
            }

            var emailConfig = new EmailConnectionInfo
            {
                FromEmail = configuration["mailCredentials:FromEmail"],
                ToEmail = configuration["mailCredentials:ToEmail"],
                MailServer = configuration["mailCredentials:MailServer"],
                EmailSubject = "Thomson Exception Mail from Backend Solution (" + Environment.MachineName + ")"
            };

            if (environment == "other")
            {
                emailConfig = new EmailConnectionInfo
                {
                    FromEmail = configuration["mailCredentials:FromEmail"],
                    ToEmail = configuration["mailCredentials:ToEmail"],
                    MailServer = configuration["mailCredentials:MailServer"],
                    NetworkCredentials = new NetworkCredential
                    {
                        UserName = configuration["mailCredentials:UserName"],
                        Password = configuration["mailCredentials:Password"],
                    },
                    EnableSsl = true,
                    Port = 465,
                    EmailSubject = "Thomson Exception Mail from Backend Solution (" + Environment.MachineName + ")"
                };
            }

            config = config.WriteTo.Email(
                            connectionInfo: emailConfig,
                            outputTemplate: "{Message}{NewLine}{NewLine}Time: {Timestamp:HH:mm:ss}{NewLine}Class: {SourceContext}{NewLine}{NewLine}{Exception}",
                            batchPostingLimit: 10,
                            restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error
                        );
            return config.CreateLogger();
        }

    }
}

