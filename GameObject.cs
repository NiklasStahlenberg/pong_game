using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NiklasGame
{
    public class Timer
    {
        public int Duration;
        public double? Added;
        public bool IsDone;
        public Func<int, bool> Action;
        public int Count { get; set; }
    }
    
    public class GameObject
    {
        public Vector2 Position;
        public Texture2D Texture;
        public Vector2 Direction;
        public Rectangle Bounds;
        internal bool ShouldBeDeleted;
        internal List<IAnimation> Animations = new();
        internal List<Timer> Timers = new();
        public float Speed { get; set; } = 1;

        public virtual void Initialize()
        {
        }

        public void Animate(IAnimation animation)
        {
            Animations.Add(animation);
        }

        public void Animate(int ms, Action<float> action)
        {
            Animations.Add(new LinearAnimation(ms, action));
        }

        public void AddTimer(int ms, Func<int,bool> onFire)
        {
            Timers.Add(new Timer()
            {
                Duration = ms,
                Count = 0,
                Action = onFire
            });
        }

        public virtual void Update(GameTime gameTime)
        {
            Position += Direction * Speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            HandleAnimations(gameTime);
            HandleTimers(gameTime);
        }

        private void HandleTimers(GameTime gameTime)
        {
            var ms = gameTime.TotalGameTime.TotalMilliseconds;
            foreach (var timer in Timers)
            {
                if (timer.IsDone)
                    continue;
                
                if (!timer.Added.HasValue)
                {
                    timer.Added = ms;
                }
                else
                {
                    var count = (int)((ms-timer.Added.Value) / timer.Duration);
                    if (count > timer.Count)
                    {
                        timer.IsDone = timer.Action(count);
                    }

                    timer.Count = count;
                }
            }

            Timers.RemoveAll(d => d.IsDone);
        }

        private void HandleAnimations(GameTime gameTime)
        {
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

        public virtual Rectangle GetWorldBounds() => Bounds.WithPosition(Position);
    }
}