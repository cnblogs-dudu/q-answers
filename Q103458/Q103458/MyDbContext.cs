using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Q103458
{
    public class MyDbContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostTag>()
                .HasKey(t => new { t.PostId, t.TagId });

            modelBuilder.Entity<PostTag>()
                .HasOne(pt => pt.Post)
                .WithMany(p => p.PostTags)
                .HasForeignKey(pt => pt.PostId);

            modelBuilder.Entity<PostTag>()
                .HasOne(pt => pt.Tag)
                .WithMany(t => t.PostTags)
                .HasForeignKey(pt => pt.TagId);
        }
    }
}
