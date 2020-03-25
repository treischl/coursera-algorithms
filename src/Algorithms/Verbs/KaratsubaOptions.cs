using CommandLine;

namespace Algorithms.Verbs
{
    [Verb("karatsuba", HelpText = "Perform Karatsuba multiplication on two integers.")]
    public class KaratsubaOptions
    {
        [Value(0, HelpText = "The first integer being multiplied.")]
        public string X { get; set; } = "0";

        [Value(1, HelpText = "The second integer being multiplied.")]
        public string Y { get; set; } = "0";
    }
}
