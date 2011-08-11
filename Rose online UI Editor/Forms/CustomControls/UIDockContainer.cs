#region using
using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rose_online_UI_Editor.Files_Handlers;
using Rose_online_UI_Editor.Forms.CustomControls.GraphicsDeviceControl;

using Rose_online_UI_Editor.Render;
using Rose_online_UI_Editor.RoseControls;
using Rose_online_UI_Editor.Mouse;
#endregion

namespace Rose_online_UI_Editor.Forms.CustomControls
{
    class UIDockContainer : DevComponents.DotNetBar.DockContainerItem , ICustomControl
    {
            #region Variables
            public RenderControl renderControl { get; set; }
            Dialog dialog;
            private DevComponents.DotNetBar.PanelDockContainer codePanelDockContainer;
            private string XMLFolder;
            private string TSIFolder;            
            private Vector2 deviceMiddle;            
            #endregion

            #region constructor
            public UIDockContainer(string Name, string Text, string XMLFolder , string TSIFolder)
             {
              this.Name = Name;
              this.Text = Text;
              this.XMLFolder = XMLFolder;
              this.TSIFolder = TSIFolder;
              
             // 
             // renderPanel
             // 
            renderControl = new RenderControl();
            renderControl.Location = new System.Drawing.Point(3, 3);
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
            }
            #endregion

            #region public methods
            public void Load(string Path)
            {
                dialog = new Dialog();
                dialog.Load(Path); 
        
            }
            public void Reload()
            {

            }
            public void Save()
            {
               
            }
            public void Save(string Path)
            {
              
            }
            public void RenderUI()
            {
                /*this.renderControl.ClearSprites();
                this.renderControl.ClearAeras();
                this.renderControl.ClearTexts();*/              
                
            }            
            public void DrawAxis()
            {
                Rectangle rect_x = new Rectangle(-100,0,200,2);
                Aera Axe_x = new Aera(this.renderControl.GraphicsDevice,rect_x,Color.Red);
                
                Rectangle rect_y = new Rectangle(0, -100, 2, 200);
                Aera Axe_y = new Aera(this.renderControl.GraphicsDevice, rect_y, Color.Red);

                this.renderControl.listAera.Add(Axe_x);
                this.renderControl.listAera.Add(Axe_y);
            }
            public void SetZoom(float zoom)
            {
            this.renderControl.cam.Zoom = zoom;
            }
            public void SetMouseType(MouseType mType)
            {
                
            }
            #endregion

            #region Event
            private new void MouseMove(object sender, EventArgs e)
            {
              Vector2 deviceMiddle = new Vector2(this.renderControl.Width / 2, this.renderControl.Height / 2);
              System.Drawing.Point MousePosition = renderControl.PointToClient(Cursor.Position);
              SpriteFont font = this.renderControl.Content.Load<SpriteFont>("Arial");
              Text text = new Text(this.renderControl.GraphicsDevice);
              text.Set((MousePosition.X -deviceMiddle.X) + "," + (MousePosition.Y- deviceMiddle.Y), new Vector2(0,0), font, Color.Red);
              this.renderControl.ClearTexts();
              this.renderControl.AddText(text);

            }            
            #endregion
    }
}
