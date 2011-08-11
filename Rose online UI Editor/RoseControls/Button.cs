using System;
using System.Xml.XPath;
using Rose_online_UI_Editor.Render;
using Microsoft.Xna.Framework.Graphics;

namespace Rose_online_UI_Editor.RoseControls
{
    public class Button : Image, IDrawable, ILoadable
    {
        #region public variables
        public bool NoImage { get; set; }
        public int Alpha {get;set;}
        public int Red {get;set;}
        public int Blue {get;set;}
        public int Green {get;set;}
        public int ClickSoundID {get;set;}
        public int OverSoundID {get;set;}
        public int TextX{get;set;}
        public int TextY{get;set;}
        public string BlinkGID{get;set;}
        public string DisableGID{get;set;}
        public string DownGID {get;set;}
        public string NormalGID {get;set;}
        public string OverGID{get;set;}
        public string Text{get;set;}
        #endregion

        #region methods
        public override void Load(XPathNavigator xmlNavigator)
        {
            base.Load(xmlNavigator);
            if (xmlNavigator.GetAttribute("NORMALGID", "") != string.Empty)
            {
                NormalGID = xmlNavigator.GetAttribute("NORMALGID", "");
            }
            if (xmlNavigator.GetAttribute("OVERGID", "") != string.Empty)
            {
                OverGID = xmlNavigator.GetAttribute("OVERGID", "");
            }
            if (xmlNavigator.GetAttribute("DOWNGID", "") != string.Empty)
            {
               DownGID = xmlNavigator.GetAttribute("DOWNGID", "");
            }
            if (xmlNavigator.GetAttribute("BLINKGID", "") != string.Empty)
            {
                BlinkGID = xmlNavigator.GetAttribute("BLINKGID", "");
            }
            if (xmlNavigator.GetAttribute("DISABLEGID", "") != string.Empty)
            {
                DisableGID = xmlNavigator.GetAttribute("DISABLEGID", "");
            }
            if (xmlNavigator.GetAttribute("OVERSID", "") != string.Empty)
            {
                OverSoundID = Convert.ToInt32(xmlNavigator.GetAttribute("OVERSID", ""));
            }
            if (xmlNavigator.GetAttribute("CLICKSID", "") != string.Empty)
            {
                ClickSoundID = Convert.ToInt32(xmlNavigator.GetAttribute("CLICKSID", ""));
            }
            if (xmlNavigator.GetAttribute("NOIMAGE", "") != string.Empty)
            {
                NoImage = Convert.ToBoolean(xmlNavigator.GetAttribute("NOIMAGE", ""));
            }            
        }
        public override void Draw(SpriteBatch spriteBatch)
        {

        }
        #endregion

    }
}
