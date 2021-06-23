using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace NiklasGame
{
    public class Scoreboard: GameObject
    {
        private SpriteFont _spriteFont;
        public int Player1Score { get; set; }
        public int Player2Score { get; set; }

        public float LastGameTime;

        public override void LoadContent(ContentManager content)
        {
            _spriteFont = content.Load<SpriteFont>("HUD");
        }

        public override void Update(GameTime gameTime)
        {
            LastGameTime = gameTime.ElapsedGameTime.Milliseconds;
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var textToPrint = $"{Player1Score} : {Player2Score}";
            var size = _spriteFont.MeasureString(textToPrint);
            var position = Position - new Vector2(size.X / 2, size.Y / 2);
            
            spriteBatch.DrawString(_spriteFont, textToPrint, position, Color.Black);
        }

        public void IncreasePlayerScore(Edge.Side side)
        {
            switch (side)
            {
                case Edge.Side.Top:
                    break;
                case Edge.Side.Bottom:
                    break;
                case Edge.Side.Left:
                    Player2Score++;
                    break;
                case Edge.Side.Right:
                    Player1Score++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(side), side, null);
            }
            
        }
        
        
    }
}