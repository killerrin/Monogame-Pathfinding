using MonogamePathfinding.AI.Pathfinding.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePathfinding.AI.Pathfinding.Graph
{
    public interface IGraphNode
    {
        int ID { get; }
        IList<IGraphNodeConnection> Connections { get; }
    }
}
