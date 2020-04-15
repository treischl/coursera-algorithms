using Algorithms.Core;
using Algorithms.Core.DivideAndConquer;
using Algorithms.Verbs;
using System.IO;
using System.Linq;

namespace Algorithms.Commands
{
    public class KaratsubaCommand : ICommand<KaratsubaOptions>
    {
        private readonly TextWriter _consoleOut;
        private readonly IKaratsuba _karatsuba;

        public KaratsubaCommand(TextWriter consoleOut, IKaratsuba karatsuba)
        {
            _consoleOut = consoleOut;
            _karatsuba = karatsuba;
        }

        public void Execute(KaratsubaOptions options)
        {
            var x = options.X.ToSpanOfInt();
            var y = options.Y.ToSpanOfInt();
            var product = _karatsuba.MultiplyXAndY(x, y);
            var productStr = string.Concat(product.ToArray().Select(n => n.ToString()));
            _consoleOut.WriteLine(productStr);
        }
    }
}
