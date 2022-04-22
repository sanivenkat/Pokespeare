using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace Pokespeare
{
    public class Program
    {
        public static void Main(string[] args)
        {
             Log.Logger = new LoggerConfiguration()
               .Enrich.FromLogContext()
               .MinimumLevel.Debug()
               .WriteTo.ColoredConsole(
                   LogEventLevel.Information,
                   "{NewLine}{Timestamp:HH:mm:ss} [{Level}] ({CorrelationToken}) {Message}{NewLine}{Exception}")
                   .CreateLogger();
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseSerilog()
                .UseStartup<Startup>();
    }
}
