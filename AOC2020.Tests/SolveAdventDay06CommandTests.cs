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
    public class SolveAdventDay06CommandTests
    {

        private const string data =
            @"abc

a
b
c

ab
ac

a
a
a
a

b";

        [Fact]
        public async Task CanSolveProblem06Async()
        {
            var dataService = new Mock<IProblemDataService>();

            dataService.Setup(a => a.GetDataForProblemAsync(It.Is<QuestionIds>(s => s == QuestionIds.QuestionDay06)))
                .Returns(Task.FromResult(data));

            var solver = new SolveAdventDay06CommandHandler(Mock.Of<ILogger<SolveAdventDay06Command>>(), dataService.Object);

            var result = await solver.Handle(new SolveAdventDay06Command(), CancellationToken.None);

            Assert.Equal("11", result.PartA);
            Assert.Equal("6", result.PartB);
        }

    }
}
