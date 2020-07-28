using System;

namespace Algorithms.Core.DataStructures
{
    public class MaxHeap<T> : Heap<T>
        where T : struct, IComparable<T>
    {
        protected override bool CompareItems(T item1, T item2)
        {
            return item1.CompareTo(item2) < 0;
        }
    }
}
