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

        private const int initialHeight = 100;

        public Pad()
        {
            Bounds = new Rectangle(-5, -initialHeight / 2, 10, initialHeight);
        }

        public override void Initialize()
        {
            Texture = new Texture2D(Game1.GraphicsDeviceManager.GraphicsDevice, Bounds.Width, Bounds.Height).Fill(Color.White);
        }


        public override void Update(GameTime gameTime)
        {
            UpdateBounds();
            GetDirectionInput(gameTime);
            base.Update(gameTime);
        }

        private void UpdateBounds()
        {
            double h = 0;
            foreach (var animation in Animations)
            {
                h += (pupSize * Math.Sin(animation.Position * Math.PI));
            }

            additionalHeight = (int)h;

            if (additionalHeight != 0)
            {
                Bounds.Height = initialHeight + additionalHeight;
            }
        }

        public override Rectangle GetWorldBounds() => Bounds.WithPosition(Position, 0, (int)(-additionalHeight / 2d));

        private void GetDirectionInput(GameTime gameTime)
        {
            var vp = Game1.ViewPortBounds;
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

            var hh = Bounds.Height / 2;
            if (Position.Y < hh)
            {
                Position.Y = hh;
            }

            if (Position.Y > vp.Height - hh)
            {
                Position.Y = vp.Height - hh;
            }
        }

        private int additionalHeight = 0;

        private const float pupSize = 15;

        public Action IncreaseSize()
        {
            Animate(5000, (_) => { });
            return () => { };
        }

        public override void OnCollision(GameObject collidesWith)
        {
            if (collidesWith is Edge edge)
            {
                switch (edge.EdgeSide)
                {
                    case Edge.Side.Top:
                        Direction.Y = -Direction.Y * 0.8f;
                        Position.Y = -Bounds.Top;
                        break;
                    case Edge.Side.Bottom:
                        Direction.Y = -Direction.Y * 0.8f;
                        Position.Y = Game1.GraphicsDeviceManager.GraphicsDevice.Viewport.Height + Bounds.Top;
                        break;
                }
            }
        }
    }
}