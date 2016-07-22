using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePathfinding.AI.Pathfinding.Graph
{
    public class GraphNode : IGraphNode
    {
        public int ID { get; }
        public IList<IGraphNodeConnection> Connections { get; }

        public GraphNode(int id)
        {
            ID = id;
            Connections = new List<IGraphNodeConnection>();
        }
    }
}
