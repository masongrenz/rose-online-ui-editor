using System;
using System.Xml.XPath;
using Rose_online_UI_Editor.Render;
using Microsoft.Xna.Framework.Graphics;

namespace Rose_online_UI_Editor.RoseControls
{
   public class ZListBox : Image , IDrawable, ILoadable
   {
       #region public variables
       public int Extent{get;set;}
       #endregion

       #region methods
       public override void Load(XPathNavigator xmlNavigator)
       {
           base.Load(xmlNavigator);
           if (xmlNavigator.GetAttribute("EXTENT", "") != string.Empty)
           {
               Extent = Convert.ToInt32(xmlNavigator.GetAttribute("EXTENT", ""));
           }          
       }
       public override void Draw(SpriteBatch spriteBatch)
       {

       }
       #endregion

   }
}
