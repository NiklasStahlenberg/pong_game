using System;
using Microsoft.Xna.Framework;

namespace NiklasGame
{
    public interface IAnimation
    {
        bool IsDone { get; }
        float Position { get; }

        void Update(GameTime gameTime);
    }

    public class RevertingLinearAnimation : IAnimation
    {
        public int TotalMilliseconds;
        public Action<float> Action;
        public float Position { get; internal set; }

        public void Update(GameTime gameTime)
        {
        }

        private float accumulatedTime;

        public bool IsDone { get; internal set; }
    }
    
    public class Cubic
    {		
        public static float In (float k) {
            return k*k*k;
        }
		
        public static float Out (float k) {
            return 1f + ((k -= 1f)*k*k);
        }
		
        public static float InOut (float k) {
            if ((k *= 2f) < 1f) return 0.5f*k*k*k;
            return 0.5f*((k -= 2f)*k*k + 2f);
        }
    };

    public class EaseAnimation : LinearAnimation
    {
        public override float GetUpdatePosition(GameTime gameTime)
        {
            var linearPosition = base.GetUpdatePosition(gameTime);
            return Cubic.InOut(linearPosition);
        }

        public EaseAnimation(int ms, Action<float> action) : base(ms, action)
        {
        }
    }

    public class LinearAnimation : IAnimation
    {
        public int TotalMilliseconds;
        public Action<float> Action;
        public float Position { get; internal set; }

        private float accumulatedTime;

        public bool IsDone { get; internal set; }

        public LinearAnimation(int ms, Action<float> action)
        {
            TotalMilliseconds = ms;
            Action = action;
        }

        public void Update(GameTime gameTime)
        {
            Position = GetUpdatePosition(gameTime);

            Action(Position);
        }

        public virtual float GetUpdatePosition(GameTime gameTime)
        {
            accumulatedTime += gameTime.ElapsedGameTime.Milliseconds;

            if (accumulatedTime > TotalMilliseconds)
            {
                IsDone = true;
                return 1f;
            }

            return accumulatedTime / (float) TotalMilliseconds;
        }
    }
}