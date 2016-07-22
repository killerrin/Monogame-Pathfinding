using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePathfinding.AI.Pathfinding.Graph
{
    public class GraphNodeFactory : IGraphNodeFactory
    {
        public IGraphNode CreateNode(int id)
        {
            return new GraphNode(id);
        }
    }
}
