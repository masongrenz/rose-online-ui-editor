using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rose_online_UI_Editor.Files_Handlers;
using Rose_online_UI_Editor.CustomControl.GraphicsDeviceControl;
namespace Rose_online_UI_Editor.CustomControl
{
   partial class TSIDockContainer : DevComponents.DotNetBar.DockContainerItem
    {
        #region Variables
       public RenderControl renderControl { get; set; }
            private DevComponents.DotNetBar.PanelDockContainer codePanelDockContainer;
            private string TSIFolder;
            public string TSIPath { get; set; }
            public TSI tsi { get; set; }
          
        #endregion
            public TSIDockContainer(string Name, string Text, string TSIFolder )
             {
                 this.Name = Name;
                 this.Text = Text;
                 this.TSIFolder = TSIFolder;
             // 
             // renderPanel
             // 
            renderControl = new RenderControl();
            renderControl.Location = new System.Drawing.Point(3, 1);
            renderControl.Dock = DockStyle.Fill;
            renderControl.Name = "renderControl";
            renderControl.Size = new System.Drawing.Size(657, 423);
            renderControl.TabIndex = 0;
            renderControl.MouseMove += new MouseEventHandler(MouseMove);
            
            codePanelDockContainer = new DevComponents.DotNetBar.PanelDockContainer();
            // 
            // codePanelDockContainer
            // 
            codePanelDockContainer.Controls.Add(renderControl);
            codePanelDockContainer.Location = new System.Drawing.Point(3, 28);
            codePanelDockContainer.Dock = DockStyle.Fill;
            codePanelDockContainer.Name = "codePanelDockContainer1";
            codePanelDockContainer.Size = new System.Drawing.Size(663, 427);
            codePanelDockContainer.Style.Alignment = System.Drawing.StringAlignment.Center;
            codePanelDockContainer.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            codePanelDockContainer.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            codePanelDockContainer.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            codePanelDockContainer.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            codePanelDockContainer.Style.GradientAngle = 90;
            codePanelDockContainer.TabIndex = 0;

            this.Control = codePanelDockContainer;
            tsi = new TSI();
            }

                            
            public void LoadTSI(string Path)
            {
                tsi.Load(Path);
                this.TSIPath = Path;
            }

            public void Save()
            {
                this.tsi.Save(TSIPath);
            }
            public void Save(string Path)
            {
                this.tsi.Save(Path);
            }

            public void DrawDDS(int DDSIndex)
            {
                
                string DDSPath = tsi.listDDS[DDSIndex].Path;
                Sprite sprite = new Sprite(this.renderControl.GraphicsDevice);
                sprite.LoadTextureFromFile(TSIFolder + "\\" + DDSPath , new Vector2(0,0));
                this.renderControl.ClearSprites();
                this.renderControl.ClearAeras();
                this.renderControl.AddSprite(sprite);
            }

            public void DrawAera(int DDSIndex , int ElementIndex )
            {
                Rectangle rectangle = new Rectangle(this.tsi.listDDS[DDSIndex].ListDDS_element[ElementIndex].X, this.tsi.listDDS[DDSIndex].ListDDS_element[ElementIndex].Y, this.tsi.listDDS[DDSIndex].ListDDS_element[ElementIndex].Width, this.tsi.listDDS[DDSIndex].ListDDS_element[ElementIndex].Height);
                Aera aera = new Aera(this.renderControl.GraphicsDevice, rectangle, Color.Red);
                this.renderControl.ClearAeras();
                this.renderControl.AddAera(aera);
            }

        #region Event
            private void MouseMove(object sender, EventArgs e)
            {
              System.Drawing.Point MousePosition = renderControl.PointToClient(Cursor.Position);
              SpriteFont font = this.renderControl.Content.Load<SpriteFont>("Arial");
              Text text = new Text(this.renderControl.GraphicsDevice);
              text.Set(MousePosition.X + "," + MousePosition.Y, new Vector2(0,0), font, Color.Red);
              this.renderControl.ClearTexts();
              this.renderControl.AddText(text);

            }
        #endregion
    }
}
