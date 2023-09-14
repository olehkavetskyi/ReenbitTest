using Microsoft.Extensions.DependencyInjection;
using ReenbitTest.Interfaces;
using ReenbitTest.Services;

namespace ReenbitTest.Extensions;

public static class ApiServiceResolver
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<IBlobService, BlobService>();
    }
}
