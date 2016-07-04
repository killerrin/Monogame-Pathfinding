using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePathfinding.AI.Pathfinding.Grid
{
    public class GridNode : IGridNode
    {
        public NodePosition Position { get; }
        public int MovementCost { get; set; }
        public TraversalSettings Navigatable { get; set; }

        public GridNode(NodePosition position) : this(position, 0, TraversalSettings.Passable) { }
        public GridNode(NodePosition position, int movementCost, TraversalSettings traversable)
        {
            Position = position;
            MovementCost = movementCost;
            Navigatable = Navigatable;
        }
    }
}
