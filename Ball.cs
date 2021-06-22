using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace NiklasGame
{
    public class Ball : GameObject
    {
        private readonly Scoreboard scoreboard;
        private readonly Vector2 initialPosition;
        private readonly GameObject[] pads;
        private const float initialSpeed = 0.25f;

        public override void Initialize()
        {
           
        }

        public override void LoadContent(ContentManager content)
        {
            Texture = content.Load<Texture2D>("ball");
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

        private void Reset(float x)
        {
            Position = initialPosition;
            Speed = initialSpeed;
            Direction = new Vector2(x, 0);
        }
    }
}