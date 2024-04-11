using GenericRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewsLetter.Domain.Entities;
using NewsLetter.Domain.Repositories;
using NewsLetter.Persistance.Context;
using NewsLetter.Persistance.Repositories;

namespace NewsLetter.Persistance;
public static class DependencyInjection
{
    public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("SqlServer");
            //options.UseInMemoryDatabase("InMemoryBlogDb" ?? "");
            options.UseSqlServer(connectionString);
        });

        services.AddIdentityCore<AppUser>().AddEntityFrameworkStores<AppDbContext>();

        services.AddScoped<IUnitOfWork>(srv => srv.GetRequiredService<AppDbContext>());


        services.AddScoped<IBlogRepository, BlogRepository>();
        services.AddScoped<ISubscribeRepository, SubscribeRepository>();

        //services.Scan(selector => selector
        //    .FromAssemblies(
        //        typeof(DependencyInjection).Assembly)
        //    .AddClasses(publicOnly: false)
        //    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
        //    .AsMatchingInterface()
        //    .WithScopedLifetime());

        return services;
    }
}
