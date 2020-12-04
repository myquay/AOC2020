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
    public class SolveAdventDay01CommandTests
    {
        [Fact]
        public async Task CanSolveProblem01Async()
        {
            var dataService = new Mock<IProblemDataService>();

            dataService.Setup(a => a.GetDataForProblemAsync(It.Is<QuestionIds>(s => s == QuestionIds.QuestionDay01)))
                .Returns(Task.FromResult("1721\n979\n366\n299\n675\n1456"));

            var solver = new SolveAdventDay01CommandHandler(Mock.Of<ILogger<SolveAdventDay01Command>>(), dataService.Object);

            var result = await solver.Handle(new SolveAdventDay01Command(), CancellationToken.None);

            Assert.Equal("514579", result.PartA);
            Assert.Equal("241861950", result.PartB);
        }

    }
}
