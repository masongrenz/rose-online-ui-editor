using System;
using System.Xml.XPath;
using Rose_online_UI_Editor.Render;
using Microsoft.Xna.Framework.Graphics;

namespace Rose_online_UI_Editor.RoseControls
{
 public class EditBox : Caption , ILoadable , IDrawable
 {
     #region public variables
     public bool HideCursor {get;set;}
     public bool Multline {get;set;}
     public bool Number {get;set;}
     public bool Password {get;set;}
     public int CharHeight {get;set;}
     public int CharWidth {get;set;}
     public int Font {get;set;}
     public int LimitText {get;set;}
     public int TextAlign {get;set;}
     public int TextColor {get;set;}
     #endregion

     #region methods
     public override void Load(XPathNavigator xmlNavigator)
     {
         base.Load(xmlNavigator);
         if (xmlNavigator.GetAttribute("CHARWIDTH", "") != string.Empty)
         {
             CharWidth = Convert.ToInt32(xmlNavigator.GetAttribute("CHARWIDTH", ""));
         }
         if (xmlNavigator.GetAttribute("CHARHEIGHT", "") != string.Empty)
         {
             CharHeight = Convert.ToInt32(xmlNavigator.GetAttribute("CharHeight", ""));
         }
         if (xmlNavigator.GetAttribute("NUMBER", "") != string.Empty)
         {
             Number = Convert.ToBoolean(xmlNavigator.GetAttribute("NUMBER", ""));
         }
         if (xmlNavigator.GetAttribute("LIMITTEXT", "") != string.Empty)
         {
             LimitText = Convert.ToInt32(xmlNavigator.GetAttribute("LIMITTEXT", ""));
         }
         if (xmlNavigator.GetAttribute("PASSWORD", "") != string.Empty)
         {
             Password = Convert.ToBoolean(xmlNavigator.GetAttribute("PASSWORD", ""));
         }
         if (xmlNavigator.GetAttribute("HIDECURSOR", "") != string.Empty)
         {
             HideCursor = Convert.ToBoolean(xmlNavigator.GetAttribute("HIDECURSOR", ""));
         }
         if (xmlNavigator.GetAttribute("TEXTALIGN", "") != string.Empty)
         {
             TextAlign = Convert.ToInt32(xmlNavigator.GetAttribute("TEXTALIGN", ""));
         }
         if (xmlNavigator.GetAttribute("MULTILINE", "") != string.Empty)
         {
             Multline = Convert.ToBoolean(xmlNavigator.GetAttribute("MULTILINE", ""));
         }
         if (xmlNavigator.GetAttribute("TEXTCOLOR", "") != string.Empty)
         {
             TextColor = Convert.ToInt32(xmlNavigator.GetAttribute("TEXTCOLOR", ""));
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
