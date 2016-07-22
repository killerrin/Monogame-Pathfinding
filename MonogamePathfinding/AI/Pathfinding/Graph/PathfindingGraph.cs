using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePathfinding.AI.Pathfinding.Graph
{
    public class PathfindingGraph : IPathfindingGraph
    {
        private Dictionary<int, IGraphNode> Nodes { get; }
        protected int NodeIDCount { get; set; }

        public IGraphNodeFactory NodeFactory { get; }
        public IGraphNodeConnectionFactory ConnectionFactory { get; }
        public int NodeCount { get { return Nodes.Count; } }

        public PathfindingGraph(IGraphNodeFactory nodeFactory, IGraphNodeConnectionFactory connectionFactory)
        {
            Nodes = new Dictionary<int, IGraphNode>();
            NodeIDCount = 0;

            NodeFactory = nodeFactory;
            ConnectionFactory = connectionFactory;
        }

        public IReadOnlyCollection<IGraphNode> GetAllNodes() => Nodes.Values.ToList();
        public IGraphNode FindNode(int id) => Nodes[id];
        public bool NodeExists(int id) => Nodes.ContainsKey(id);

        public bool RemoveNode(int id) => Nodes.Remove(id);
        public IGraphNode CreateNode()
        {
            var node = NodeFactory.CreateNode(NodeIDCount++);
            Nodes[node.ID] = node;
            return node;
        }

        public IGraphNodeConnection CreateUnidirectionalConnection(IGraphNode from, IGraphNode to)
        {
            var connection = ConnectionFactory.CreateConnection(from, to);
            from.Connections.Add(connection);
            return connection;
        }

        public Tuple<IGraphNodeConnection, IGraphNodeConnection> CreateBidirectionalConnection(IGraphNode from, IGraphNode to)
        {
            var connectionOne = ConnectionFactory.CreateConnection(from, to);
            from.Connections.Add(connectionOne);

            var connectionTwo = ConnectionFactory.CreateConnection(to, from);
            to.Connections.Add(connectionTwo);

            return new Tuple<IGraphNodeConnection, IGraphNodeConnection>(connectionOne, connectionTwo);
        }
    }
}
