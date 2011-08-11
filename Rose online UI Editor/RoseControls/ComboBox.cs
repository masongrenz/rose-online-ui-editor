using System;
using System.Xml.XPath;
using Rose_online_UI_Editor.Render;
using Microsoft.Xna.Framework.Graphics;

namespace Rose_online_UI_Editor.RoseControls
{
    class ComboBox : Caption, IDrawable, ILoadable
    {
        #region public variables
        public bool OwnerDraw {get;set;}   
        #endregion

        #region methods
        public override void Load(XPathNavigator xmlNavigator)
        {
            base.Load(xmlNavigator);
            if (xmlNavigator.GetAttribute("OWNERDRAW", "") != string.Empty)
            {
                OwnerDraw = Convert.ToBoolean(xmlNavigator.GetAttribute("OWNERDRAW", ""));
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {

        }
        #endregion
    }
}
