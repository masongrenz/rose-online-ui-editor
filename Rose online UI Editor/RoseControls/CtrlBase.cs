using System;
using System.Xml.XPath;
using Rose_online_UI_Editor.Render;
using Microsoft.Xna.Framework.Graphics;

namespace Rose_online_UI_Editor.RoseControls
{
    public class CtrlBase : ILoadable
    {
        #region public variables
        public int? Height { get; set; }
        public int? Width { get; set; }
        public int? OffsetX { get;set; }
        public int? OffsetY { get; set; }
        public int? ParentPosX { get; set; }
        public int? ParentPosY { get; set; }
        public int? PosX {get;set;}
        public int? PosY { get; set; }
        public int? ScrX {get;set;}
        public int? ScrY { get;set; }
        #endregion

        #region public methods
       public virtual void Load(XPathNavigator xmlNavigator)
        {
            if(xmlNavigator.HasAttributes)
            {
                if (xmlNavigator.GetAttribute("HEIGHT", "") != string.Empty)
                {
                    Height = Convert.ToInt32(xmlNavigator.GetAttribute("HEIGHT", ""));
                }
                if (xmlNavigator.GetAttribute("WIDTH", "") != string.Empty)
                {
                    Width = Convert.ToInt32(xmlNavigator.GetAttribute("WIDTH", ""));
                }
                if (xmlNavigator.GetAttribute("OFFSETX", "") != string.Empty)
                {
                    OffsetX = Convert.ToInt32(xmlNavigator.GetAttribute("OFFSETX", ""));
                }
                if (xmlNavigator.GetAttribute("OFFSETY", "") != string.Empty)
                {
                    OffsetY = Convert.ToInt32(xmlNavigator.GetAttribute("OFFSETY", ""));
                }
                if (xmlNavigator.GetAttribute("PARENTPOSX", "") != string.Empty)
                {
                    ParentPosX = Convert.ToInt32(xmlNavigator.GetAttribute("PARENTPOSX", ""));
                }
                if (xmlNavigator.GetAttribute("PARENTPOSY", "") != string.Empty)
                {
                    ParentPosY = Convert.ToInt32(xmlNavigator.GetAttribute("PARENTPOSY", ""));
                }
                if (xmlNavigator.GetAttribute("POSX", "") != string.Empty)
                {
                    PosX = Convert.ToInt32(xmlNavigator.GetAttribute("POSX", ""));
                }
                if (xmlNavigator.GetAttribute("POSY", "") != string.Empty)
                {
                    PosY = Convert.ToInt32(xmlNavigator.GetAttribute("POSY", ""));
                }
                if (xmlNavigator.GetAttribute("X", "") != string.Empty)
                {
                    ScrX = Convert.ToInt32(xmlNavigator.GetAttribute("X", ""));
                }
                if (xmlNavigator.GetAttribute("Y", "") != string.Empty)
                {
                    ScrY = Convert.ToInt32(xmlNavigator.GetAttribute("Y", ""));
                }
            }            
        }
        #endregion
    }
}
