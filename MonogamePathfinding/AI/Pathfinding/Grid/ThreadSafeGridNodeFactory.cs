using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePathfinding.AI.Pathfinding.Grid
{
    public class ThreadSafeGridNodeFactory : IGridNodeFactory
    {
        public IGridNode CreateNode(NodePosition position)
        {
            return new ThreadSafeGridNode(position);
        }
    }
}
