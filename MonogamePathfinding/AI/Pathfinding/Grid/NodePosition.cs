using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MonogamePathfinding.AI.Pathfinding.Grid
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public NodePosition North() => this + new NodePosition(0, -1);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public NodePosition South() => this + new NodePosition(0, 1);

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public NodePosition West() => this + new NodePosition(-1, 0);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public NodePosition East() => this + new NodePosition(1, 0);

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public NodePosition NorthWest() => this + new NodePosition(-1, -1);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public NodePosition NorthEast() => this + new NodePosition(1, -1);

        [MethodImpl(MethodImplOptions.AggressiveInlining)] public NodePosition SouthWest() => this + new NodePosition(-1, 1);
        [MethodImpl(MethodImplOptions.AggressiveInlining)] public NodePosition SouthEast() => this + new NodePosition(1, 1);
        #endregion

        public bool IsNextTo(NodePosition other)
        {
            return Math.Abs(other.X - X) <= 1 && Math.Abs(other.Y - Y) <= 1;
        }
        public bool IsDiagonalTo(NodePosition other)
        {
            return Math.Abs(other.X - X) == 1 && Math.Abs(other.Y - Y) == 1;
        }

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

        public override int GetHashCode() //=> X.GetHashCode() ^ Y.GetHashCode();
        {
            unchecked // Overflow is fine, just wrap
            {
                int hash = 17;
                // Suitable nullity checks etc, of course :)
                hash = hash * 23 + X.GetHashCode();
                hash = hash * 23 + Y.GetHashCode();
                return hash;
            }
        }
        public override string ToString() => $"{X}, {Y}";
    }
}
