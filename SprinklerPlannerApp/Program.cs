using Microsoft.Extensions.DependencyInjection;
using SprinklerPlannerApp.Config;
using SprinklerPlannerApp.Runner;

namespace SprinklerPlannerApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            ServiceRegistration.ConfigureServices(services);
            ServiceProvider provider = services.BuildServiceProvider();

            AppRunner runner = provider.GetRequiredService<AppRunner>();
            runner.Run();
        }
    }
}
