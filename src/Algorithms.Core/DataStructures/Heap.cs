using System;

namespace Algorithms.Core.DataStructures
{
    public abstract class Heap<TItem> where TItem : struct, IComparable<TItem>
    {
        private TItem[] _items = new TItem[1];

        public int Size { get; private set; }

        public virtual void Insert(TItem item)
        {
            if (_items.Length == Size)
            {
                _items = IncreaseSize(_items);
            }

            _items[Size] = item;

            EnsureHeapPropertyUp(Size);
            Size++;
        }

        private T[] IncreaseSize<T>(T[] current)
        {
            var newItems = new T[current.Length << 1].AsSpan();
            current.AsSpan().CopyTo(newItems);
            return newItems.ToArray();
        }

        private void EnsureHeapPropertyUp(int index)
        {
            if (index == 0) return;

            var child = _items[index];
            var parentIndex = (index - 1) >> 1;
            var parent = _items[parentIndex];

            if (CompareItems(parent, child))
            {
                Swap(_items.AsSpan(), index, parentIndex);
                EnsureHeapPropertyUp(parentIndex);
            }
        }

        protected abstract bool CompareItems(TItem item1, TItem item2);

        private void Swap<T>(Span<T> array, int childIndex, int parentIndex)
        {
            var temp = array[childIndex];
            array[childIndex] = array[parentIndex];
            array[parentIndex] = temp;
        }

        public virtual TItem ExtractRoot()
        {
            var result = _items[0];

            _items[0] = _items[Size - 1];

            EnsureHeapPropertyDown(0);
            Size--;
            return result;
        }

        private void EnsureHeapPropertyDown(int parentIndex)
        {
            var parent = _items[parentIndex];
            var childrenStartIndex = (parentIndex << 1) + 1;
            if (childrenStartIndex >= Size) return;
            var sliceLength = childrenStartIndex == Size - 1 ? 1 : 2;
            var children = _items.AsSpan().Slice(childrenStartIndex, sliceLength);

            var childIndex = children.Length == 1 || CompareItems(children[1], children[0])
                ? childrenStartIndex
                : childrenStartIndex + 1;
            var child = _items[childIndex];

            if (CompareItems(parent, child))
            {
                Swap(_items.AsSpan(), parentIndex, childIndex);
                EnsureHeapPropertyDown(childIndex);
            }
        }

        public TItem Root()
        {
            return _items[0];
        }
    }
}
