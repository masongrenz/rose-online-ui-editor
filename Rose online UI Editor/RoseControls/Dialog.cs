using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.XPath;
using Rose_online_UI_Editor.Render;
using Rose_online_UI_Editor.Forms;
using Microsoft.Xna.Framework.Graphics;



namespace Rose_online_UI_Editor.RoseControls
{
    class Dialog : CtrlBase , IDrawable
    {
        #region public variable
        public List<Caption> listElement;
        
        public bool? DefaultVisible{get;set;}
        public bool? Extent { get; set; }
        public bool? Modal { get; set; }
        public int? DefaultPosX { get; set; }
        public int? DefaultPosY { get; set; }
        public int? DefaultAdjustPosX { get; set; }
        public int? DefaultAdjustPosY { get; set; }
        public int? ShowSoundID { get; set; }
        public int? HideSoundID { get; set; }
        #endregion

        #region constructors
        public Dialog()
        {
            listElement = new List<Caption>();
        }
        #endregion

        #region public methods
        public void Load(string filePath)
        {
            try
            {
                StatusManager.AddLog("Starting loading XML : " + filePath);
                XPathDocument xmlFile = new XPathDocument(File.Open(filePath, FileMode.Open));
                XPathNavigator xmlNavigator = xmlFile.CreateNavigator();
                xmlNavigator.MoveToRoot();
                xmlNavigator.MoveToFirstChild();

                if (xmlNavigator.Name == "Root_Element")
                {
                    base.Load(xmlNavigator);

                    if (xmlNavigator.GetAttribute("DEFAULT_VISIBLE", "") != string.Empty)
                    {
                        DefaultVisible = Convert.ToBoolean(xmlNavigator.GetAttribute("DEFAULTVISIBLE", ""));
                    }
                    if (xmlNavigator.GetAttribute("EXTENT", "") != string.Empty)
                    {
                        Extent = Convert.ToBoolean(xmlNavigator.GetAttribute("EXTENT", ""));
                    }
                    if (xmlNavigator.GetAttribute("Modal", "") != string.Empty)
                    {
                        Modal = Convert.ToBoolean(xmlNavigator.GetAttribute("Modal", ""));
                    }
                    if (xmlNavigator.GetAttribute("DEFAULT_X", "") != string.Empty)
                    {
                        DefaultPosX = Convert.ToInt32(xmlNavigator.GetAttribute("DEFAULT_X", ""));
                    }
                    if (xmlNavigator.GetAttribute("DEFAULT_Y", "") != string.Empty)
                    {
                        DefaultPosY = Convert.ToInt32(xmlNavigator.GetAttribute("DEFAULT_Y", ""));
                    }
                    if (xmlNavigator.GetAttribute("ADJUST_X", "") != string.Empty)
                    {
                        DefaultAdjustPosX = Convert.ToInt32(xmlNavigator.GetAttribute("ADJUST_X", ""));
                    }
                    if (xmlNavigator.GetAttribute("ADJUST_Y", "") != string.Empty)
                    {
                        DefaultAdjustPosY = Convert.ToInt32(xmlNavigator.GetAttribute("ADJUST_Y", ""));
                    }
                    if (xmlNavigator.GetAttribute("SHOWSID", "") != string.Empty)
                    {
                        ShowSoundID = Convert.ToInt32(xmlNavigator.GetAttribute("SHOWSID", ""));
                    }
                    if (xmlNavigator.GetAttribute("HIDESID", "") != string.Empty)
                    {
                        HideSoundID = Convert.ToInt32(xmlNavigator.GetAttribute("HIDESID", ""));
                    }                
                    if (xmlNavigator.HasChildren)
                    {
                     xmlNavigator.MoveToFirstChild();
                     listElement.Add(ElementConstructor.CreateElement(xmlNavigator));
                     while(xmlNavigator.MoveToNext())
                     {
                         listElement.Add(ElementConstructor.CreateElement(xmlNavigator));
                     }
                    }
                    StatusManager.AddLog("XML Successfully loaded");                    
                }
                else
                {
                    StatusManager.AddLog("No \"Root_Element\" in XML file");
                }
                
            }
            catch (Exception e)
            {
                StatusManager.AddLog("Error loading XML : " + e.Message);
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            
        }
        #endregion


        #region private methods
        
        #endregion

    }
}
