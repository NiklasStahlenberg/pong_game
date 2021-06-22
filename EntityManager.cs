using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace NiklasGame
{
    public class EntityManager
    {
        private readonly ContentManager content;
        private List<GameObject> gameObjects = new();
        private List<GameObject> addList = new();

        public EntityManager(ContentManager content)
        {
            this.content = content;
        }

        public void Add(GameObject gameObject)
        {
            addList.Add(gameObject);
        }

        public void Add(params GameObject[] objects)
        {
            foreach (var gameObject in objects)
            {
                Add(gameObject);       
            }
        }

        public void Update(GameTime gameTime)
        {
            var toDelete = new HashSet<GameObject>();
            foreach (var obj in addList)
            {
                gameObjects.Add(obj);
                obj.LoadContent(content);
                obj.Initialize();
            }
            addList.Clear();
            
            foreach (var gameObject in gameObjects)
            {
                gameObject.Update(gameTime);
                if (gameObject.ShouldBeDeleted)
                    toDelete.Add(gameObject);
            }

            foreach (var a in gameObjects)
            {
                foreach (var b in gameObjects)
                {
                    if (a == b)
                    {
                        continue;
                    }

                    if (a.GetWorldBounds().Intersects(b.GetWorldBounds()))
                    {
                        a.OnCollision(b);
                    }
                }
            }

            foreach (var obj in toDelete)
            {
                gameObjects.Remove(obj);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var obj in gameObjects)
            {
                obj.Draw(spriteBatch); 
            }
        }

    }
}