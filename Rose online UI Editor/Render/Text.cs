
namespace Rose_online_UI_Editor.Render
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    class Text
    {
        public GraphicsDevice graphicsDevice { get; set; }
        private string text;
        private Vector2 position;
        private Color color;
        private SpriteFont font;
        public bool Drawing { get; set; }

        public Text(GraphicsDevice gDevice)
       {
           Drawing = true;
           this.graphicsDevice = gDevice;
       
       }
        public void Set(string txt,Vector2 pos,SpriteFont pfont,Color col)
        {
            this.text = txt;
            this.position = pos;
            this.font = pfont;
            this.color = col;
        }
            
        public void Draw(SpriteBatch spritebatch)
        {
            if (Drawing)
            spritebatch.DrawString(font, text, position, color);
        }
    }
}
