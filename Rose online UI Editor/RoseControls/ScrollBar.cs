using System;
using System.Xml.XPath;
using Rose_online_UI_Editor.Render;
using Microsoft.Xna.Framework.Graphics;

namespace Rose_online_UI_Editor.RoseControls
{
   public class ScrollBar : Caption , IDrawable , ILoadable
    {
        #region public variables
        public int ListBoxID {get;set;}
        public int Type {get;set;}
        #endregion

        #region methods
        public override void Load(XPathNavigator xmlNavigator)
        {
            base.Load(xmlNavigator);
            if (xmlNavigator.GetAttribute("LISTBOXID", "") != string.Empty)
            {
                ListBoxID = Convert.ToInt32(xmlNavigator.GetAttribute("LISTBOXID", ""));
            }
            if (xmlNavigator.GetAttribute("TYPE", "") != string.Empty)
            {
                Type = Convert.ToInt32(xmlNavigator.GetAttribute("TYPE", ""));
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {

        }
        #endregion
    }
}
