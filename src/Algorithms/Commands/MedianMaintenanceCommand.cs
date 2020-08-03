using Algorithms.Core.GraphSearch;
using Algorithms.Verbs;
using System;

namespace Algorithms.Commands
{
    public class MedianMaintenanceCommand : ICommand<MedianMaintenanceOptions>
    {
        private readonly IMedianMaintenance _medianMaintenance;
        private readonly IMedianFileParser _parser;

        public MedianMaintenanceCommand(
            IMedianMaintenance medianMaintenance,
            IMedianFileParser parser
        )
        {
            _medianMaintenance = medianMaintenance;
            _parser = parser;
        }

        public void Execute(MedianMaintenanceOptions options)
        {
            var numbers = _parser.ParseFile(options.File);
            var medianSum = _medianMaintenance.CalculateSumOfMedians(numbers).GetAwaiter().GetResult();

            Console.WriteLine(medianSum);
        }
    }
}
