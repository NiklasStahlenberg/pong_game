using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NiklasGame
{
    public class Animation
    {
        
        public int TotalMilliseconds;
        public Action<float> Action;
        private float current;

        internal bool IsDone;

        public Animation(int ms, Action<float> action)
        {
            TotalMilliseconds = ms;
            Action = action;
        }

        public void Update(GameTime gameTime)
        {
            current += gameTime.ElapsedGameTime.Milliseconds;
            Action(current / (float)TotalMilliseconds);
        }
    }

    public class GameObject
    {
        public Vector2 Position;
        public Texture2D Texture;
        public Vector2 Direction;
        public Rectangle Bounds;
        internal bool ShouldBeDeleted;
        internal List<Animation> Animations = new();

        public float Speed { get; set; } = 1;

        public virtual void Initialize()
        {
        }

        public void Animate(int ms, Action<float> action)
        {
            Animations.Add(new Animation(ms, action));
        }

        public virtual void Update(GameTime gameTime)
        {
            
            Position += Direction * Speed * (float) gameTime.ElapsedGameTime.Milliseconds;
            foreach (var animation in Animations)
            {
                animation.Update(gameTime);
            }

            Animations.RemoveAll(d => d.IsDone);

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

        public void MarkForDeletion()
        {
            ShouldBeDeleted = true;
        }

        public Rectangle GetWorldBounds() => Bounds.WithPosition(Position);
    }
}