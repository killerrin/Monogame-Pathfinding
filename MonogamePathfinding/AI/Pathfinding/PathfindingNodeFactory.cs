using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonogamePathfinding.AI.Pathfinding.Graph.Grid;

namespace MonogamePathfinding.AI.Pathfinding
{
    public class PathfindingNodeFactory : IPathfindingNodeFactory
    {
        public IPathfindingNode CreateNode(IGridNode gridNode, IPathfindingNode parent)
        {
            return new PathfindingNode(gridNode, parent);
        }
    }
}
