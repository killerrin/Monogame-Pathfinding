using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePathfinding.AI.Pathfinding.Graph
{
    public interface IPathfindingGraph
    {
        IGraphNodeFactory NodeFactory { get; }
        IGraphNodeConnectionFactory ConnectionFactory { get; }
        int NodeCount { get; }

        IReadOnlyCollection<IGraphNode> GetAllNodes();
        IGraphNode FindNode(int id);
        bool NodeExists(int id);

        IGraphNodeConnection CreateUnidirectionalConnection(IGraphNode from, IGraphNode to);
        Tuple<IGraphNodeConnection, IGraphNodeConnection> CreateBidirectionalConnection(IGraphNode from, IGraphNode to);
    }
}
