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
    public class SolveAdventDayThreeCommandTests
    {
        [Fact]
        public async Task CanSolveProblem03Async()
        {
            var dataService = new Mock<IProblemDataService>();

            dataService.Setup(a => a.GetDataForProblemAsync(It.Is<QuestionIds>(s => s == QuestionIds.QuestionDayThree)))
                .Returns(Task.FromResult("..##.......\n#...#...#..\n.#....#..#.\n..#.#...#.#\n.#...##..#.\n..#.##.....\n.#.#.#....#\n.#........#\n#.##...#...\n#...##....#\n.#..#...#.#"));

            var solver = new SolveAdventDayThreeCommandHandler(Mock.Of<ILogger<SolveAdventDayThreeCommand>>(), dataService.Object);

            var result = await solver.Handle(new SolveAdventDayThreeCommand(), CancellationToken.None);

            Assert.Equal("7", result.PartA);
            Assert.Equal("336", result.PartB);
        }

    }
}
