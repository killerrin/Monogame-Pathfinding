using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePathfinding.AI.Pathfinding.Grid
{
    public class PathfindingGrid : IPathfindingGrid
    {
        private IGridNode[,] Grid { get; }

        public IGridNodeFactory NodeFactory { get; }
        public int Width { get { return Grid.GetLength(0); } }
        public int Height { get { return Grid.GetLength(1); } }

        public PathfindingGrid(IGridNodeFactory gridNodeFactory, int width, int height)
        {
            NodeFactory = gridNodeFactory;

            Grid = new IGridNode[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Grid[x, y] = NodeFactory.CreateNode(new NodePosition(x, y));
                }
            }
        }

        public IGridNode this[NodePosition position] { get { return FindNode(position); } }
        public IGridNode this[int x, int y] { get { return FindNode(x, y); } }

        public IGridNode FindNode(NodePosition position) => FindNode(position.X, position.Y);
        public IGridNode FindNode(int x, int y)
        {
            if (!WithinGrid(x, y))
                return null;

            return Grid[x, y];
        }

        public bool WithinGrid(NodePosition position) => WithinGrid(position.X, position.Y);
        public bool WithinGrid(int x, int y)
        {
            if (x < 0 || y < 0)
                return false;

            if (x >= Width || y >= Height)
                return false;

            return true;
        }

        public IList<IGridNode> GetEntireGrid()
        {
            List<IGridNode> nodes = new List<IGridNode>();
            for (int x = 0; x < Width; x++)
            {
                nodes.AddRange(GetXColumn(x));
            }

            return nodes;
        }

        public IList<IGridNode> GetXColumn(int column)
        {
            List<IGridNode> nodes = new List<IGridNode>();
            for (int y = 0; y < Height; y++)
            {
                nodes.Add(Grid[column, y]);
            }

            return nodes;
        }

        public IList<IGridNode> GetYRow(int row)
        {
            List<IGridNode> nodes = new List<IGridNode>();
            for (int x = 0; x < Width; x++)
            {
                nodes.Add(Grid[x, row]);
            }

            return nodes;
        }

        public IList<IGridNode> GetAdjacentNodes(NodePosition centerNode, bool allowHorizontalVertical, bool allowDiagonal)
        {
            // Using the helper methods, grab all of the nodes from each direction
            List<NodePosition> positions = new List<NodePosition>();

            if (allowHorizontalVertical)
            {
                positions.Add(centerNode.North());
                positions.Add(centerNode.South());
                positions.Add(centerNode.East());
                positions.Add(centerNode.West());
            }
            if (allowDiagonal)
            {
                positions.Add(centerNode.NorthEast());
                positions.Add(centerNode.SouthEast());
                positions.Add(centerNode.NorthWest());
                positions.Add(centerNode.SouthWest());
            }

            // Finally, convert all those positions to GridNodes and return them
            List<IGridNode> adjacentNodes = new List<IGridNode>();
            foreach (var position in positions)
            {
                var node = FindNode(position);
                if (node != null)
                    adjacentNodes.Add(node);
            }

            return adjacentNodes;
        }
    }
}
