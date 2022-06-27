using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.Email;
using Serilog.Sinks.MSSqlServer;

namespace ResearchTHM.API
{
    public class Program
    {

        public static void Main(string[] args)
        {
            Serilog.Debugging.SelfLog.Enable(Console.WriteLine);
            Log.Logger = new LoggerConfiguration()
#if DEBUG
                .WriteTo.Console()
#endif
                .CreateLogger();

            CreateHostBuilder(args).Build().Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {

                    try
                    {
                        if (hostingContext?.HostingEnvironment?.EnvironmentName?.ToLower() != "local")
                        {
                            var builtConfig = config.Build();
                            var secretClient = new SecretClient(
                                new Uri($"https://{builtConfig["AzureKeyVaultName"]}.vault.azure.net/"),
                                new DefaultAzureCredential());
                            config.AddAzureKeyVault(secretClient, new KeyVaultSecretManager());
                        }
                        else
                        {
                            config.AddUserSecrets("85617ed7-db04-448c-9fee-7c5da3616c72");
                        }
                    }
                    catch (Exception ex)
                    {
                        config.AddInMemoryCollection(new Dictionary<string, string> { ["keyvaulterror"] = ex.Message });
                    }

                })
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

    }
}
