using System;
using System.Xml.XPath;
using Rose_online_UI_Editor.Render;
using Microsoft.Xna.Framework.Graphics;

namespace Rose_online_UI_Editor.RoseControls
{
   public class Guage : Image, IDrawable, ILoadable
    {
        #region public variables
        public string BGID {get;set;}
        #endregion

        #region methods
        public override void Load(XPathNavigator xmlNavigator)
        {
            base.Load(xmlNavigator);
            if (xmlNavigator.GetAttribute("BGID", "") != string.Empty)
            {
               BGID = xmlNavigator.GetAttribute("BGID", "");
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {

        }
        #endregion
    }
}
