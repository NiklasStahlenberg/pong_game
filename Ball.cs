using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace NiklasGame
{
    public class Ball : GameObject
    {
        private readonly Scoreboard scoreboard;
        private readonly Vector2 initialPosition;
        private readonly GameObject[] pads;
        private const float initialSpeed = 3.0f;

        public override void Initialize()
        {
            Texture = new Texture2D(Game1.GraphicsDeviceManager.GraphicsDevice, Bounds.Width, Bounds.Height);
            Color[] colorData = new Color[Bounds.Width * Bounds.Height];
            for (int i = 0; i < Bounds.Width * Bounds.Height; i++)
                colorData[i] = Color.White;

            Texture.SetData(colorData);
        }

        public Ball(Scoreboard scoreboard, Vector2 initialPosition, params GameObject[] pads)
        {
            var randomNumber = Environment.TickCount % 2 - 1;
            this.scoreboard = scoreboard;
            this.initialPosition = initialPosition;
            this.pads = pads;
            Bounds = new Rectangle(-5, -5, 10, 10);
            Direction = new Vector2(-1, -1);
            Reset(randomNumber == 0 ? -1 : 1);
        }

        public override void OnCollision(GameObject collidesWith)
        {
            if (collidesWith is Edge edge)
            {
                switch (edge.EdgeSide)
                {
                    case Edge.Side.Top:

                    case Edge.Side.Bottom:
                        Direction.Y = -Direction.Y;
                        break;
                    case Edge.Side.Left:
                    case Edge.Side.Right:
                        scoreboard.IncreasePlayerScore(edge.EdgeSide);
                        Reset(-Direction.X);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else if (collidesWith is Pad pad)
            {
                var totalHeight = pad.Bounds.Height;
                var centerOffset = GetWorldBounds().Center - collidesWith.GetWorldBounds().Center;
                var offsetPercentage = (centerOffset.Y / (totalHeight / 2f));


                Direction.X = -Direction.X;
                Direction.Y += offsetPercentage;
                Speed *= 1.05f;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        private void Reset(float x)
        {
            Position = initialPosition;
            Speed = initialSpeed;
            Direction = new Vector2(x, 0);
        }
    }
}