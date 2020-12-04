
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AOC2020.Solvers.Solutions
{
    public class SolveAdventDay02Command : ISolveProblemCommand
    {
        public int Day => 2;

        public string ProblemTitle => "--- Day 2: Password Philosophy ---";
    }
}

namespace AOC2020.Solvers.Solutions.Handlers
{
    /// <summary>
    /// Implementation to solve day one
    /// </summary>
    public class SolveAdventDay02CommandHandler : IRequestHandler<SolveAdventDay02Command, ProblemSolution>
    {
        private readonly ILogger log;
        private readonly IProblemDataService dataService;

        public SolveAdventDay02CommandHandler(
            ILogger<SolveAdventDay02Command> log,
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
        public async Task<ProblemSolution> Handle(SolveAdventDay02Command request, CancellationToken cancellationToken)
        {
            var data = (await dataService.GetDataForProblemAsync(QuestionIds.QuestionDay02)).Split('\n')
                .Select(a =>
                {
                    var match = Regex.Match(a, @"(\d*)[\-]{1}(\d*)\s{1}([a-z]){1}[\:\s]{2}([a-z]*)");

                    return new
                    {
                        Upper = int.Parse(match.Groups[2].Value),
                        Lower = int.Parse(match.Groups[1].Value),
                        MatchCharacter = match.Groups[3].Value,
                        FirstCharacter = match.Groups[4].Value.Substring(int.Parse(match.Groups[1].Value) - 1, 1),
                        SecondCharacter = match.Groups[4].Value.Substring(int.Parse(match.Groups[2].Value) - 1, 1),
                        RegexPartA = $"^([^{ match.Groups[3].Value}]*[{ match.Groups[3].Value}][^{ match.Groups[3].Value}]*){{{match.Groups[1].Value},{match.Groups[2].Value}}}$",
                        Password = match.Groups[4].Value
                    };
                }).ToArray();

            return new ProblemSolution
            {
                PartA = $"{data.Count(a => Regex.IsMatch(a.Password, a.RegexPartA))}",
                PartB = $"{data.Where(a => a.FirstCharacter != a.SecondCharacter).Count(a => a.MatchCharacter == a.FirstCharacter || a.MatchCharacter == a.SecondCharacter)}",
            };
        }
    }

}