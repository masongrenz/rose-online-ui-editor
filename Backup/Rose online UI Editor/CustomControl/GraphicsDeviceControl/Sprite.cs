

namespace Rose_online_UI_Editor.CustomControl.GraphicsDeviceControl
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    class Sprite
    {
       public Texture2D texture { get; set; }
       public Vector2 position { get; set; }
       public GraphicsDevice graphicsDevice { get; set; }
       public Rectangle sourceRectangle { get; set; }
       private bool using_sourceRectangle;

       public Sprite(GraphicsDevice gDevice)
       {
           this.graphicsDevice = gDevice;
           using_sourceRectangle = false;
       }
     
       public void LoadTextureFromFile(string Path , Vector2 pos)
       {
           this.texture = Texture2D.FromFile(this.graphicsDevice,Path);
           this.position = pos;
           
       }
       public void LoadTextureFromFile(string Path, Vector2 pos, Rectangle sRectangle )
       {
           this.texture = Texture2D.FromFile(this.graphicsDevice, Path);
           this.position = pos;
           this.sourceRectangle = sRectangle;
           this.using_sourceRectangle = true;
       }


       public void Move(Vector2 newpostion)
       {
           this.position = newpostion;
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
       
        
    }
}
