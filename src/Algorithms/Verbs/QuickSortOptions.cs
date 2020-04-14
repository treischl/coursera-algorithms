using Algorithms.Core.DivideAndConquer;
using CommandLine;

namespace Algorithms.Verbs
{
    [Verb("quicksort", HelpText = "Count the number of comparisons made while sorting an array of integers using quicksort")]
    public class QuickSortOptions
    {
        [Option('p', "path", HelpText = "Path to a file with integers separated by linebreaks.")]
        public string Path { get; set; } = string.Empty;

        [Option(
            "pivot-choice",
            Default = PivotChoice.Random,
            HelpText = "How the quicksort routine should choose the pivot to partition about.")]
        public PivotChoice PivotChoice { get; set; } = PivotChoice.Random;
    }
}
