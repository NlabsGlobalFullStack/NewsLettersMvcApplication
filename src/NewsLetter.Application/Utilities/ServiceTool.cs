using Microsoft.Extensions.DependencyInjection;

namespace NewsLetter.Application.Utilities;
public static class ServiceTool
{
    public static IServiceProvider? ServiceProvider { get; set; }
    public static IServiceCollection CreateServiceTool(this IServiceCollection services)
    {
        ServiceProvider = services.BuildServiceProvider();

        return services;
    }
}
