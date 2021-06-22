using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NiklasGame
{
    public static class TextureExtensions
    {
        public static Texture2D Fill(this Texture2D texture, Color color)
        {
            var size = texture.Bounds.Width * texture.Bounds.Height;
            var colorData = new Color[size];
            for (int i = 0; i < size; i++)
                colorData[i] = color;

            texture.SetData(colorData);
            return texture;
        }
    }
    
    public static class RectangleExtensions
    {
        public static Vector2 ClampY(this Vector2 vector2, float min, float max)
        {
            if (vector2.Y < min)
            {
                vector2.Y = min;
            }
            else if (vector2.Y > max)
            {
                vector2.Y = max;
            }

            return vector2;
        }
        
        public static Rectangle WithPosition(this Rectangle rect, Vector2 position)
        {
            return new Rectangle((int)(rect.X + position.X), (int)(rect.Y + position.Y), rect.Width, rect.Height);
        }
        
        public static Rectangle WithPosition(this Rectangle rect, Vector2 position, int offsetX, int offsetY)
        {
            return new Rectangle((int)(rect.X + position.X+offsetX), (int)(rect.Y + position.Y+offsetY), rect.Width, rect.Height);
        }
    }
}