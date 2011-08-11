using System;
using System.Xml.XPath;
using Rose_online_UI_Editor.Render;
using Microsoft.Xna.Framework.Graphics;

namespace Rose_online_UI_Editor.RoseControls
{
    public class CheckBox : Image, IDrawable, ILoadable
    {
        #region public variables
        public string CheckGID { get; set; }
        public string UnCheckGID { get; set; }
        #endregion

        #region methods
        public override void Load(XPathNavigator xmlNavigator)
        {
            base.Load(xmlNavigator);
            if (xmlNavigator.GetAttribute("CHECKGID", "") != string.Empty)
            {
                CheckGID = xmlNavigator.GetAttribute("CHECKGID", "");
            }
            if (xmlNavigator.GetAttribute("UNCHECKGID", "") != string.Empty)
            {
                UnCheckGID = xmlNavigator.GetAttribute("UNCHECKGID", "");
            } 
        }
        public override void Draw(SpriteBatch spriteBatch)
        {

        }
        #endregion
    }
}
