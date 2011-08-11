using System;
using System.Xml.XPath;
using Rose_online_UI_Editor.Render;
using Microsoft.Xna.Framework.Graphics;

namespace Rose_online_UI_Editor.RoseControls
{
    public class Table : Image, IDrawable, ILoadable
    {
        #region public variables
        public int CellHeight {get;set;}
        public int CellWidth {get;set;}
        public int ColMargin {get;set;}
        public int RowMargin {get;set;}
        public int ColumnCount {get;set;}
        public int Extent {get;set;}
        #endregion

        #region methods
        public override void Load(XPathNavigator xmlNavigator)
        {
            base.Load(xmlNavigator);
            if (xmlNavigator.GetAttribute("EXTENT", "") != string.Empty)
            {
                Extent = Convert.ToInt32(xmlNavigator.GetAttribute("EXTENT", ""));
            }
            if (xmlNavigator.GetAttribute("CELLWIDTH", "") != string.Empty)
            {
                CellWidth = Convert.ToInt32(xmlNavigator.GetAttribute("CELLWIDTH", ""));
            }
            if (xmlNavigator.GetAttribute("CELLHEIGHT", "") != string.Empty)
            {
                CellHeight = Convert.ToInt32(xmlNavigator.GetAttribute("CELLHEIGHT", ""));
            }
            if (xmlNavigator.GetAttribute("COLUMNCOUNT", "") != string.Empty)
            {
                ColumnCount = Convert.ToInt32(xmlNavigator.GetAttribute("COLUMNCOUNT", ""));
            }
            if (xmlNavigator.GetAttribute("ROWMARGIN", "") != string.Empty)
            {
                RowMargin = Convert.ToInt32(xmlNavigator.GetAttribute("ROWMARGIN", ""));
            }
            if (xmlNavigator.GetAttribute("COLMARGIN", "") != string.Empty)
            {
                ColMargin = Convert.ToInt32(xmlNavigator.GetAttribute("COLMARGIN", ""));
            } 
        }
        public override void Draw(SpriteBatch spriteBatch)
        {

        }
        #endregion
    }
}
