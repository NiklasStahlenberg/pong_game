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
            accumulatedTime += gameTime.ElapsedGameTime.Milliseconds;

            if (accumulatedTime > TotalMilliseconds)
            {
                Position = 1;
                IsDone = true;
            }
            else
            {
                Position = accumulatedTime / (float)TotalMilliseconds;
            }

            Action(Position);
        }
    }
}