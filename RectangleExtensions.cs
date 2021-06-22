using Microsoft.Xna.Framework;

namespace NiklasGame
{
    public static class RectangleExtensions
    {
        public static Rectangle WithPosition(this Rectangle rect, Vector2 position)
        {
            return new Rectangle((int)(rect.X + position.X), (int)(rect.Y + position.Y), rect.Width, rect.Height);
        }
    }
}