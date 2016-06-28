using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePathfinding.AI.Pathfinding
{
    public class PathfindingResult
    {
        public IPathfindingEngine Engine { get; }

        public IPathfindingNode Path { get; }
        public List<IPathfindingNode> ClosedList { get; }
        public List<IPathfindingNode> OpenedList { get; }

        public PathfindingResult(IPathfindingEngine engine, IPathfindingNode path, List<IPathfindingNode> closedList, List<IPathfindingNode> openedList)
        {
            Engine = engine;
            Path = path;
            ClosedList = closedList;
            OpenedList = openedList;
        }
    }
}
