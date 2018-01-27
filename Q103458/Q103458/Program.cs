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
                options.UseInMemoryDatabase("blog_sample");
            });

            IServiceProvider sp = services.BuildServiceProvider();

            var writeDbContext = sp.GetService<MyDbContext>();

            var post = new Post
            {
                Title = "test title",
                Content = "test body",
                PostId = 1
            };
            writeDbContext.Add(post);

            var tag = new Tag
            {
                TagId = 2,
                TagName = "efcore"
            };
            writeDbContext.Add(tag);

            var postTag = new PostTag
            {
                PostId = 1,
                TagId = 2
            };
            writeDbContext.Add(postTag);
            writeDbContext.SaveChanges();

            var readDbContext = sp.GetService<MyDbContext>();
            post = await readDbContext.Posts.FirstOrDefaultAsync();
            Console.WriteLine(post.PostTags.FirstOrDefault().Tag.TagId); //output is 2
        }
    }
}
