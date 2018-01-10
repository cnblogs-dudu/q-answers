using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Q102939
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
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use(async (context, next) =>
            {
                using (var ms = new MemoryStream())
                {
                    var orgBodyStream = context.Response.Body;
                    context.Response.Body = ms;

                    await next();

                    using (var sr = new StreamReader(ms))
                    {
                        ms.Seek(0, SeekOrigin.Begin);
                        Console.WriteLine("Read Response.Body:\n" + sr.ReadToEnd());

                        ms.Seek(0, SeekOrigin.Begin);
                        await ms.CopyToAsync(orgBodyStream);
                        context.Response.Body = orgBodyStream;
                    }
                }
            });

            app.UseMvc();
        }
    }
}
