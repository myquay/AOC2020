using AOC2020.Solvers;
using AOC2020.Solvers.Solutions;
using AOC2020.Solvers.Solutions.Handlers;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AOC2020.Tests
{
    public class SolveAdventDay03CommandTests
    {
        [Fact]
        public async Task CanSolveProblem03Async()
        {
            var dataService = new Mock<IProblemDataService>();

            dataService.Setup(a => a.GetDataForProblemAsync(It.Is<QuestionIds>(s => s == QuestionIds.QuestionDay03)))
                .Returns(Task.FromResult("..##.......\n#...#...#..\n.#....#..#.\n..#.#...#.#\n.#...##..#.\n..#.##.....\n.#.#.#....#\n.#........#\n#.##...#...\n#...##....#\n.#..#...#.#"));

            var solver = new SolveAdventDay03CommandHandler(Mock.Of<ILogger<SolveAdventDay03Command>>(), dataService.Object);

            var result = await solver.Handle(new SolveAdventDay03Command(), CancellationToken.None);

            Assert.Equal("7", result.PartA);
            Assert.Equal("336", result.PartB);
        }

    }
}
