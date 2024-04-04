using GenericRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewsLetter.Domain.Entities;
using NewsLetter.Persistance.Context;
using Scrutor;

namespace NewsLetter.Persistance;
public static class DependencyInjection
{
    public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseInMemoryDatabase("InMemoryBlogDb" ?? "");
        });

        services.AddIdentityCore<AppUser>().AddEntityFrameworkStores<AppDbContext>();

        services.AddScoped<IUnitOfWork>(srv => srv.GetRequiredService<AppDbContext>());

        services.Scan(selector => selector
            .FromAssemblies(
                typeof(DependencyInjection).Assembly)
            .AddClasses(publicOnly: false)
            .UsingRegistrationStrategy(RegistrationStrategy.Skip)
            .AsMatchingInterface()
            .WithScopedLifetime());

        return services;
    }
}
