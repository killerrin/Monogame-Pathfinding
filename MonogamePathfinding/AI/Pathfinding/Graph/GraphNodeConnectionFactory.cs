using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePathfinding.AI.Pathfinding.Graph
{
    public class GraphNodeConnectionFactory : IGraphNodeConnectionFactory
    {
        public IGraphNodeConnection CreateConnection(IGraphNode from, IGraphNode to)
        {
            return new GraphNodeConnection(from, to);
        }
    }
}
