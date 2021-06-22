using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace NiklasGame
{
    public class PowerUp: GameObject
    {
        public PowerUp(params GameObject[] gameObjects)
        {
            Bounds = new Rectangle(-8, -8, 16, 16);
            var selectedPlayer = gameObjects[RandomHelper.GetNext(0, gameObjects.Length)];
            var plane = selectedPlayer.Position.X;
            Position = GetPosition(selectedPlayer.GetWorldBounds(), plane);
        }

        private Vector2 GetPosition(Rectangle playerBounds, float xPlane)
        {
            var y = RandomHelper.GetNext(0, Game1.ViewPortBounds.Height);
            if (playerBounds.Contains(xPlane, y))
                return GetPosition(playerBounds, xPlane);
            return new Vector2(xPlane, y);
        }

        public override void LoadContent(ContentManager content)
        {
            Texture = content.Load<Texture2D>("powerup");
        }

        public override void OnCollision(GameObject collidesWith)
        {
            if (collidesWith is Pad pup)
            {
                var afterPowerUpEnds = pup.IncreaseSize();
                MarkForDeletion();
                Task.Delay(5000).ContinueWith((_) => afterPowerUpEnds());
            }
            base.OnCollision(collidesWith);
        }
    }
}