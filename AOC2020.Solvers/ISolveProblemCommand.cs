using MediatR;

namespace AOC2020.Solvers
{
    /// <summary>
    /// Solve problem command
    /// </summary>
    public interface ISolveProblemCommand : IRequest<ProblemSolution>
    {
        /// <summary>
        /// The day this command solves
        /// </summary>
        int Day { get; }

        /// <summary>
        /// Problem title
        /// </summary>
        string ProblemTitle { get; }
    }

    /// <summary>
    /// The problem solution
    /// </summary>
    public class ProblemSolution
    {
        public string PartA { get; set; }

        public string PartB { get; set; }
    }
}
