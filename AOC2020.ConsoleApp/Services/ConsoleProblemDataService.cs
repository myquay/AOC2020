using AOC2020.Solvers;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AOC2020.ConsoleApp.Services
{
    /// <summary>
    /// Data provider for the console application
    /// </summary>
    public class ConsoleProblemDataService : IProblemDataService
    {
        /// <summary>
        /// Get the data for each question
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<string> GetDataForProblemAsync(QuestionIds id)
        {
            var questionDataFile = id switch
            {
                QuestionIds.QuestionDay01 => "q01.txt",
                QuestionIds.QuestionDay02 => "q02.txt",
                QuestionIds.QuestionDay03 => "q03.txt",
                QuestionIds.QuestionDay04 => "q04.txt",
                _ => throw new ArgumentException($"Data provider does not support problem '{id}'")
            };

            return await File.ReadAllTextAsync($"{AppDomain.CurrentDomain.BaseDirectory}/Data/{questionDataFile}", Encoding.UTF8);
        }
    }
}
