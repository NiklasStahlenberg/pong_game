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
            Bounds = new Rectangle(0,0, 10, 100);
        }

        public override void Initialize()
        {
            Texture = new Texture2D(Game1.GraphicsDevice.GraphicsDevice, Bounds.Width, Bounds.Height);
            Color[] colorData = new Color[Bounds.Width * Bounds.Height];
            for (int i = 0; i < Bounds.Width * Bounds.Height; i++)
                colorData[i] = Color.White;

            Texture.SetData(colorData);
        }

      
        public override void Update(GameTime gameTime)
        {
           GetDirectionInput(gameTime);
           OutOfBound();
           base.Update(gameTime);
        }

        private void GetDirectionInput(GameTime gameTime)
        {
            var keyBoardState = Keyboard.GetState();
            var keyWasDown = false;

            if (keyBoardState.IsKeyDown(KeyUp))
            {
                Momentum -= 0.01f;
                keyWasDown = true;
            }

            if (keyBoardState.IsKeyDown(KeyDown))
            {
                Momentum += 0.01f;
                keyWasDown = true;
            }

            if (keyWasDown == false)
            {
                Momentum *= 0.99f;
            }
        }

        public void OutOfBound()
        {
            var y = Position.Y;
            var wpHeight = Game1.GraphicsDevice.GraphicsDevice.Viewport.Height;

            if (y + Bounds.Height > wpHeight)
            {
                Position.Y = wpHeight - Bounds.Height;
                Momentum = -Momentum * 0.8f;
            }

            if (y < 0)
            {
                Position.Y = 0;
                Momentum = -Momentum * 0.8f;
            }
                
        }
    }
}