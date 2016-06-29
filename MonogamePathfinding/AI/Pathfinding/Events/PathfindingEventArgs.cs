using MonogamePathfinding.AI.Pathfinding.Engines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePathfinding.AI.Pathfinding.Events
{
    public delegate void PathfindingEventHandler(IPathfindingEngine sender, PathfindingEventArgs args);
    public class PathfindingEventArgs : EventArgs
    {
        public PathfindingResult Result { get; }
        public PathfindingEventArgs(PathfindingResult result)
            :base()
        {
            Result = result;
        }
    }
}
