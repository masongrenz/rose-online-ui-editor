#region using
using System;
using System.Collections;

using Microsoft.Xna.Framework.Graphics;

using Rose_online_UI_Editor.Files_Handlers;
using Rose_online_UI_Editor.Files_Handlers.Common;

#endregion

namespace Rose_online_UI_Editor.Content_Manager
{
    public class ContentManager : ICleaning
    {
        #region static variables
        private static string rootPath;
        private static string controlFolderPath;
        private static string resFolderPath;
        private static string xmlFolderPath;
        static ContentManager currentInstance;
        static GraphicsDevice device;
        public const string resFolderPartialPath = "\\3DDATA\\CONTROL\\RES";
        public const string xmlFolderPartialPath = "\\3DDATA\\CONTROL\\XML";
        #endregion 
        #region variables        
        Hashtable Textures;
        Hashtable TSIs;        
        #endregion
        
        #region constructors
        public ContentManager()
        {           
            Textures = new Hashtable();
            TSIs = new Hashtable();
        }
        public static ContentManager Instance()
        {
            if(currentInstance == null) 
            {
                currentInstance = new ContentManager();
            }
            return currentInstance;
        }
        #endregion 

        #region methods        
        public Texture2D GetTexture(string name)
        {
            if (!Textures.Contains(name))
            {
                Texture2D texture = Texture2D.FromFile(device, rootPath + name);
                Textures.Add(name, texture);
                return texture;
            }
            else
            {
                return (Texture2D)Textures[name];
            }
        }
        public TSI GetTSI(string name)
        {
            return GetTSI(name, rootPath);
        }
        public TSI GetTSI(string name, string folderPath)
        {

            if (!TSIs.Contains(name))
            {
                TSI tsi = new TSI();
                tsi.Load(folderPath + name, ClientType.IROSE);
                TSIs.Add(name, tsi);
                return tsi;
            }
            else
            {
                return (TSI)TSIs[name];
            }
        }
        public void ClearAll()
        {
            
            /*for (IDictionaryEnumerator i = Textures.GetEnumerator(); i < Textures.Count;i++)
            {
             
                (Texture)(Textures[i]).Dispose();         
            }*/
            Textures.Clear();
            TSIs.Clear();
        }       
        #endregion

        #region static method
        public static void SetRootPath(string path)
        {
            rootPath = path;
        }
        public static string GetRootPath()
        {
            return rootPath;
        }
        public static void SetControlFolderPath(string path)
        {
            controlFolderPath = path;
        }
        public static string GetControlFolderPath()
        {
            return controlFolderPath;
        }
        public static void SetResFolderPath(string path)
        {
            resFolderPath = path;
        }
        public static string GetResFolderPath()
        {
            return resFolderPath;
        }
        public static void SetXMLFolderPath(string path)
        {
            xmlFolderPath = path;
        }
        public static string GetXMLFolderPath()
        {
            return xmlFolderPath;
        }
        public static void SetGraphicsDevice(GraphicsDevice gdevice)
        {
            device = gdevice;
        }
        #endregion


    }
}
