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
        private readonly Scoreboard _scoreboard;
        private readonly Vector2 _initialPosition;
        private const float InitialSpeed = 0.25f;

        public int Size { get; set; } = 20;

        public override void Initialize()
        {
           
        }

        public override void LoadContent(ContentManager content)
        {
            Texture = content.Load<Texture2D>("ball");
        }

        public Ball(Scoreboard scoreboard, Vector2 initialPosition)
        {
            var randomNumber = Environment.TickCount % 2 - 1;
            this._scoreboard = scoreboard;
            this._initialPosition = initialPosition;
            var half = Size / 2;
            Bounds = new Rectangle(half, half, Size, Size);
            Reset(randomNumber == 0 ? -1 : 1);
        }

        public override void OnCollision(GameObject collidesWith)
        {
            if (collidesWith is Edge edge)
            {
                var side = edge.EdgeSide;
                switch (side)
                {
                    case Edge.Side.Top:

                    case Edge.Side.Bottom:
                        Direction.Y = -Direction.Y;
                        break;
                    case Edge.Side.Left:
                    case Edge.Side.Right:
                        _scoreboard.IncreasePlayerScore(side);
                        Reset(-Direction.X);
                        break;
                }
            }
            else if (collidesWith is Pad pad)
            {
                var totalHeight = pad.Bounds.Height;
                var centerOffset = GetWorldBounds().Center - collidesWith.GetWorldBounds().Center;
                var offsetPercentage = (centerOffset.Y / (totalHeight / 2f));


                Direction.X = -Direction.X;
                Direction.Y += offsetPercentage;
                Speed *= pad.RestitutionCoefficient;
            }
        }

        private void Reset(float x)
        {
            Position = _initialPosition;
            Speed = InitialSpeed;
            Direction = new Vector2(x, 0);
        }
    }
}