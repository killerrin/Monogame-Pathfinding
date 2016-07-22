using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePathfinding.AI.Pathfinding.Graph.Grid
{
    public interface IPathfindingGrid
    {
        IGridNodeFactory NodeFactory { get; }

        int NodeCount { get; }
        int Width { get; }
        int Height { get; }

        IGridNode FindNode(int x, int y);
        IGridNode FindNode(NodePosition position);

        bool NodeExists(NodePosition position);
        bool NodeExists(int x, int y);

        IReadOnlyCollection<IGridNode> GetAllNodes();
        IReadOnlyCollection<IGridNode> GetXColumn(int column);
        IReadOnlyCollection<IGridNode> GetYRow(int row);

        IReadOnlyCollection<IGridNode> GetAdjacentNodes(NodePosition centerNode, bool allowHorizontalVertical, bool allowDiagonal);
    }
}
