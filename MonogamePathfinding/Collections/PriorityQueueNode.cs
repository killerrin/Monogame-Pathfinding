using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePathfinding.Collections
{
    public class PriorityQueueNode<T> : IComparable<PriorityQueueNode<T>>
    {
        public readonly int Priority;
        public T Data;

        public PriorityQueueNode(int priority, T data)
        {
            Priority = priority;
            Data = data;
        }

        public int CompareTo(PriorityQueueNode<T> other)
        {
            if (Priority < other.Priority) return -1;
            else if (Priority > other.Priority) return 1;
            else return 0;
        }

        public override string ToString() => $"Priority: {Priority}";
    }
}
