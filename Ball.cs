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
        private const int initialSize = 18;
        
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
            SetBoundsFromSize(initialSize);
            Reset(randomNumber == 0 ? -1 : 1);
        }

        private void SetBoundsFromSize(int size)
        {
            var half = size / 2;
            Bounds = new Rectangle(half, half, size, size);
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
            else if (collidesWith is PowerUp pup)
            {
                var s = Bounds.Width;
                Animate(500, p=>SetBoundsFromSize((int)(p*10f)+s));
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