
namespace Rose_online_UI_Editor.Render
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    class Aera
    {
        public Rectangle rectangle { get; set; }
        public Color[] color { get; set; }
        private Texture2D texture;
        public GraphicsDevice graphicsDevice { get; set; }
        public bool Drawing { get; set; }

        public Aera(GraphicsDevice gDevice , Rectangle rect , Color col)
        {
         Drawing = true;
        this.graphicsDevice = gDevice;
        this.rectangle = rect;
         
        
        texture = new Texture2D(gDevice, rectangle.Width, rectangle.Height, 1, TextureUsage.None, SurfaceFormat.Color);
        Color[] color = new Color[rectangle.Width * rectangle.Height];
        
        //set red color
            col.A = 50;
        for (int i = 0; i < color.Length; i++)
        {
            color[i] = col;
        }
        //set the limit
            col.A = 255;
        for (int i = 0; i < rectangle.Width; i++)
        {
            color[i] = col;
        }
        
        for (int i = 0; i < rectangle.Width; i++)
        {
            color[color.Length - 1 - i] = col;
        }
        for (int i = 0; i < rectangle.Height; i++)
        {
            color[rectangle.Width * i] = col;
        }
        for (int i = 0; i < rectangle.Height ; i++)
        {
            color[(rectangle.Width * i) + rectangle.Width - 1] = col;
        }

        texture.SetData(color);
        }        
                   
        public void Draw(SpriteBatch spriteBatch)
        {
            if (Drawing)
            spriteBatch.Draw(texture, new Vector2( rectangle.X,rectangle.Y),Color.White);
            
        }
    
    }
}
