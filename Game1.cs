using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NiklasGame
{
    public class Game1 : Game
    {
        private SpriteBatch spriteBatch;
        private EntityManager entityManager;
        private Scoreboard scoreboard;
        public static GraphicsDeviceManager GraphicsDeviceManager;

        public Game1()
        {
            GraphicsDeviceManager = new GraphicsDeviceManager(this);
           
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {   
            entityManager = new EntityManager(Content);
            var vp = GraphicsDeviceManager.GraphicsDevice.Viewport.Bounds;
            scoreboard = new Scoreboard()
            {
                Position = new Vector2(vp.Width / 2f, 50)
            };
            var playerY = (vp.Height / 2);

            var pad1 = new Pad()
            {
                Position = new Vector2(25, playerY)
            };

            var pad2 = new Pad()
            {
                KeyUp = Keys.Up,
                KeyDown = Keys.Down,
                Position = new Vector2(vp.Width - 25, playerY)
            };

            var ball = new Ball(scoreboard, new Vector2(vp.Width / 2f, vp.Height / 2f), pad1, pad2);

            entityManager.Add(pad1, pad2, ball, new Edge(Edge.Side.Top, vp), new Edge(Edge.Side.Bottom, vp),
                new Edge(Edge.Side.Left, vp), new Edge(Edge.Side.Right, vp), scoreboard);

            base.Initialize();
        }

        //todo: Momentum to vector, Pads vector.Y, ball both, bounds.intersect, collision on roof etc,  


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDeviceManager.GraphicsDevice);
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

            entityManager.Update(gameTime);

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDeviceManager.GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            entityManager.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}