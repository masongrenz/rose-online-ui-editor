using System;
using System.Xml.XPath;
using Rose_online_UI_Editor.Render;
using Microsoft.Xna.Framework.Graphics;

namespace Rose_online_UI_Editor.RoseControls
{
    public class RadioButton : Button, IDrawable, ILoadable
    {
        #region public variables
        public int DisableSID { get; set; }
        public int Ghost { get; set; }
        public int OutRadioBox { get; set; }
        #endregion

        #region methods
        public override void Load(XPathNavigator xmlNavigator)
        {
            base.Load(xmlNavigator);
            if (xmlNavigator.GetAttribute("RADIOBOXID", "") != string.Empty)
            {
                OutRadioBox = Convert.ToInt32(xmlNavigator.GetAttribute("RADIOBOXID", ""));
            }
            if (xmlNavigator.GetAttribute("GHOST", "") != string.Empty)
            {
                Ghost = Convert.ToInt32(xmlNavigator.GetAttribute("GHOST", ""));
            }
            if (xmlNavigator.GetAttribute("DISABLESID", "") != string.Empty)
            {
                DisableSID = Convert.ToInt32(xmlNavigator.GetAttribute("DISABLESID", ""));
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {

        }
        #endregion
    }
}
