using System;
using Microsoft.Xna.Framework;

namespace NiklasGame
{
    public class Edge: GameObject
    {
        public enum Side
        {
            Top,
            Bottom,
            Left,
            Right
        }

        public Side EdgeSide { get; set; }

        public Edge(Side side, Rectangle rect)
        {
            EdgeSide = side;
            Bounds = GetInitialBounds(side, rect, 10);
        }

        private Rectangle GetInitialBounds(Side side, Rectangle rect, int margin = 100) =>
            side switch
            {
                Side.Top => new Rectangle(rect.Left, rect.Top - margin, rect.Width, margin),
                Side.Bottom => new Rectangle(rect.Left, rect.Bottom, rect.Width, margin),
                Side.Left => new Rectangle(rect.Left - margin, rect.Top - margin, margin, rect.Height + 2*margin),
                Side.Right => new Rectangle(rect.Right, rect.Top - margin, margin, rect.Height + 2*margin),
                _ => throw new ArgumentOutOfRangeException(nameof(side), side, null)
            };
    }
}