using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AOC2020.Solvers.Solutions
{
    public class SolveAdventDayOneCommand : ISolveProblemCommand
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
    public class SolveAdventDayOneCommandHandler : IRequestHandler<SolveAdventDayOneCommand, ProblemSolution>
    {
        private readonly ILogger log;
        private readonly IProblemDataService dataService;

        public SolveAdventDayOneCommandHandler(
            ILogger<SolveAdventDayOneCommand> log,
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
        public async Task<ProblemSolution> Handle(SolveAdventDayOneCommand request, CancellationToken cancellationToken)
        {
            var data = (await dataService.GetDataForProblemAsync(QuestionIds.QuestionDayOne)).Split('\n').Select(a => int.Parse(a));

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