using System.Reflection;
using System.Threading.Tasks;
using AOC2020.ConsoleApp.Services;
using AOC2020.Solvers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AOC2020.ConsoleApp
{
    class Program
    {
        static Task Main(string[] args) =>
           CreateHostBuilder(args).Build().RunAsync();

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services.AddHostedService<SolverApplication>()
                            .AddMediatR(Assembly.GetAssembly(typeof(ISolveProblemCommand)))
                            .AddLogging(configure => configure.AddConsole())
                            .AddSingleton<IProblemDataService, ConsoleProblemDataService>())
           ;
    }
}
