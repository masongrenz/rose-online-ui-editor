using System;
using System.Xml.XPath;
using Rose_online_UI_Editor.Render;
using Microsoft.Xna.Framework.Graphics;

namespace Rose_online_UI_Editor.RoseControls
{
    public class ScrollBox : Image, IDrawable, ILoadable
    {
        #region public variables
        public bool Blink {get;set;}
        public int BlinkMID {get;set;}
        public int BlinkSwapTime {get;set;}
        public int TickMove {get;set;}
        public string BlinkGID {get;set;}
        #endregion

        #region methods
        public override void Load(XPathNavigator xmlNavigator)
        {
            base.Load(xmlNavigator);
            if (xmlNavigator.GetAttribute("TICKMOVE", "") != string.Empty)
            {
                TickMove = Convert.ToInt32(xmlNavigator.GetAttribute("TICKMOVE", ""));
            }
            if (xmlNavigator.GetAttribute("BLINK", "") != string.Empty)
            {
               Blink = Convert.ToBoolean(xmlNavigator.GetAttribute("BLINK", ""));
            }
            if (xmlNavigator.GetAttribute("BLINKMID", "") != string.Empty)
            {
                BlinkMID = Convert.ToInt32(xmlNavigator.GetAttribute("BLINKMID", ""));
            }
            if (xmlNavigator.GetAttribute("BLINKGID", "") != string.Empty)
            {
               BlinkGID = xmlNavigator.GetAttribute("BLINKGID", "");
            }
            if (xmlNavigator.GetAttribute("BLINKSWAPTIME", "") != string.Empty)
            {
                BlinkSwapTime = Convert.ToInt32(xmlNavigator.GetAttribute("BLINKSWAPTIME", ""));
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {

        }
        #endregion

    }
}
