using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NiklasGame
{
    public class GameObject
    {
        public Vector2 Position;
        public Texture2D Texture;
        public float Momentum;
        public Rectangle Bounds;

        public float Speed { get; set; } = 1;
        
        public virtual void Initialize()
        {
            
        }
        
        public virtual void Update(GameTime gameTime)
        {
            Position.Y += Momentum * Speed * gameTime.ElapsedGameTime.Milliseconds;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Texture == null)
                return;
            
            spriteBatch.Draw(Texture, Position, null, Color.Red, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
        }
    }
}