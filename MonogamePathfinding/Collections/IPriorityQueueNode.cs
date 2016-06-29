using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePathfinding.Collections
{
    public interface IPriorityQueueNode<K, T> : IComparable<IPriorityQueueNode<K, T>>
        where K : IComparable
    {
        K Priority { get; }
        T Data { get; set; }
    }
}
