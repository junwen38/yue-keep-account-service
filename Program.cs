using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using YueKeepAccountService.Db;

namespace YueKeepAccountService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var rootCommand = new RootCommand() {
                new Option<string>(
                    "--db-host",
                    getDefaultValue: ()=>Environment.GetEnvironmentVariable("AC_DB_HOST")
                ),
                new Option<string>(
                    "--db-name",
                    getDefaultValue: ()=>Environment.GetEnvironmentVariable("AC_DB_NAME")
                ),
                new Option<string>(
                    "--db-username",
                    getDefaultValue: ()=>Environment.GetEnvironmentVariable("AC_DB_USERNAME")
                ),
                new Option<string>(
                    "--db-password",
                    getDefaultValue: ()=>Environment.GetEnvironmentVariable("AC_DB_PASSWORD")
                )
            };
            rootCommand.Handler = CommandHandler.Create<string, string, string, string>((dbHost, dbName, dbUsername, dbPassword)=>{
                AccountBookDb.DbHost = dbHost;
                AccountBookDb.DbName = dbName;
                AccountBookDb.DbUsername = dbUsername;
                AccountBookDb.DbPassword = dbPassword;
                CreateHostBuilder(args).Build().Run();
            });
            rootCommand.InvokeAsync(args);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls("http://0.0.0.0:5000");
                    webBuilder.UseStartup<Startup>();
                });
    }
}
