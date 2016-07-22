using MonogamePathfinding.AI.Pathfinding.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePathfinding.AI.Pathfinding
{
    public class PathfindingNode : IPathfindingNode
    {
        public IPathfindingNode Parent { get; set; }
        public IGridNode GridNode { get; }
        public int CurrentMovementCost { get; set; }

        public PathfindingNode(IGridNode gridNode) : this(gridNode, null) { }
        public PathfindingNode(IGridNode gridNode, IPathfindingNode parent)
        {
            GridNode = gridNode;
            Parent = parent;

            //TotalMovementCost = gridNode.MovementCost;
        }
    }
}
