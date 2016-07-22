using MonogamePathfinding.AI.Pathfinding.Graph.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePathfinding.AI.Pathfinding.Graph
{
    public interface IGraphNodeConnection
    {
        IGraphNode From { get; }
        IGraphNode To { get; }

        int MovementCost { get; set; }
        TraversalSettings Navigatable { get; set; }
    }
}
