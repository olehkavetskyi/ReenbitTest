using EmailNotificationFunc.Interfaces;
using EmailNotificationFunc.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(s =>
    {
        s.AddScoped<IEmailNotificationFunctionService, EmailNotificationFunctionService>();
    })
    .Build();


host.Run();
