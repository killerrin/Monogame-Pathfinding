using MonogamePathfinding.AI.Pathfinding.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePathfinding.AI.Pathfinding
{
    public interface IPathfindingNode
    {
        IPathfindingNode Parent { get; set; }
        IGridNode GridNode { get; }
    }

    public static class PathfindingExtensions
    {
        public static IPathfindingNode Reverse(this IPathfindingNode root)
        {
            IPathfindingNode p = root, n = null;
            while (p != null)
            {
                IPathfindingNode tmp = p.Parent;
                p.Parent = n;
                n = p;
                p = tmp;
            }
            root = n;
            return root;
        }
    }
}
