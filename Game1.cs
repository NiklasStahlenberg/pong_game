using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NiklasGame
{
    public class Game1 : Game
    {
        private SpriteBatch spriteBatch;
        private List<GameObject> objects = new();
        public static GraphicsDeviceManager GraphicsDevice;

        public Game1()
        {
            GraphicsDevice = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            objects.Add(new GameObject()
            {
                Position = new Vector2(500, 300),
                Speed = 0.5f
            });
        }

        protected override void Initialize()
        {
            objects.Add(new Pad()
            {
                Position = new Vector2(20,100)
            });
            
            objects.Add(new Pad()
            {
                KeyUp = Keys.Up,
                KeyDown = Keys.Down,
                Position = new Vector2(GraphicsDevice.GraphicsDevice.Viewport.Width - 30, 100)
            });
            
            foreach (var obj in objects)
            {
                obj.Initialize();
            }

            base.Initialize();
        }

        //todo: Momentum to vector, Pads vector.Y, ball both, bounds.intersect, collision on roof etc,  
        
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice.GraphicsDevice);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back ==
                ButtonState.Pressed || Keyboard.GetState().IsKeyDown(
                    Keys.Escape))
                Exit();

            foreach (var obj in objects)
            {
                obj.Update(gameTime);
            }

            base.Update(gameTime);
        }
        

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            foreach (var obj in objects)
            {
                obj.Draw(spriteBatch); 
            }
            
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}