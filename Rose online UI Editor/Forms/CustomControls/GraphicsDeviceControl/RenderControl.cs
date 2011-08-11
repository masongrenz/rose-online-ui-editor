#region using
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Rose_online_UI_Editor.Render;
#endregion

namespace Rose_online_UI_Editor.Forms.CustomControls.GraphicsDeviceControl
{
  

    class RenderControl : GraphicsDeviceControl
    {
        public ContentManager Content { get; set; }
        SpriteBatch spriteBatch;
        public Camera2d cam { get; set; }
        public List<Sprite> listSprite { get; set; }
        public List<Aera> listAera { get; set; }
        public List<Text> listText { get; set; }
        /// <summary>
        /// Initializes the control.
        /// </summary>
        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Content = new ContentManager(Services, "Content");
            cam = new Camera2d();
            listSprite = new List<Sprite>();
            listAera = new List<Aera>();
            listText = new List<Text>();
            // Hook the idle event to constantly redraw our animation.
            Application.Idle += delegate { Invalidate(); };
        }
       
        /// <summary>
        /// Disposes the control, unloading the ContentManager.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Content.Unload();
            }

            base.Dispose(disposing);
        }

        public void AddSprite(Sprite sprite)
        {
            listSprite.Add(sprite);
        }
        public void ClearSprites()
        {
            listSprite.Clear();
        }

        public void AddAera(Aera aera)
        {
            this.listAera.Add(aera);
        }
        public void ClearAeras()
        {
            this.listAera.Clear();
        }

        public void AddText(Text text)
        {
            this.listText.Add(text);
        }
        public void ClearTexts()
        {
            this.listText.Clear();
        }

        /// <summary>
        /// Draws the control.
        /// </summary>
        protected override void Draw()
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None,cam.get_transformation(this.GraphicsDevice));
            for (int i = 0; i != listSprite.Count; i++)
            {
               if(listSprite[i] != null)
                listSprite[i].Draw(this.spriteBatch);
            }
            for (int i = 0; i != listAera.Count; i++)
            {
                if(listAera[i] != null)
                listAera[i].Draw(this.spriteBatch);
            }
            for (int i = 0; i != listText.Count; i++)
            {
                if(listText[i] != null)
                listText[i].Draw(this.spriteBatch);
            }
            spriteBatch.End();
        }
    }
    public class Camera2d 
    {
        protected float _zoom; // Camera Zoom
        public Matrix _transform; // Matrix Transform
        public Vector2 _pos { get; set; } // Camera Position
        protected float _rotation; // Camera Rotation

        public Camera2d()
        {
            _zoom = 1.0f;
            _rotation = 0.0f;
            _pos = new Vector2(0, 0);
        }
        
        // Sets and gets zoom
        public float Zoom
        {
            get { return _zoom; }
            set { _zoom = value; if (_zoom == 0.1f) _zoom = 0.1f; } // Negative zoom will flip image
        }

        // Auxiliary function to move the camera
        public void Move(Vector2 amount)
        {
            _pos += amount;
        }
        // Get set position
        
        public Matrix get_transformation(GraphicsDevice graphicsDevice)
        {
            _transform =  Matrix.CreateTranslation(new Vector3(-_pos.X, -_pos.Y, 0)) *
                                         Matrix.CreateRotationZ(_rotation) *
                                         Matrix.CreateScale(new Vector3(Zoom, Zoom, 0)) *
                                         Matrix.CreateTranslation(new Vector3(0, 0, 0));
            return _transform;
        }
    }
}
