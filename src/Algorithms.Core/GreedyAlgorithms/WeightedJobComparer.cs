using System;
using System.Collections.Generic;

namespace Algorithms.Core.GreedyAlgorithms
{
    public abstract class WeightedJobComparer : IComparer<WeightedJob>
    {
        public static WeightedJobComparer Difference => new DifferenceComparer();

        public abstract int Compare(WeightedJob x, WeightedJob y);
    }

    internal class DifferenceComparer : WeightedJobComparer
    {
        public override int Compare(WeightedJob x, WeightedJob y)
        {
            throw new NotImplementedException();
        }
    }
}
