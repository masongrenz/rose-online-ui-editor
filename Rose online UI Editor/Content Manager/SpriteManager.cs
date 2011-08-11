using System;
using System.Collections;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Rose_online_UI_Editor.Files_Handlers;
using Rose_online_UI_Editor.Render;

namespace Rose_online_UI_Editor.Content_Manager
{
    public class SpriteManager : ICleaning
    {
        #region static variables
        static SpriteManager currentInstance;
        #endregion

        #region variables
        Hashtable sprites;
        #endregion

        #region constructors
        public SpriteManager()
        {
            sprites = new Hashtable();
        }
        public static SpriteManager Instance()
        {
            if (currentInstance == null)
            {
                currentInstance = new SpriteManager();
            }
            return currentInstance;
        }
        #endregion

        #region methods
        public void GenerateSpritesFromTSI(TSI tsi)
        {
            for (int i = 0; i < tsi.listDDS.Count; i++)
            {
                Sprite newDDSSprite = new Sprite();
                newDDSSprite.LoadTextureFromFile(ContentManager.resFolderPartialPath + "\\" + tsi.listDDS[i].Path, new Vector2(0, 0));
                if (!sprites.Contains(tsi.listDDS[i].Path.ToLower()))//check this later
                sprites.Add(tsi.listDDS[i].Path.ToLower(),newDDSSprite);
                for (int j = 0; j < tsi.listDDS[i].ListDDS_element.Count; j++)
                {
                   
                    Sprite newSprite = new Sprite();
                    newSprite.LoadTextureFromFile(ContentManager.resFolderPartialPath + "\\" + tsi.listDDS[i].Path, new Vector2(0, 0),
                        new Rectangle(
                        tsi.listDDS[i].ListDDS_element[j].X,
                        tsi.listDDS[i].ListDDS_element[j].Y,
                        tsi.listDDS[i].ListDDS_element[j].Width,
                        tsi.listDDS[i].ListDDS_element[j].Height));
                    if (!sprites.Contains(tsi.listDDS[i].ListDDS_element[j].Name.ToLower()))//check this later
                    sprites.Add(tsi.listDDS[i].ListDDS_element[j].Name.ToLower(), newSprite);
                }

            }
        }
        public Sprite GetSprite(string name)
        {
            return (Sprite)sprites[name.ToLower()];
        }
        public void ClearAll()
        {
            sprites.Clear();
        }
        #endregion
    }
}
