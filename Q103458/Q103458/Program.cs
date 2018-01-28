using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Q103458
{
    class Program
    {
        //Answer to https://q.cnblogs.com/q/103458/
        static async Task Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            services.AddDbContext<MyDbContext>(options =>
            {
                //options.UseSqlServer("server=.;database=blog_sample;Integrated Security=true");
                options.UseInMemoryDatabase("blog_sample");
            });

            IServiceProvider sp = services.BuildServiceProvider();

            var writeDbContext = sp.GetService<MyDbContext>();

            var tag = new Tag
            {
                TagName = "efcore"
            };

            var post = new Post
            {
                Title = "test title",
                Content = "test body"
            };

            var postTag = new PostTag
            {
                Tag = tag,
                Post = post
            };

            writeDbContext.Add(postTag);
            writeDbContext.SaveChanges();

            var readDbContext = sp.GetService<MyDbContext>();
            var queryPost = await readDbContext.Posts
                .Include(p => p.PostTags)
                .ThenInclude(pt => pt.Tag)
                .FirstOrDefaultAsync();
            Console.WriteLine(queryPost.PostTags[0].Tag.TagName);
        }
    }
}
