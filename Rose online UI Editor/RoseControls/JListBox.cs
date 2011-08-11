using System;
using System.Xml.XPath;
using Rose_online_UI_Editor.Render;
using Microsoft.Xna.Framework.Graphics;

namespace Rose_online_UI_Editor.RoseControls
{
    class JListBox : Image, IDrawable, ILoadable
    {
        #region public variables
        public int ItemHeight {get;set;}
        public int OwnerDraw {get;set;}
        #endregion

        #region methods
        public override void Load(XPathNavigator xmlNavigator)
        {
            base.Load(xmlNavigator);
            if (xmlNavigator.GetAttribute("ITEMHEIGHT", "") != string.Empty)
            {
                ItemHeight = Convert.ToInt32(xmlNavigator.GetAttribute("ITEMHEIGHT", ""));
            }
            if (xmlNavigator.GetAttribute("OWNERDRAW", "") != string.Empty)
            {
                OwnerDraw = Convert.ToInt32(xmlNavigator.GetAttribute("OWNERDRAW", ""));
            }  
        }
        public override void Draw(SpriteBatch spriteBatch)
        {

        }
        #endregion
    }
}
