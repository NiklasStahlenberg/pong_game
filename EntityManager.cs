using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace NiklasGame
{
    public class EntityManager
    {
        private readonly ContentManager _content;
        private List<GameObject> _gameObjects = new();
        private List<GameObject> _addList = new();

        public EntityManager(ContentManager content)
        {
            this._content = content;
        }

        public void Add(GameObject gameObject)
        {
            _addList.Add(gameObject);
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
            foreach (var obj in _addList)
            {
                _gameObjects.Add(obj);
                obj.LoadContent(_content);
                obj.Initialize();
            }
            _addList.Clear();
            
            foreach (var gameObject in _gameObjects)
            {
                gameObject.Update(gameTime);
                if (gameObject.ShouldBeDeleted)
                    toDelete.Add(gameObject);
            }

            foreach (var a in _gameObjects)
            {
                foreach (var b in _gameObjects)
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
                _gameObjects.Remove(obj);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var obj in _gameObjects)
            {
                obj.Draw(spriteBatch); 
            }
        }

    }
}