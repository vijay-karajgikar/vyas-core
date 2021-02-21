using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace VyasApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
				.ConfigureAppConfiguration((hostBuilderContext, configBuilder) => 
				{
					configBuilder.Sources.Clear();

					var currentDirectory = string.Empty;
					var homeDirectory = Environment.GetEnvironmentVariable("HOME");
					if (!string.IsNullOrEmpty(homeDirectory))
					{
						if (File.Exists(Path.Join(homeDirectory, "vyaspi/config", "appsettings.json")))
						{
							currentDirectory = Path.Join(homeDirectory, "vyasapi");
						}
					}
					if (string.IsNullOrEmpty(currentDirectory)) currentDirectory = Directory.GetCurrentDirectory();
					var configDirectory = Path.Combine(currentDirectory, "config");
					configBuilder.SetBasePath(configDirectory);
					configBuilder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
					configBuilder.AddEnvironmentVariables();
					configBuilder.Build();
				})
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
						.ConfigureKestrel(serverOptions => 
						{
							serverOptions.ListenAnyIP(46789);
							serverOptions.ConfigureHttpsDefaults(x => x.SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13);
							serverOptions.ConfigureEndpointDefaults(listenOptions => listenOptions.UseConnectionLogging());
						})					
						.UseStartup<Startup>();
                });
    }
}
