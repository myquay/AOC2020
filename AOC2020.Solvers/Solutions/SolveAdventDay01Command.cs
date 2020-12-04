using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AOC2020.Solvers.Solutions
{
    public class SolveAdventDay01Command : ISolveProblemCommand
    {
        public int Day => 1;

        public string ProblemTitle => "--- Day 1: Report Repair ---";
    }
}

namespace AOC2020.Solvers.Solutions.Handlers
{
    /// <summary>
    /// Implementation to solve day one
    /// </summary>
    public class SolveAdventDay01CommandHandler : IRequestHandler<SolveAdventDay01Command, ProblemSolution>
    {
        private readonly ILogger log;
        private readonly IProblemDataService dataService;

        public SolveAdventDay01CommandHandler(
            ILogger<SolveAdventDay01Command> log,
            IProblemDataService dataService)
        {
            this.log = log;
            this.dataService = dataService;
        }

        /// <summary>
        /// Day one solver
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ProblemSolution> Handle(SolveAdventDay01Command request, CancellationToken cancellationToken)
        {
            var data = (await dataService.GetDataForProblemAsync(QuestionIds.QuestionDay01)).Split('\n').Select(a => int.Parse(a));

            var datasetA = data.SelectMany(a => data, (a, b) => new { A = a, B = b });
            var datasetB = datasetA.SelectMany(a => data, (a, b) => new { a.A, a.B, C = b });


            var targetA = datasetA.Where(entity => entity.A + entity.B == 2020)
                .First();
            var targetB = datasetB.Where(entity => entity.A + entity.B + entity.C == 2020)
                .First();

            return new ProblemSolution
            {
                PartA = $"{targetA.A * targetA.B}",
                PartB = $"{targetB.A * targetB.B * targetB.C}"
            };
        }
    }

}