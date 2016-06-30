using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePathfinding.AI.Pathfinding.Grid
{
    public static class PathfindingGridExtensions
    {
        public static int TotalNodes(this IPathfindingGrid grid)
        {
            return grid.Width * grid.Height;
        }

        public static int TotalTraversalNodes(this IPathfindingGrid grid, TraversalSettings traversal)
        {
            var entireGrid = grid.GetEntireGrid();
            int total = 0;
            foreach (var node in entireGrid)
            {
                if (node.Navigatable == traversal)
                    total++;
            }

            return total;
        }
    }
}
