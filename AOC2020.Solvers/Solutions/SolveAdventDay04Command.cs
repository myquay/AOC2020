using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AOC2020.Solvers.Solutions
{
    public class SolveAdventDay04Command : ISolveProblemCommand
    {
        public int Day => 4;

        public string ProblemTitle => "--- Day 4: Passport Processing ---";
    }
}

namespace AOC2020.Solvers.Solutions.Handlers
{
    /// <summary>
    /// Implementation to solve day four
    /// </summary>
    public class SolveAdventDay04CommandHandler : IRequestHandler<SolveAdventDay04Command, ProblemSolution>
    {
        private readonly ILogger log;
        private readonly IProblemDataService dataService;

        public SolveAdventDay04CommandHandler(
            ILogger<SolveAdventDay04Command> log,
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
        public async Task<ProblemSolution> Handle(SolveAdventDay04Command request, CancellationToken cancellationToken)
        {
            var requiredKeys = new[] {
                new ValidationParameter{
                    Key = "byr",
                    Validator = s => Regex.IsMatch(s, @"^\d{4}$") && int.Parse(s) >= 1920 && int.Parse(s) <= 2002
                },
                new ValidationParameter{
                    Key = "iyr",
                    Validator = s => Regex.IsMatch(s, @"^\d{4}$") && int.Parse(s) >= 2010 && int.Parse(s) <= 2020
                },
                new ValidationParameter{
                    Key = "eyr",
                    Validator = s => Regex.IsMatch(s, @"^\d{4}$") && int.Parse(s) >= 2020 && int.Parse(s) <= 2030
                },
                new ValidationParameter{
                    Key = "hcl",
                    Validator = s => Regex.IsMatch(s, @"^#[\da-f]{6}$")
                },
                new ValidationParameter{
                    Key = "ecl",
                    Validator = s => Regex.IsMatch(s, @"^(amb|blu|brn|gry|grn|hzl|oth)$")
                },
                new ValidationParameter{
                    Key = "pid",
                    Validator = s => Regex.IsMatch(s, @"^[\d]{9}$")
                },
                new ValidationParameter{
                    Key = "hgt",
                    Validator = s => {
                        var inch = Regex.Match(s, @"^([\d]+)in$");
                        var cm = Regex.Match(s, @"^([\d]+)cm$");

                        if(cm.Success)
                            return int.Parse(cm.Groups[1].Value) >= 150 && int.Parse(cm.Groups[1].Value) <= 193;

                        if(inch.Success)
                            return int.Parse(inch.Groups[1].Value) >= 59 && int.Parse(inch.Groups[1].Value) <= 76;

                        return false;
                }
            }
            };

            var passports = Regex.Split(await dataService.GetDataForProblemAsync(QuestionIds.QuestionDay04), "^(?:\r?\n|\r)+", RegexOptions.Multiline).Select(p => Regex.Split(p, @"\s+").Where(s => !string.IsNullOrWhiteSpace(s)).ToDictionary(k => k.Split(":")[0], k => k.Split(":")[1]));

            return new ProblemSolution
            {
                PartA = $"{passports.Where(a => requiredKeys.All(k => a.ContainsKey(k.Key))).Count()}",
                PartB = $"{passports.Where(a => requiredKeys.All(k => a.ContainsKey(k.Key) && k.Validator(a[k.Key]))).Count()}",
            };
        }

        public class ValidationParameter
        {
            public string Key { get; set; }

            public Func<string, bool> Validator { get; set; }
        }

    }

}