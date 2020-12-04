using AOC2020.Solvers;
using MediatR;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace AOC2020.ConsoleApp
{
    public class SolverApplication : IHostedService
    {

        private readonly IMediator mediator;


        public SolverApplication(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var allSolvers = Assembly.GetAssembly(typeof(ISolveProblemCommand)).GetTypes()
                .Where(x => x.GetInterfaces().Contains(typeof(ISolveProblemCommand)))
                .Select(x => Activator.CreateInstance(x) as ISolveProblemCommand)
                .ToDictionary(x => x.Day, x => x);

            Console.WriteLine("Advent of Code 2018");
            Console.WriteLine("------------------------");
            Console.WriteLine("Enter a day (1-25) or q to quit");
            Console.WriteLine("========================");
            Console.WriteLine();

            while (true)
            {
                Console.WriteLine();
                var command = Console.ReadLine();
                Console.WriteLine();

                switch(command)
                {
                    case  var o when int.TryParse(o, out int day):
                        if (allSolvers.ContainsKey(day))
                        {
                            var solution = await mediator.Send(allSolvers[day]);
                            Console.WriteLine(allSolvers[day].ProblemTitle);
                            Console.WriteLine($"Result: {solution.PartA}, {solution.PartB}");
                        }
                        else
                        {
                            Console.WriteLine("No solution for that day");
                        }
                        break;
                    case "q":
                        return;
                    default:
                        Console.WriteLine("Unknown command");
                        break;
                };

            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
