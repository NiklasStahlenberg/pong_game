using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NiklasGame
{
    public class GameObject
    {
        public Vector2 Position;
        public Texture2D Texture;
        public Vector2 Direction;
        public Rectangle Bounds;

        public float Speed { get; set; } = 1;
        
        public virtual void Initialize()
        {
            
        }
        
        public virtual void Update(GameTime gameTime)
        {
            Position += Direction * Speed; //* (float)gameTime.ElapsedGameTime.Milliseconds;
        }

        public virtual void LoadContent(ContentManager content)
        {
            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Texture == null)
                return;
            spriteBatch.Draw(Texture, GetWorldBounds(), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0);
        }

        public virtual void OnCollision(GameObject collidesWith)
        {
            
        }

        public Rectangle GetWorldBounds() => Bounds.WithPosition(Position);
    }
}