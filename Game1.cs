using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace NiklasGame
{
    public class Game1 : Game
    {
        private SpriteBatch _spriteBatch;
        private EntityManager _entityManager;
        private Scoreboard _scoreboard;
        private float _powerupTtl = 1000;
        public GameObject[] Players;

        public static GraphicsDeviceManager GraphicsDeviceManager;


        public Game1()
        {
            GraphicsDeviceManager = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
        }

        public static Rectangle ViewPortBounds => GraphicsDeviceManager.GraphicsDevice.Viewport.Bounds;

        protected override void Initialize()
        {
            _entityManager = new EntityManager(Content);
            var vp = GraphicsDeviceManager.GraphicsDevice.Viewport.Bounds;
            _scoreboard = new Scoreboard()
            {
                Position = new Vector2(vp.Width / 2f, 50)
            };
            var playerY = (vp.Height / 2);

            Players = new GameObject[]
            {
                new Pad()
                {
                    Position = new Vector2(25, playerY)
                },
                new Pad()
                {
                    KeyUp = Keys.Up,
                    KeyDown = Keys.Down,
                    Position = new Vector2(vp.Width - 25, playerY)
                }
            };

            var ball = new Ball(_scoreboard, new Vector2(vp.Width / 2f, vp.Height / 2f));

            _entityManager.Add(Players);
            _entityManager.Add( ball, _scoreboard);
            _entityManager.Add(new Edge(Edge.Side.Top, vp), new Edge(Edge.Side.Bottom, vp),
                new Edge(Edge.Side.Left, vp), new Edge(Edge.Side.Right, vp));
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDeviceManager.GraphicsDevice);
        }

        protected override void UnloadContent()
        {
        }

        private void PupPower(Pad pad) => pad.MorePower();
        private void PupSize(Pad pad) => pad.IncreaseSize();

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back ==
                ButtonState.Pressed || Keyboard.GetState().IsKeyDown(
                    Keys.Escape))
                Exit();

            _powerupTtl -= gameTime.ElapsedGameTime.Milliseconds;

            if (_powerupTtl < 1 && Players != null)
            {
                _powerupTtl = 1000;
                Action<Pad> action = gameTime.ElapsedGameTime.Milliseconds % 2 == 0
                    ? PupPower
                    : PupSize;
                _entityManager.Add(new PowerUp(action,Players));
            }

            _entityManager.Update(gameTime);

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDeviceManager.GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _entityManager.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}