using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NiklasGame
{
    public class Pad: GameObject
    {
        public Keys KeyUp { get; set; } = Keys.W;
        public Keys KeyDown { get; set; } = Keys.S;
        public Pad()
        {
            Bounds = new Rectangle(-5,-50, 10, 100);
        }

        public override void Initialize()
        {
            Texture = new Texture2D(Game1.GraphicsDeviceManager.GraphicsDevice, Bounds.Width, Bounds.Height);
            Color[] colorData = new Color[Bounds.Width * Bounds.Height];
            for (int i = 0; i < Bounds.Width * Bounds.Height; i++)
                colorData[i] = Color.White;

            Texture.SetData(colorData);
        }

      
        public override void Update(GameTime gameTime)
        {
           GetDirectionInput(gameTime);
           base.Update(gameTime);
        }

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