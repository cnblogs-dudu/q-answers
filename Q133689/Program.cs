using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Moq;

namespace Q133689
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var requestPath = "/cmt/p/14408628.html";

            var routeTemplate = "/{blogApp}/{postType}/{idOrSlug}.html";
            var routeHandler = new RouteHandler((context) => null);

            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<ILoggerFactory>(NullLoggerFactory.Instance);
            services.AddOptions<RouteOptions>();
            var sp = services.BuildServiceProvider();

            var routeOptions = sp.GetRequiredService<IOptions<RouteOptions>>();
            var inlineConstraintResolver = new DefaultInlineConstraintResolver(routeOptions, sp);
            var route = new Route(
                routeHandler,
                routeTemplate,
                defaults: null,
                constraints: new RouteValueDictionary(),
                dataTokens: null,
                inlineConstraintResolver: inlineConstraintResolver);

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Path = new PathString(requestPath);
            httpContext.RequestServices = sp;

            var routeContext = new RouteContext(httpContext);
            await route.RouteAsync(routeContext);

            Console.WriteLine("blogApp: " + routeContext.RouteData.Values["blogApp"]);
            Console.WriteLine("postType: " + routeContext.RouteData.Values["postType"]);
            Console.WriteLine("idOrSlug: " + routeContext.RouteData.Values["idOrSlug"]);
        }
    }
}
