using System;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Microsoft.AspNetCore.Hosting;

namespace Q92475
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console(outputTemplate: 
                "{Timestamp:HH:mm:ss} [{Level}] {SourceContext} {NewLine}{Message}{NewLine}{Exception}")
                .CreateLogger();

            var webHost = WebHost.CreateDefaultBuilder(args)
                .Configure(_ => { })
                .UseSerilog()                
                .Build();

            var logger = webHost.Services.GetService<ILoggerFactory>().CreateLogger<Program>();
            logger.LogError("an error occured");
        }
    }
}
