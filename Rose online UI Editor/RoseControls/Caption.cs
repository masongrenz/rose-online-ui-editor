using System;
using System.Xml.XPath;
using Rose_online_UI_Editor.Render;
using Microsoft.Xna.Framework.Graphics;

namespace Rose_online_UI_Editor.RoseControls
{
    public class Caption : CtrlBase , ILoadable , IDrawable
    {
        #region public variable
        public int? ID {get;set;}
        public string Name { get; set; }
        #endregion

        #region public methods
       public override void Load(XPathNavigator xmlNavigator)
        {
          base.Load(xmlNavigator);
          if(xmlNavigator.GetAttribute("ID","") != string.Empty)
          {
              ID = Convert.ToInt32(xmlNavigator.GetAttribute("ID", ""));
          }
          if (xmlNavigator.GetAttribute("NAME", "") != string.Empty)
          {
              Name = xmlNavigator.GetAttribute("NAME", "");
          }
        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
        #endregion
    }
}
