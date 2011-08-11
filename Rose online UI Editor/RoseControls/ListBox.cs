using System;
using System.Xml.XPath;
using Rose_online_UI_Editor.Render;
using Microsoft.Xna.Framework.Graphics;

namespace Rose_online_UI_Editor.RoseControls
{
    public class ListBox : Caption, ILoadable, IDrawable
    {
        #region public variables
        public bool SelectAble {get;set;}
        public int CharHeight {get;set;}
        public int CharWidth {get;set;}
        public int Extent {get;set;}
        public int Font {get;set;}
        public int LineSpace {get;set;}
        public int MaxSize {get;set;}
        public int OwnerDraw {get;set;}
        #endregion

        #region methods 
        public override void Load(XPathNavigator xmlNavigator)
        {
            base.Load(xmlNavigator);
            if (xmlNavigator.GetAttribute("EXTENT", "") != string.Empty)
            {
                Extent = Convert.ToInt32(xmlNavigator.GetAttribute("EXTENT", ""));
            }
            if (xmlNavigator.GetAttribute("LINESPACE", "") != string.Empty)
            {
                LineSpace = Convert.ToInt32(xmlNavigator.GetAttribute("LINESPACE", ""));
            }
            if (xmlNavigator.GetAttribute("SELECTABLE", "") != string.Empty)
            {
                SelectAble = Convert.ToBoolean(xmlNavigator.GetAttribute("SELECTABLE", ""));
            }
            if (xmlNavigator.GetAttribute("CHARWIDTH", "") != string.Empty)
            {
                CharWidth= Convert.ToInt32(xmlNavigator.GetAttribute("CHARWIDTH", ""));
            }
            if (xmlNavigator.GetAttribute("MAXSIZE", "") != string.Empty)
            {
                MaxSize = Convert.ToInt32(xmlNavigator.GetAttribute("MAXSIZE", ""));
            }
            if (xmlNavigator.GetAttribute("OWNERDRAW", "") != string.Empty)
            {
                OwnerDraw = Convert.ToInt32(xmlNavigator.GetAttribute("OWNERDRAW", ""));
            }
             if (xmlNavigator.GetAttribute("FONT", "") != string.Empty)
            {
                Font = Convert.ToInt32(xmlNavigator.GetAttribute("FONT", ""));
            }
        
        }
        public override void Draw(SpriteBatch spriteBatch)
        {

        }
        #endregion
    }
}
