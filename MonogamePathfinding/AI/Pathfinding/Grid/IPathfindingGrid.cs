using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePathfinding.AI.Pathfinding.Grid
{
    public interface IPathfindingGrid
    {
        int Width { get; }
        int Height { get; }

        IGridNode FindNode(int x, int y);
        IGridNode FindNode(NodePosition position);

        bool WithinGrid(NodePosition position);
        bool WithinGrid(int x, int y);
    }

    public class PathfindingGrid : IPathfindingGrid
    {
        private IGridNode[,] Grid { get; }
        public int Width { get { return Grid.GetLength(0); } }
        public int Height { get { return Grid.GetLength(1); } }

        public PathfindingGrid(int width, int height)
        {
            Grid = new GridNode[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Grid[x, y] = new GridNode(new NodePosition(x, y));
                }
            }
        }

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

            if (x > Width || y > Height)
                return false;

            return true;
        }

    }
}
