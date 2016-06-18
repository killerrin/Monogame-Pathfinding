using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePathfinding.AI.Pathfinding.Grid
{
    public interface IGridNode
    {
        NodePosition Position { get; }

        int MovementCost { get; set; }
        TraversalSettings Navigatable { get; set; }
    }

    public class GridNode : IGridNode
    {
        public NodePosition Position { get; }

        private int m_movementCost;
        public int MovementCost
        {
            get { lock (m_lockObject) { return m_movementCost; } }
            set { lock (m_lockObject) { m_movementCost = value; } }
        }

        private TraversalSettings m_navigatable;
        public TraversalSettings Navigatable
        {
            get { lock (m_lockObject) { return m_navigatable; } }
            set { lock (m_lockObject) { m_navigatable = value; } }
        }

        private readonly object m_lockObject = new object();
        public GridNode(NodePosition position)
        {
            Position = position;
            MovementCost = 10;
            Navigatable = TraversalSettings.Passable;
        }
        public GridNode(NodePosition position, int movementCost, TraversalSettings traversable)
        {
            Position = position;
            MovementCost = movementCost;
            Navigatable = Navigatable;
        }
    }
}
