using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NiklasGame
{
    public class Pad : GameObject
    {
        public Keys KeyUp { get; set; } = Keys.W;
        public Keys KeyDown { get; set; } = Keys.S;

        public float RestitutionCoefficient { get; internal set; }

        private const int InitialHeight = 100;
        private int additionalHeight = 0;

        private const float PowerupSize = 15;

        public Pad()
        {
            Reset();
        }

        private void Reset()
        {
            Bounds = new Rectangle(-5, -InitialHeight / 2, 10, InitialHeight);
            Animations.Clear();
            additionalHeight = 0;
            RestitutionCoefficient = 0.8f;
        }

        public override void Initialize()
        {
            Texture =
                new Texture2D(Game1.GraphicsDeviceManager.GraphicsDevice, Bounds.Width, Bounds.Height)
                    .Fill(Color.White);
        }


        public override void Update(GameTime gameTime)
        {
            UpdateBounds();
            GetDirectionInput(gameTime);
            var hh = Bounds.Height / 2;
            Position.ClampY(hh, Game1.ViewPortBounds.Height - hh);
            base.Update(gameTime);
        }

        private void UpdateBounds()
        {
            double powerupSize = 0;
            foreach (var animation in Animations)
            {
                powerupSize += PowerupSize * Math.Sin(animation.Position * Math.PI);
            }

            additionalHeight = (int) powerupSize;

            if (additionalHeight != 0)
            {
                Bounds.Height = InitialHeight + additionalHeight;
            }
        }

        public override Rectangle GetWorldBounds() => Bounds.WithPosition(Position, 0, (int) (-additionalHeight / 2d));

        private void GetDirectionInput(GameTime gameTime)
        {
            var keyBoardState = Keyboard.GetState();
            var keyWasDown = false;

            if (keyBoardState.IsKeyDown(KeyUp))
            {
                Direction.Y -= 0.01f;
                keyWasDown = true;
            }

            if (keyBoardState.IsKeyDown(KeyDown))
            {
                Direction.Y += 0.01f;
                keyWasDown = true;
            }

            if (keyWasDown == false)
            {
                Direction *= 0.99f;
            }
        }


        public void IncreaseSize()
        {
            Animate(new EaseAnimation(5000, (_) => { }));
        }

        public override void OnCollision(GameObject collidesWith)
        {
            if (collidesWith is Edge edge)
            {
                switch (edge.EdgeSide)
                {
                    case Edge.Side.Top:
                        Direction.Y = -Direction.Y * 0.8f;

                        break;
                    case Edge.Side.Bottom:
                        Direction.Y = -Direction.Y * 0.8f;

                        break;
                }
            }
        }
    }
}