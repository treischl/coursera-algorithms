using Algorithms.Verbs;
using System;
using System.IO;

namespace Algorithms.Commands
{
    public class CountInversionsCommand : ICommand<CountInversionsOptions>
    {
        private readonly TextWriter _consoleOut;

        public CountInversionsCommand(TextWriter consoleOut)
        {
            _consoleOut = consoleOut;
        }

        public void Execute(CountInversionsOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
