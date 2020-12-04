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
    public class SolveAdventDay02CommandTests
    {
        [Fact]
        public async Task CanSolveProblem02Async()
        {
            var dataService = new Mock<IProblemDataService>();

            dataService.Setup(a => a.GetDataForProblemAsync(It.Is<QuestionIds>(s => s == QuestionIds.QuestionDay02)))
                .Returns(Task.FromResult("1-3 a: abcde\n1-3 b: cdefg\n2-9 c: ccccccccc"));

            var solver = new SolveAdventDay02CommandHandler(Mock.Of<ILogger<SolveAdventDay02Command>>(), dataService.Object);

            var result = await solver.Handle(new SolveAdventDay02Command(), CancellationToken.None);

            Assert.Equal("2", result.PartA);
            Assert.Equal("1", result.PartB);
        }

    }
}
