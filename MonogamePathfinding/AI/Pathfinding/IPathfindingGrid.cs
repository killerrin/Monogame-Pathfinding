using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePathfinding.AI.Pathfinding
{
    public interface IPathfindingGrid
    {
        IEnumerable<IEnumerable<IPathfindingNode>> Grid { get; }

        void FindNode(int x, int y);
        void FindNode(NodePosition position);
    }
}
