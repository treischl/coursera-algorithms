using Algorithms.Core.GraphSearch;
using Algorithms.Verbs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Algorithms.Commands
{
    public class TwoSumCommand : ICommand<TwoSumOptions>
    {
        private readonly ITwoSum _twoSum;
        private readonly ITwoSumFileParser _parser;

        public TwoSumCommand(
            ITwoSum twoSum,
            ITwoSumFileParser parser
        )
        {
            _twoSum = twoSum;
            _parser = parser;
        }

        public void Execute(TwoSumOptions options)
        {
            var numbersMap = GetNumbers(options.File).GetAwaiter().GetResult();
            var targetCount = options.End - options.Start + 1;
            var targets = new long[targetCount].Select((_, index) => index + options.Start);
            foreach (var distinctSum in _twoSum.GetDistinctSums(numbersMap, targets))
            {
                Console.WriteLine(distinctSum);
            }
        }

        private async Task<IDictionary<long, long>> GetNumbers(string inputFile)
        {
            var numbers = new Dictionary<long, long>();

            await foreach (var number in _parser.ParseFile(inputFile))
            {
                numbers[number] = number;
            }

            return numbers;
        }
    }
}
