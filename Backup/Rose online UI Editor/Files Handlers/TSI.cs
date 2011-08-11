using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rose_online_UI_Editor.Files_Handlers.Common;

namespace Rose_online_UI_Editor.Files_Handlers
{
    class TSI
    {
        #region variables
        private string Path;
        public List<DDS> listDDS { get; set; }
        private RoseFileHandler FileHandler;
        #endregion

        public TSI()
        {
         listDDS = new List<DDS>();
        }

        public void Load(string filePath)
        {
        this.Path = filePath;
        FileHandler = new RoseFileHandler (filePath , FileMode.Open,Encoding.GetEncoding("EUC-KR"));
        short DDSCount = FileHandler.Read<short>();
        this.listDDS = new List<DDS>(DDSCount);
        for (int i = 0; i < DDSCount; i++)
        {
            DDS newDDS = new DDS();
            newDDS.Path = FileHandler.ReadSString();
            newDDS.ColourKey = FileHandler.Read<int>();
            this.listDDS.Add(newDDS);
        }

        short TotalementCount = FileHandler.Read<short>();

        for (int i = 0; i < DDSCount; i++)
        {
            short ElementCount = FileHandler.Read<short>();
            listDDS[i].ListDDS_element = new List<DDS.DDSElement>(ElementCount);
            for (int a = 0; a < ElementCount; a++)
            {
                DDS.DDSElement newElement = new DDS.DDSElement();
                newElement.OwnerId = FileHandler.Read<short>();
                newElement.X = FileHandler.Read<int>();
                newElement.Y = FileHandler.Read<int>();
                newElement.Width = FileHandler.Read<int>() - newElement.X;
                newElement.Height = FileHandler.Read<int>() - newElement.Y;
                newElement.Color = FileHandler.Read<int>();
                newElement.Name = FileHandler.Read<string>(0x20);
                listDDS[i].ListDDS_element.Add(newElement);
            }
        
        
        }
        FileHandler.Dispose();
        }

        public void Reload()
        {
            Load(this.Path);
        }

        public void Save(string filePath)
        {
            this.Path = filePath;
            FileHandler = new RoseFileHandler(filePath, FileMode.Create, Encoding.GetEncoding("EUC-KR"));
            FileHandler.Write<short>((short)listDDS.Count);
            int totalElementCount = 0;
            listDDS.ForEach(delegate (DDS dds){

               FileHandler.WriteSString(dds.Path);
               FileHandler.Write<int>(dds.ColourKey);
            totalElementCount += dds.ListDDS_element.Count;
            });

            FileHandler.Write<short>((short)totalElementCount);

            listDDS.ForEach(delegate (DDS dds){
                FileHandler.Write<short>((short)dds.ListDDS_element.Count);
                dds.ListDDS_element.ForEach(delegate(DDS.DDSElement dds_element)
                {
                    FileHandler.Write<short>(dds_element.OwnerId);
                    FileHandler.Write<int>(dds_element.X);
                    FileHandler.Write<int>(dds_element.Y);
                    FileHandler.Write<int>(dds_element.Width+ dds_element.X);
                    FileHandler.Write<int>(dds_element.Height + dds_element.Y);
                    FileHandler.Write<int>(dds_element.Color);
                    FileHandler.Write<string>(dds_element.Name);
                });
            });
            FileHandler.Dispose();
        }

        public void Save()
        {
            Save(this.Path);
        }

        public class DDS
        {

            public DDS()
            {
                ListDDS_element = new List<DDSElement>();
            }
            
            #region Variables
            Texture2D texture;

            [Description("Enter the path of DDS") ,Category("DDS"),DisplayName("Name of dds :")]
             public string Path {get;set;}

            [Description("Enter the color key"), Category("DDS"), DisplayName("Color Key :")]
             public int ColourKey {get; set;}

            public List<DDSElement> ListDDS_element { get; set; }
            
            #endregion
                       
            public class DDSElement
            {
                public DDSElement()
                {
                
                }
                public DDSElement(short OwnerId, int X, int Y, int Width, int Height, int Color, string Name)
                {
                    this.OwnerId = OwnerId;
                    this.X = X;
                    this.Y = Y;
                    this.Width = Width;
                    this.Height = Height;
                    this.Color = Color;
                    this.Name = Name;
                }
                [Description("Select owner id"), Category("1) Element"), DisplayName("Owner id :")]
                public short OwnerId { get; set; }


                [Description("Enter X cord"), Category("2) Coord")]
                public int X { get; set; }

                [Description("Enter Y cord"), Category("2) Coord")]
                public int Y { get; set; }

                [Description("Enter the widht"), Category("3) Dimension")]
                public int Width { get; set; }


                [Description("Enter the height"), Category("3) Dimension")]
                public int Height { get; set; }

                [Description("Entrer the color"), Category("1) Element"), DisplayName("Color :")]
                public int Color { get; set; }

                [Description("Enter the name (32 caractères maximum)"), Category("1) Element"), DisplayName("Name :")]
                public string Name { get; set; }

               
            }
        }
    
    
    
    }
}
