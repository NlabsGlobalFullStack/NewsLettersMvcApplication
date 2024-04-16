using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewsLetter.Application.Services;
using NewsLetter.Domain.Entities;

namespace NewsLetter.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        //FLuentEmail
        services.AddFluentEmail("info@admin.com").AddSmtpSender("localhost", 2525);
        //FLuentEmail

        //BlogBackgroundService
        services.AddHostedService<BlogBackgroundService>();
        //BlogBackgroundService

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssemblies(
                typeof(DependencyInjection).Assembly,
                          typeof(Blog).Assembly
                );
        });

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }
}
