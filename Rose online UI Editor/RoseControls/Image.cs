using System;
using System.Xml.XPath;
using Rose_online_UI_Editor.Render;
using Microsoft.Xna.Framework.Graphics;

namespace Rose_online_UI_Editor.RoseControls
{
    public class Image : Caption, ILoadable, IDrawable
    {
        #region public variables
        public bool SizeFit {get;set;}
        public float ScaleHeight {get;set;}
        public float ScaleWidth {get;set;}
        public int Alpha {get;set;}
        public int AlphaValue {get;set;}
        public int Blue {get;set;}
        public int Red {get;set;}
        public int Green {get;set;}
        public int ModuleID {get;set;}
        public int TextX {get;set;}
        public int TextY {get;set;}
        public string GID {get;set;}
        public string Text {get;set;}
        #endregion

        #region methods
        public override void Load(XPathNavigator xmlNavigator)
        {
            base.Load(xmlNavigator);
            if (xmlNavigator.GetAttribute("GID", "") != string.Empty)
            {
                GID = xmlNavigator.GetAttribute("GID", "");
            }
            if (xmlNavigator.GetAttribute("SCALEWIDTH", "") != string.Empty)
            {
                ScaleWidth = Convert.ToSingle(xmlNavigator.GetAttribute("SCALEWIDTH", ""));
            }
            if (xmlNavigator.GetAttribute("SCALEHEIGHT", "") != string.Empty)
            {
                ScaleHeight = Convert.ToSingle(xmlNavigator.GetAttribute("SCALEHEIGHT", ""));
            }
            if (xmlNavigator.GetAttribute("MODULEID", "") != string.Empty)
            {
                ModuleID = Convert.ToInt32(xmlNavigator.GetAttribute("MODULEID", ""));
            }
            if (xmlNavigator.GetAttribute("SIZEFIT", "") != string.Empty)
            {
                SizeFit = Convert.ToBoolean(xmlNavigator.GetAttribute("SIZEFIT", ""));
            }
            if (xmlNavigator.GetAttribute("A", "") != string.Empty)
            {
                Alpha = Convert.ToInt32(xmlNavigator.GetAttribute("A", ""));
            }
            if (xmlNavigator.GetAttribute("R", "") != string.Empty)
            {
                Red = Convert.ToInt32(xmlNavigator.GetAttribute("R", ""));
            }
            if (xmlNavigator.GetAttribute("B", "") != string.Empty)
            {
                Blue = Convert.ToInt32(xmlNavigator.GetAttribute("B", ""));
            }
            if (xmlNavigator.GetAttribute("G", "") != string.Empty)
            {
                Green = Convert.ToInt32(xmlNavigator.GetAttribute("G", ""));
            }
            if (xmlNavigator.GetAttribute("TEXT", "") != string.Empty)
            {
                Text = xmlNavigator.GetAttribute("TEXT", "");
            }
            if (xmlNavigator.GetAttribute("TEXTX", "") != string.Empty)
            {
                TextX = Convert.ToInt32(xmlNavigator.GetAttribute("TEXTX", ""));
            }
            if (xmlNavigator.GetAttribute("TEXTY", "") != string.Empty)
            {
                TextY = Convert.ToInt32(xmlNavigator.GetAttribute("TEXTY", ""));
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {

        }
        #endregion
    }
}
