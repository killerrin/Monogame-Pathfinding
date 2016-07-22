using MonogamePathfinding.AI.Pathfinding.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePathfinding.AI.Pathfinding.Graph
{
    public class GraphNodeConnection : IGraphNodeConnection
    {
        public IGraphNode From { get; }
        public IGraphNode To { get; }

        public int MovementCost { get; set; }
        public TraversalSettings Navigatable { get; set; }

        public GraphNodeConnection(IGraphNode from, IGraphNode to) : this(from, to, 0, TraversalSettings.Passable) { }
        public GraphNodeConnection(IGraphNode from, IGraphNode to, int movementCost, TraversalSettings navigatable)
        {
            From = from;
            To = to;
            MovementCost = movementCost;
            Navigatable = navigatable;
        }
    }
}
