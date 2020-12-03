using System.Threading.Tasks;

namespace AOC2020.Solvers
{
    /// <summary>
    /// Service for retriving problem data
    /// </summary>
    public interface IProblemDataService
    {
        /// <summary>
        /// Get the data for a problem Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<string> GetDataForProblemAsync(QuestionIds id);
    }
}
