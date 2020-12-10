using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AOC2020.Solvers.Solutions
{
    public class SolveAdventDay06Command : ISolveProblemCommand
    {
        public int Day => 6;

        public string ProblemTitle => "--- Day 6: Custom Customs ---";
    }
}

namespace AOC2020.Solvers.Solutions.Handlers
{
    /// <summary>
    /// Implementation to solve day five
    /// </summary>
    public class SolveAdventDay06CommandHandler : IRequestHandler<SolveAdventDay06Command, ProblemSolution>
    {
        private readonly ILogger log;
        private readonly IProblemDataService dataService;

        public SolveAdventDay06CommandHandler(
            ILogger<SolveAdventDay06Command> log,
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
        public async Task<ProblemSolution> Handle(SolveAdventDay06Command request, CancellationToken cancellationToken)
        {

            var lettersPerGroup = Regex.Split(await dataService.GetDataForProblemAsync(QuestionIds.QuestionDay06), "^(?:\r?\n|\r)+", RegexOptions.Multiline)
                .Select(a => a.Split("\n").Select(b => new string(b.Where(c => char.IsLetterOrDigit(c)).ToArray())).Where(d => !string.IsNullOrEmpty(d))).ToList();

            return new ProblemSolution
            {
                PartA = $"{lettersPerGroup.Sum(a => a.SelectMany(b => b.ToCharArray()).Distinct().Count())}",
                PartB = $"{lettersPerGroup.Select(g => (Passengers: g.Count(), Answers: g.SelectMany(b => b.ToCharArray()).GroupBy(b => b).ToDictionary(a => a.Key, a => a.Count()))).SelectMany(g => g.Answers.Where(a => a.Value == g.Passengers)).Count()}"
            };
        }

    }

}