using MediatR;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AOC2020.Solvers.Solutions
{
    public class SolveAdventDay03Command : ISolveProblemCommand
    {
        public int Day => 3;

        public string ProblemTitle => "--- Day 3: Toboggan Trajectory ---";
    }
}

namespace AOC2020.Solvers.Solutions.Handlers
{
    /// <summary>
    /// Implementation to solve day three
    /// </summary>
    public class SolveAdventDay03CommandHandler : IRequestHandler<SolveAdventDay03Command, ProblemSolution>
    {
        private readonly ILogger log;
        private readonly IProblemDataService dataService;

        public SolveAdventDay03CommandHandler(
            ILogger<SolveAdventDay03Command> log,
            IProblemDataService dataService)
        {
            this.log = log;
            this.dataService = dataService;
        }

        /// <summary>
        /// Solver
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ProblemSolution> Handle(SolveAdventDay03Command request, CancellationToken cancellationToken)
        {
            var treeMap = (await dataService.GetDataForProblemAsync(QuestionIds.QuestionDay03)).Split('\n').Select(n => n.Where(a => a == '.' || a == '#').Select(a => a == '.' ? false : true).ToArray()).ToArray();

            var candidateSlopes = new[]
            {
                (Right: 1, Down: 1),
                (Right: 3, Down: 1),
                (Right: 5, Down: 1),
                (Right: 7, Down: 1),
                (Right: 1, Down: 2)
            };

            var resultForPartB = candidateSlopes.Select(c => treeMap.Where((a, i) => (c.Down * i) < treeMap.Length ? treeMap[c.Down * i][i * c.Right % a.Length] : false).Count())
                .Select(s => (long)s)
                .Aggregate((a,b) => a*b);

            return new ProblemSolution
            {
                PartA = $"{treeMap.Where((a, i) => treeMap[i][i * 3 % a.Length]).Count()}",
                PartB = $"{resultForPartB}",
            };
        }

    }

}