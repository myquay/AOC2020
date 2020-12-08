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
    public class SolveAdventDay05CommandTests
    {

        private const string data =
            @"FBFBBFFRLR
BFFFBBFRRR
FFFBBBFRRR
BBFFBBFRLL";

        [Fact]
        public async Task CanSolveProblem04ParkAAsync()
        {
            var dataService = new Mock<IProblemDataService>();

            dataService.Setup(a => a.GetDataForProblemAsync(It.Is<QuestionIds>(s => s == QuestionIds.QuestionDay05)))
                .Returns(Task.FromResult(data));

            var solver = new SolveAdventDay05CommandHandler(Mock.Of<ILogger<SolveAdventDay05Command>>(), dataService.Object);

            var result = await solver.Handle(new SolveAdventDay05Command(), CancellationToken.None);

            Assert.Equal("820", result.PartA);
            //Assert.Equal("NO SAMPLE PROVIDED", result.PartB);
        }

    }
}
