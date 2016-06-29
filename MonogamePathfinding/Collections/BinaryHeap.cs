using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePathfinding.Collections
{
    public class BinaryHeap<K, T> : IEnumerable<T>
        where K : IComparable
    {
        protected IPriorityQueueNode<K, T>[] _data;
        protected Comparison<K> _comparison;

        public int Size { get; protected set; }
        public int Count { get { return Size; } }

        public BinaryHeap() : this(4) { }
        public BinaryHeap(int capacity) : this(capacity, null) { }
        public BinaryHeap(int capacity, Comparison<K> comparison)
        {
            _data = new IPriorityQueueNode<K, T>[capacity];
            _comparison = comparison;
            if (_comparison == null)
                _comparison = new Comparison<K>(Comparer<K>.Default.Compare);
        }

        /// <summary>
        /// Add an item to the heap
        /// </summary>
        /// <param name="item"></param>
        public void Insert(IPriorityQueueNode<K, T> item)
        {
            if (Size == _data.Length)
                Resize();
            _data[Size] = item;
            HeapifyUp(Size);
            Size++;
        }
        /// <summary>
        /// Add an item to the heap
        /// </summary>
        /// <param name="item"></param>
        public void Insert(K priority, T item)
        {
            if (Size == _data.Length)
                Resize();
            _data[Size] = new BinaryHeapNode<K, T>(priority, item);
            HeapifyUp(Size);
            Size++;
        }

        /// <summary>
        /// Get the item of the root
        /// </summary>
        /// <returns></returns>
        public IPriorityQueueNode<K, T> Peak()
        {
            return _data[0];
        }

        /// <summary>
        /// Extract the item of the root
        /// </summary>
        /// <returns></returns>
        public IPriorityQueueNode<K, T> Pop()
        {
            IPriorityQueueNode<K, T> item = _data[0];
            Size--;
            _data[0] = _data[Size];
            HeapifyDown(0);
            return item;
        }

        #region Sort
        private void Resize()
        {
            IPriorityQueueNode<K, T>[] resizedData = new IPriorityQueueNode<K, T>[_data.Length * 2];
            Array.Copy(_data, 0, resizedData, 0, _data.Length);
            _data = resizedData;
        }

        private void HeapifyUp(int childIdx)
        {
            if (childIdx > 0)
            {
                int parentIdx = (childIdx - 1) / 2;
                if (_comparison.Invoke(_data[childIdx].Priority, _data[parentIdx].Priority) > 0)
                {
                    // swap parent and child
                    IPriorityQueueNode<K, T> t = _data[parentIdx];
                    _data[parentIdx] = _data[childIdx];
                    _data[childIdx] = t;
                    HeapifyUp(parentIdx);
                }
            }
        }

        private void HeapifyDown(int parentIdx)
        {
            int leftChildIdx = 2 * parentIdx + 1;
            int rightChildIdx = leftChildIdx + 1;
            int largestChildIdx = parentIdx;
            if (leftChildIdx < Size && _comparison.Invoke(_data[leftChildIdx].Priority, _data[largestChildIdx].Priority) > 0)
            {
                largestChildIdx = leftChildIdx;
            }
            if (rightChildIdx < Size && _comparison.Invoke(_data[rightChildIdx].Priority, _data[largestChildIdx].Priority) > 0)
            {
                largestChildIdx = rightChildIdx;
            }
            if (largestChildIdx != parentIdx)
            {
                IPriorityQueueNode<K, T> t = _data[parentIdx];
                _data[parentIdx] = _data[largestChildIdx];
                _data[largestChildIdx] = t;
                HeapifyDown(largestChildIdx);
            }
        }
        #endregion

        public IPriorityQueueNode<K, T> this[int key]
        {
            get
            {
                return _data[key];
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in _data)
            {
                yield return item.Data;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #region Special Heaps
        public static BinaryHeap<K, T> MinBinaryHeap(int capacity)
        {
            return new BinaryHeap<K, T>(capacity, new Comparison<K>(MinCompare));
        }
        public static BinaryHeap<K, T> MaxBinaryHeap(int capacity)
        {
            return new BinaryHeap<K, T>(capacity, new Comparison<K>(MaxCompare));
        }

        protected static int MinCompare(K i1, K i2)
        {
            if (i1.CompareTo(i2) < 0)
                return 1;
            else if (i1.CompareTo(i2) > 0)
                return -1;
            return 0;
        }

        protected static int MaxCompare(K i1, K i2)
        {
            if (i1.CompareTo(i2) < 0)
                return -1;
            else if (i1.CompareTo(i2) > 0)
                return 1;
            return 0;
        }
        #endregion
    }
}
