using Microsoft.EntityFrameworkCore;
using Newsletter.Consumer.Models;

namespace Newsletter.Consumer.Context;
public sealed class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=NLABS;Initial Catalog=NewsLettersDb;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
    }

    public DbSet<Blog> Blogs { get; set; }
}
