﻿using System;
using System.Xml.XPath;
using Rose_online_UI_Editor.Render;
using Microsoft.Xna.Framework.Graphics;

namespace Rose_online_UI_Editor.RoseControls
{
    class RadioBox : Caption , ILoadable , IDrawable
    {

        #region methods
        public override void Load(XPathNavigator xmlNavigator)
        {
            base.Load(xmlNavigator);
        }
       public override void Draw(SpriteBatch spriteBatch)
        {

        }
        #endregion
    }
}
