using Algorithms.Core.GraphSearch;
using Algorithms.Verbs;
using System;
using System.Linq;

namespace Algorithms.Commands
{
    public class KosarajuCommand : ICommand<KosarajuOptions>
    {
        private readonly IKosaraju _kosaraju;
        private readonly IKosarajuFileParser _parser;

        public KosarajuCommand(
            IKosaraju kosaraju,
            IKosarajuFileParser parser)
        {
            _kosaraju = kosaraju;
            _parser = parser;
        }

        public void Execute(KosarajuOptions options)
        {
            var inputGraph = _parser.ParseFile(options.File);
            var output = _kosaraju.GetSccGroupSizes(inputGraph);

            var results = (output) switch
            {
                var _ when options.ResultsCount <= 0 => output,
                var _ when (output.Length < options.ResultsCount) =>
                    output.Concat(new int[options.ResultsCount - output.Length]),
                var _ when (output.Length > options.ResultsCount) =>
                    output.Take(options.ResultsCount),
                _ => output,
            };

            foreach (var result in results)
            {
                Console.WriteLine(result);
            }
        }
    }
}
