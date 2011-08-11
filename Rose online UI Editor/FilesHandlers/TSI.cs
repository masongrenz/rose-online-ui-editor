using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Rose_online_UI_Editor.Files_Handlers.Common;

namespace Rose_online_UI_Editor.Files_Handlers
{
    public class TSI : IReadable , ISavable
    {
        #region Variables
        private string Path;
        public List<DDS> listDDS { get; set; }
        public ClientType clientType { get; set; }
        #endregion

        #region Constructors
        public TSI()
        {
         listDDS = new List<DDS>();
        }
        #endregion

        #region Methods
        public void Load(string filePath)
         {
             Load(filePath, ClientType.IROSE);
         }

        public void Load(string filePath, ClientType clientType)
        {
        StatusManager.AddLog("Starting loading TSI : " + filePath);
        try
        {
            this.Path = filePath;
            this.clientType = clientType;
            BinaryReader br = new BinaryReader(File.Open(filePath, FileMode.Open));
            short DDSCount = br.ReadInt16();
            this.listDDS = new List<DDS>(DDSCount);
            for (int i = 0; i < DDSCount; i++)
            {
                DDS newDDS = new DDS();
                newDDS.Path = RoseFile.ReadSString(ref br);
                newDDS.ColourKey = br.ReadInt32();
                this.listDDS.Add(newDDS);
            }

            short TotalementCount = br.ReadInt16();

            for (int i = 0; i < DDSCount; i++)
            {
                short ElementCount = br.ReadInt16();
                listDDS[i].ListDDS_element = new List<DDS.DDSElement>(ElementCount);
                for (int a = 0; a < ElementCount; a++)
                {
                    DDS.DDSElement newElement = new DDS.DDSElement();
                    newElement.OwnerId = br.ReadInt16();
                    newElement.X = br.ReadInt32();
                    newElement.Y = br.ReadInt32();
                    newElement.Width = br.ReadInt32() - newElement.X;
                    newElement.Height = br.ReadInt32() - newElement.Y;
                    newElement.Color = br.ReadInt32();
                    newElement.Name = RoseFile.ReadFString(ref br, 0x20);
                    listDDS[i].ListDDS_element.Add(newElement);
                }


            }
            br.Close();
            StatusManager.AddLog("TSI successfully loaded");
        }
        catch(Exception e)
        {
            StatusManager.AddLog("Error loading TSI : " + e.Message);
        }
           
        }

        public void Reload()
        {
            Load(this.Path);
        }

        public void Save(string filePath)
        {
            try
            {
                this.Path = filePath;
                BinaryWriter bw = new BinaryWriter(File.Open(filePath, FileMode.Create));
                bw.Write((short)listDDS.Count);
                int totalElementCount = 0;
                listDDS.ForEach(delegate(DDS dds)
                {

                    RoseFile.WriteSString(ref bw, dds.Path);
                    bw.Write(dds.ColourKey);
                    totalElementCount += dds.ListDDS_element.Count;
                });

                bw.Write((short)totalElementCount);

                listDDS.ForEach(delegate(DDS dds)
                {
                    bw.Write((short)dds.ListDDS_element.Count);
                    dds.ListDDS_element.ForEach(delegate(DDS.DDSElement dds_element)
                    {
                        bw.Write((short)dds_element.OwnerId);
                        bw.Write((int)dds_element.X);
                        bw.Write((int)dds_element.Y);
                        bw.Write((int)dds_element.Width + dds_element.X);
                        bw.Write((int)dds_element.Height + dds_element.Y);
                        bw.Write((int)dds_element.Color);
                        RoseFile.WriteFString(ref bw, dds_element.Name, 0x20);
                    });
                });
                bw.Close();
                StatusManager.AddLog("TSI successfully saved");
            }
            catch(Exception e)
            {
                StatusManager.AddLog("Error saving TSI : " + e.Message);
            }
        }

        public void Save()
        {
            Save(this.Path);
        }

        public void GenerateSprites()
        {

        }
        #endregion

        #region sub classes DDS
        public class DDS : ICloneable
        {
            #region Constructors
            public DDS()
            {
                ListDDS_element = new List<DDSElement>();
            }
            public object Clone()
            {
                DDS newDDS = this.MemberwiseClone() as DDS;
                Array tmp;
                this.ListDDS_element.CopyTo(tmp);
                newDDS.ListDDS_element = new List<DDSElement>(tmp);
               
                return 
            }
            #endregion

            #region Variables
            Texture2D texture;

            [Description("Enter the path of DDS") ,Category("DDS"),DisplayName("Name of dds :")]
             public string Path {get;set;}

            [Description("Enter the color key"), Category("DDS"), DisplayName("Color Key :")]
             public int ColourKey {get; set;}

            public List<DDSElement> ListDDS_element { get; set; }
            
            #endregion

            #region sub classes DDSElement
            public class DDSElement : ICloneable
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
            #endregion
        }


        #endregion
    }

}
