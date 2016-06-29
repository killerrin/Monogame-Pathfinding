using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePathfinding.AI.Pathfinding
{
    public class PathfindingResult
    {
        public IPathfindingNode Path { get; }
        public IEnumerable<IPathfindingNode> ClosedList { get; }
        public IEnumerable<IPathfindingNode> OpenedList { get; }

        public PathfindingResult() : this(null) { }
        public PathfindingResult(IPathfindingNode path) : this(path, new List<IPathfindingNode>(), new List<IPathfindingNode>()) { }
        public PathfindingResult(IPathfindingNode path, IEnumerable<IPathfindingNode> closedList, IEnumerable<IPathfindingNode> openedList)
        {
            Path = path;
            ClosedList = closedList;
            OpenedList = openedList;
        }
    }
}
