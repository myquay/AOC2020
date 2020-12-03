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
                QuestionIds.QuestionDayOne => "q01.txt",
                QuestionIds.QuestionDayTwo => "q02.txt",
                _ => throw new ArgumentException($"Data provider does not support problem '{id}'")
            };

            return await File.ReadAllTextAsync($"{AppDomain.CurrentDomain.BaseDirectory}/Data/{questionDataFile}", Encoding.UTF8);
        }
    }
}
