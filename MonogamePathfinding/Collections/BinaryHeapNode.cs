using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePathfinding.Collections
{
    public class BinaryHeapNode<K, T> : IPriorityQueueNode<K, T>
        where K : IComparable
    {
        public K Priority { get; }
        public T Data { get; set; }

        public BinaryHeapNode(K priority, T data)
        {
            Priority = priority;
            Data = data;
        }

        public int CompareTo(IPriorityQueueNode<K, T> other)
        {
            if (Priority.CompareTo(other.Priority) < 0) return -1;
            else if (Priority.CompareTo(other.Priority) > 0) return 1;
            return 0;
        }

        public override string ToString() => $"Priority: {Priority}";
    }
}
