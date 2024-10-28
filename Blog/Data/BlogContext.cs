using Blog.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace Blog.Data
{

    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options)
        : base(options) // Przekazanie opcji do bazy danych
        {
        }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Comment> Comments { get; set; }

        
    }


}

