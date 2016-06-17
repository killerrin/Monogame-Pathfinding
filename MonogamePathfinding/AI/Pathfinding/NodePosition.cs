using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePathfinding.AI.Pathfinding
{
    public struct NodePosition
    {
        public int X { get; }
        public int Y { get; }

        public NodePosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        #region Directions
        public NodePosition North() => this + new NodePosition(0, -1);
        public NodePosition South() => this + new NodePosition(0, 1);

        public NodePosition West() => this + new NodePosition(-1, 0);
        public NodePosition East() => this + new NodePosition(1, 0);

        public NodePosition NorthWest() => this + new NodePosition(-1, -1);
        public NodePosition NorthEast() => this + new NodePosition(1, -1);

        public NodePosition SouthWest() => this + new NodePosition(-1, 1);
        public NodePosition SouthEast() => this + new NodePosition(1, 1);
        #endregion

        public override bool Equals(object obj)
        {
            if (obj is NodePosition)
                return Equals((NodePosition)obj);
            return false;
        }
        public bool Equals(NodePosition other) => X == other.X && Y == other.Y;
        public bool Equals(int x, int y) => X == x && Y == y;

        public static bool operator ==(NodePosition one, NodePosition two) => one.Equals(two);
        public static bool operator !=(NodePosition one, NodePosition two) => !one.Equals(two);

        public static NodePosition operator +(NodePosition one, NodePosition two) => new NodePosition(one.X + two.X, one.Y + two.Y);
        public static NodePosition operator -(NodePosition one, NodePosition two) => new NodePosition(one.X - two.X, one.Y - two.Y);

        public override int GetHashCode() => X.GetHashCode() ^ Y.GetHashCode();
        public override string ToString() => $"{X}, {Y}";
    }
}
