using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Rose_online_UI_Editor.Content_Manager;

namespace Rose_online_UI_Editor.Render
{
    public class Sprite : IDrawable
    {

        #region variables
        public Texture2D texture { get; set; }
        public Vector2 position { get; set; }        
        public Rectangle sourceRectangle { get; set; }
        private bool using_sourceRectangle;
        #endregion

        #region constructor
        public Sprite()
        {            
            using_sourceRectangle = false;
        }
        #endregion

        #region methods
        public void LoadTextureFromFile(string path, Vector2 pos)
        {
            texture = ContentManager.Instance().GetTexture(path);
            position = pos;

        }
        public void LoadTextureFromFile(string path, Vector2 pos, Rectangle sRectangle)
        {
            texture = ContentManager.Instance().GetTexture(path);
            position = pos;
            sourceRectangle = sRectangle;
            using_sourceRectangle = true;
        }


        public void Move(Vector2 newpostion)
        {
            position = newpostion;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (!using_sourceRectangle)
            {
                spriteBatch.Draw(texture, position, Color.White);
            }

            else
            {
                spriteBatch.Draw(texture, position, this.sourceRectangle, Color.White);
            }
        }
        #endregion


    }
}
