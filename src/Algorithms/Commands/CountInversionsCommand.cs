using Algorithms.Core.DivideAndConquer;
using Algorithms.Verbs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Algorithms.Commands
{
    public class CountInversionsCommand : ICommand<CountInversionsOptions>
    {
        private readonly TextWriter _consoleOut;
        private readonly IInversionCounter _inversionCounter;

        public CountInversionsCommand(TextWriter consoleOut, IInversionCounter inversionCounter)
        {
            _consoleOut = consoleOut;
            _inversionCounter = inversionCounter;
        }

        public void Execute(CountInversionsOptions options)
        {
            var integers = (options.Integers.Any()
                ? options.Integers
                : ReadIntegerArrayFile(options.Path)).ToArray().AsSpan();

            var inversions = _inversionCounter.CountInversions(integers);

            _consoleOut.WriteLine(inversions);
        }

        private IEnumerable<int> ReadIntegerArrayFile(string path)
        {
            using var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            using var reader = new StreamReader(stream);
            string? line = null;
            while ((line = reader.ReadLine()) != null)
            {
                yield return int.Parse(line);
            }
        }
    }
}
