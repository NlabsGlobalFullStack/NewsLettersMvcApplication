using Microsoft.EntityFrameworkCore;
using Newsletter.Consumer.Models;

namespace Newsletter.Consumer.Context;
public sealed class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("");
    }

    public DbSet<Blog> Blogs { get; set; }
}
