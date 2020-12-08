using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AOC2020.Solvers.Solutions
{
    public class SolveAdventDay05Command : ISolveProblemCommand
    {
        public int Day => 5;

        public string ProblemTitle => "--- Day 5: Binary Boarding ---";
    }
}

namespace AOC2020.Solvers.Solutions.Handlers
{
    /// <summary>
    /// Implementation to solve day five
    /// </summary>
    public class SolveAdventDay05CommandHandler : IRequestHandler<SolveAdventDay05Command, ProblemSolution>
    {
        private readonly ILogger log;
        private readonly IProblemDataService dataService;

        public SolveAdventDay05CommandHandler(
            ILogger<SolveAdventDay05Command> log,
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
        public async Task<ProblemSolution> Handle(SolveAdventDay05Command request, CancellationToken cancellationToken)
        {
            static (int Low, int High) Upper((int Low, int High) a) => ((int)Math.Ceiling((a.High - a.Low) / 2.0 + a.Low), a.High);
            static (int Low, int High) Lower((int Low, int High) a) => (a.Low, (int)Math.Floor(a.High - (a.High - a.Low) / 2.0));

            var seatNumbers = (await dataService.GetDataForProblemAsync(QuestionIds.QuestionDay05)).Split('\n')
                .Select(s => s.Trim())
                .Select(s => new
                {
                    Row = s.Take(7).Aggregate((Low: 0, High: 127), (a, b) =>
                         {
                             a = b switch
                             {
                                 'B' => Upper(a),
                                 'F' => Lower(a),
                                 _ => throw new ArgumentException($"Unexpected character in sequence {b}")
                             };

                             return a;
                         }).High,
                   Column = s.Skip(7).Take(3).Aggregate((Low: 0, High: 7), (a, b) =>
                   {
                       a = b switch
                       {
                           'R' => Upper(a),
                           'L' => Lower(a),
                           _ => throw new ArgumentException($"Unexpected character in sequence {b}")
                       };

                       return a;
                   }).High
                }).Select(s => new
                {
                    s.Row,
                    s.Column,
                    SeatId = s.Row * 8 + s.Column
                }).ToList();

            var seatIds = seatNumbers.Select(s => s.SeatId).OrderBy(s => s);

            return new ProblemSolution
            {
                PartA = $"{seatNumbers.Select(s => s.SeatId).Max()}",
                PartB = $"{seatIds.Zip(seatIds.Skip(1)).First(a => a.Second - a.First > 1).First + 1}",
            };
        }

    }

}