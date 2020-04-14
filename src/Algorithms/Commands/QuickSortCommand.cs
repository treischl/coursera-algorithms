using Algorithms.Core.DivideAndConquer;
using Algorithms.Verbs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Algorithms.Commands
{
    public class QuickSortCommand : ICommand<QuickSortOptions>
    {
        private readonly TextWriter _consoleOut;
        private readonly IQuickSorter _quickSorter;

        public QuickSortCommand(TextWriter consoleOut, IQuickSorter inversionCounter)
        {
            _consoleOut = consoleOut;
            _quickSorter = inversionCounter;
        }

        public void Execute(QuickSortOptions options)
        {
            var integers = ReadIntegerArrayFile(options.Path).ToArray();
            var outPath = $"{options.Path}.sorted";

            var comparisons = _quickSorter.CountComparisons(integers.AsSpan(), options.PivotChoice);

            _consoleOut.WriteLine($"Number of comparisons: {comparisons}");
            WriteSortedArrayFile(outPath, integers);
            _consoleOut.WriteLine($"Sorted integer file: {outPath}");
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

        private void WriteSortedArrayFile(string path, Span<int> integers)
        {
            using var stream = new FileStream(path, FileMode.Create, FileAccess.Write);
            using var writer = new StreamWriter(stream);
            foreach (var integer in integers)
            {
                writer.WriteLine(integer);
            }
        }
    }
}
