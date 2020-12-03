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
    public class SolveAdventDayTwoCommandTests
    {
        [Fact]
        public async Task CanSolveProblem02Async()
        {
            var dataService = new Mock<IProblemDataService>();

            dataService.Setup(a => a.GetDataForProblemAsync(It.Is<QuestionIds>(s => s == QuestionIds.QuestionDayTwo)))
                .Returns(Task.FromResult("1-3 a: abcde\n1-3 b: cdefg\n2-9 c: ccccccccc"));

            var solver = new SolveAdventDayTwoCommandHandler(Mock.Of<ILogger<SolveAdventDayTwoCommand>>(), dataService.Object);

            var result = await solver.Handle(new SolveAdventDayTwoCommand(), CancellationToken.None);

            Assert.Equal("2", result.PartA);
            Assert.Equal("1", result.PartB);
        }

    }
}
