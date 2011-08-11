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
    class UIDockContainer : DevComponents.DotNetBar.DockContainerItem
    {
            #region Variables
            public RenderControl renderControl { get; set; }
            private DevComponents.DotNetBar.PanelDockContainer codePanelDockContainer;
            private string XMLFolder;
            private string TSIFolder;
            public string TSIPath { get; set; }
            public UI ui { get; set; }
            private Dictionary<string,TSI.DDS.DDSElement> UI_DDSElement;
            private Dictionary<string,TSI.DDS.DDSElement> EXUI_DDSElement;
            TSI UI_tsi;
            TSI EXUI_tsi;
            private Vector2 deviceMiddle;
            
     #endregion
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

            this.Control = codePanelDockContainer;
            ui = new UI();
            UI_tsi = new TSI();
            EXUI_tsi = new TSI();
            UI_tsi.Load(TSIFolder + "\\UI.TSI");
            EXUI_tsi.Load(TSIFolder + "\\EXUI.TSI");
            UI_DDSElement = new Dictionary<string,TSI.DDS.DDSElement>();
            EXUI_DDSElement = new Dictionary<string,TSI.DDS.DDSElement>();
            for (int i = 0; i < UI_tsi.listDDS.Count; i++)
            {
                for (int a = 0; a < UI_tsi.listDDS[i].ListDDS_element.Count; a++)
                {
                    UI_DDSElement.Add(UI_tsi.listDDS[i].ListDDS_element[a].Name.Trim('\0'), UI_tsi.listDDS[i].ListDDS_element[a]);
                }
            }
            for (int i = 0; i < EXUI_tsi.listDDS.Count; i++)
            {
                for (int a = 0; a < EXUI_tsi.listDDS[i].ListDDS_element.Count; a++)
                {
                    EXUI_DDSElement.Add(EXUI_tsi.listDDS[i].ListDDS_element[a].Name.Trim('\0'), EXUI_tsi.listDDS[i].ListDDS_element[a]);
                }
            }

            }


            public void LoadUI(string Path)
            {
                try
                {

                    ui.Load(Path);
                    this.TSIPath = Path;
                }
                catch
                {
                }
             }

            public void Save()
            {
               
            }
            public void Save(string Path)
            {
              
            }
            public void RenderUI()
            {
                
                    this.renderControl.ClearSprites();
                    this.renderControl.ClearAeras();
                    this.renderControl.ClearTexts();
                    DrawAxis();
                    RenderAllButtons();
                    RenderAllImages();
                    RenderAllEditBoxs();
                    RenderAllCheckBoxs();
                    RenderAllRadioBoxs();
                    RenderAllCaptions();
                    RenderAllZlistBoxs();
                    RenderAllScrollBoxs();
                    RenderAllTabbedPannes();                  
                
            }

            public void RenderAllButtons()
            {
                for (int i = 0; i < ui.root_Element.listButton.Count; i++)
                {
                    Sprite newButton = new Sprite(this.renderControl.GraphicsDevice);
                    Vector2 buttonPosition = new Vector2(ui.root_Element.listButton[i].x, ui.root_Element.listButton[i].y);
                    if (ui.root_Element.listButton[i].moduleid == 0)
                    {
                        Rectangle buttonSelection = new Rectangle(UI_DDSElement[ui.root_Element.listButton[i].normalgid].X, UI_DDSElement[ui.root_Element.listButton[i].normalgid].Y, UI_DDSElement[ui.root_Element.listButton[i].normalgid].Width, UI_DDSElement[ui.root_Element.listButton[i].normalgid].Height);
                        string texturePath = TSIFolder + "\\" + UI_tsi.listDDS[UI_DDSElement[ui.root_Element.listButton[i].normalgid].OwnerId].Path;
                        newButton.LoadTextureFromFile(texturePath, buttonPosition, buttonSelection);

                    }
                    if (ui.root_Element.listButton[i].moduleid == 3)
                    {
                        Rectangle buttonSelection = new Rectangle(EXUI_DDSElement[ui.root_Element.listButton[i].normalgid].X, EXUI_DDSElement[ui.root_Element.listButton[i].normalgid].Y, EXUI_DDSElement[ui.root_Element.listButton[i].normalgid].Width, EXUI_DDSElement[ui.root_Element.listButton[i].normalgid].Height);
                        string texturePath = TSIFolder + "\\" + EXUI_tsi.listDDS[EXUI_DDSElement[ui.root_Element.listButton[i].normalgid].OwnerId].Path;
                        newButton.LoadTextureFromFile(TSIFolder + "\\" + EXUI_tsi.listDDS[EXUI_DDSElement[ui.root_Element.listButton[i].normalgid].OwnerId].Path, buttonPosition, buttonSelection);

                    }
                    renderControl.AddSprite(newButton);
                }
            }
            public void RenderAllImages()
            {
                for (int i = 0; i < ui.root_Element.listImage.Count; i++)
                {
                    Sprite newimage = new Sprite(this.renderControl.GraphicsDevice);
                    Vector2 imagePosition = new Vector2(ui.root_Element.listImage[i].x, ui.root_Element.listImage[i].y);

                    if (ui.root_Element.listImage[i].moduleid == 0)
                    {
                        Rectangle imageSelection = new Rectangle(UI_DDSElement[ui.root_Element.listImage[i].gid].X, UI_DDSElement[ui.root_Element.listImage[i].gid].Y, UI_DDSElement[ui.root_Element.listImage[i].gid].Width, UI_DDSElement[ui.root_Element.listImage[i].gid].Height);
                        string texturePath = TSIFolder + "\\" + UI_tsi.listDDS[UI_DDSElement[ui.root_Element.listImage[i].gid].OwnerId].Path;
                        newimage.LoadTextureFromFile(texturePath, imagePosition, imageSelection);

                    }
                    if (ui.root_Element.listImage[i].moduleid == 3)
                    {

                        Rectangle imageSelection = new Rectangle(EXUI_DDSElement[ui.root_Element.listImage[i].gid].X, EXUI_DDSElement[ui.root_Element.listImage[i].gid].Y, EXUI_DDSElement[ui.root_Element.listImage[i].gid].Width, EXUI_DDSElement[ui.root_Element.listImage[i].gid].Height);
                        string texturePath = TSIFolder + "\\" + EXUI_tsi.listDDS[EXUI_DDSElement[ui.root_Element.listImage[i].gid].OwnerId].Path;
                        newimage.LoadTextureFromFile(texturePath, imagePosition, imageSelection);

                    }
                    renderControl.AddSprite(newimage);
                }
            }
            public void RenderAllEditBoxs()
            {
                for (int i = 0; i < ui.root_Element.listEditBox.Count; i++)
                {
                    Rectangle editboxAera = new Rectangle(ui.root_Element.listEditBox[i].x, ui.root_Element.listEditBox[i].y, ui.root_Element.listEditBox[i].width, ui.root_Element.listEditBox[i].height);

                    Aera newEditBox = new Aera(this.renderControl.GraphicsDevice, editboxAera, Color.Yellow);
                    this.renderControl.AddAera(newEditBox);

                }
            }
            public void RenderAllCheckBoxs()
            {
                for (int i = 0; i < ui.root_Element.listCheckBox.Count; i++)
                {
                    Rectangle checkboxAera = new Rectangle(ui.root_Element.listCheckBox[i].x, ui.root_Element.listCheckBox[i].y, ui.root_Element.listCheckBox[i].width, ui.root_Element.listCheckBox[i].height);
                    Aera newCheckBox = new Aera(this.renderControl.GraphicsDevice, checkboxAera, Color.Green);
                    this.renderControl.AddAera(newCheckBox);

                }
            }
            public void RenderAllRadioBoxs()
            {
                for (int a = 0; a < ui.root_Element.listRadioBox.Count; a++)
                {
                    for (int i = 0; i < ui.root_Element.listRadioBox[a].listRadioButton.Count; i++)
                    {
                        Sprite newRadioButton = new Sprite(this.renderControl.GraphicsDevice);
                        Vector2 buttonPosition = new Vector2(ui.root_Element.listRadioBox[a].listRadioButton[i].x, ui.root_Element.listRadioBox[a].listRadioButton[i].y);
                        if (ui.root_Element.listRadioBox[a].listRadioButton[i].moduleid == 0)
                        {
                            Rectangle buttonSelection = new Rectangle(UI_DDSElement[ui.root_Element.listRadioBox[a].listRadioButton[i].normalgid].X, UI_DDSElement[ui.root_Element.listRadioBox[a].listRadioButton[i].normalgid].Y, UI_DDSElement[ui.root_Element.listRadioBox[a].listRadioButton[i].normalgid].Width, UI_DDSElement[ui.root_Element.listRadioBox[a].listRadioButton[i].normalgid].Height);
                            string texturePath = TSIFolder + "\\" + UI_tsi.listDDS[UI_DDSElement[ui.root_Element.listRadioBox[a].listRadioButton[i].normalgid].OwnerId].Path;
                            newRadioButton.LoadTextureFromFile(texturePath, buttonPosition, buttonSelection);

                        }
                        if (ui.root_Element.listRadioBox[a].listRadioButton[i].moduleid == 3)
                        {
                            Rectangle buttonSelection = new Rectangle(EXUI_DDSElement[ui.root_Element.listRadioBox[a].listRadioButton[i].normalgid].X, EXUI_DDSElement[ui.root_Element.listRadioBox[a].listRadioButton[i].normalgid].Y, EXUI_DDSElement[ui.root_Element.listRadioBox[a].listRadioButton[i].normalgid].Width, EXUI_DDSElement[ui.root_Element.listRadioBox[a].listRadioButton[i].normalgid].Height);
                            string texturePath = TSIFolder + "\\" + EXUI_tsi.listDDS[EXUI_DDSElement[ui.root_Element.listRadioBox[a].listRadioButton[i].normalgid].OwnerId].Path;
                            newRadioButton.LoadTextureFromFile(TSIFolder + "\\" + EXUI_tsi.listDDS[EXUI_DDSElement[ui.root_Element.listRadioBox[a].listRadioButton[i].normalgid].OwnerId].Path, buttonPosition, buttonSelection);

                        }
                        renderControl.AddSprite(newRadioButton);
                    }
                }
            }
            public void RenderAllCaptions()
            {
                for (int i = 0; i < ui.root_Element.listCaption.Count; i++)
                {
                    Rectangle CaptionAera = new Rectangle(ui.root_Element.listCaption[i].x, ui.root_Element.listCaption[i].y, ui.root_Element.listCaption[i].width, ui.root_Element.listCaption[i].height);

                    Aera newCaption = new Aera(this.renderControl.GraphicsDevice, CaptionAera, Color.White);
                    this.renderControl.AddAera(newCaption);

                }
            }
            public void RenderAllZlistBoxs()
            {
                for (int i = 0; i < ui.root_Element.listZListBox.Count; i++)
                {
                    Rectangle ZListBoxAera = new Rectangle(ui.root_Element.listZListBox[i].x, ui.root_Element.listZListBox[i].y, ui.root_Element.listZListBox[i].width, ui.root_Element.listZListBox[i].height);

                    Aera newZListBox = new Aera(this.renderControl.GraphicsDevice, ZListBoxAera, Color.Pink);
                    this.renderControl.AddAera(newZListBox);

                }
            }
            public void RenderAllScrollBoxs()
            {
                for (int i = 0; i < ui.root_Element.listScrollBar.Count; i++)
                {
                    Rectangle ScrollBarAera = new Rectangle(ui.root_Element.listScrollBar[i].x, ui.root_Element.listScrollBar[i].y, ui.root_Element.listScrollBar[i].width, ui.root_Element.listScrollBar[i].height);
                    Aera newScrollBar = new Aera(this.renderControl.GraphicsDevice, ScrollBarAera, Color.Pink);
                    this.renderControl.AddAera(newScrollBar);
                    if (ui.root_Element.listScrollBar[i].scrollbox != null)
                    {
                        Sprite newScrollBox = new Sprite(this.renderControl.GraphicsDevice);
                        Vector2 ScrollBoxPosition = new Vector2(ui.root_Element.listScrollBar[i].x, ui.root_Element.listScrollBar[i].y);
                        if (ui.root_Element.listScrollBar[i].scrollbox.moduleid == 0)
                        {
                            Rectangle ScrollBoxSelection = new Rectangle(UI_DDSElement[ui.root_Element.listScrollBar[i].scrollbox.gid].X, UI_DDSElement[ui.root_Element.listScrollBar[i].scrollbox.gid].Y, UI_DDSElement[ui.root_Element.listScrollBar[i].scrollbox.gid].Width, UI_DDSElement[ui.root_Element.listScrollBar[i].scrollbox.gid].Height);
                            string texturePath = TSIFolder + "\\" + UI_tsi.listDDS[UI_DDSElement[ui.root_Element.listScrollBar[i].scrollbox.gid].OwnerId].Path;
                            newScrollBox.LoadTextureFromFile(texturePath, ScrollBoxPosition, ScrollBoxSelection);
                        }
                        else if (ui.root_Element.listScrollBar[i].scrollbox.moduleid == 3)
                        {
                            Rectangle ScrollBoxSelection = new Rectangle(EXUI_DDSElement[ui.root_Element.listScrollBar[i].scrollbox.gid].X, EXUI_DDSElement[ui.root_Element.listScrollBar[i].scrollbox.gid].Y, EXUI_DDSElement[ui.root_Element.listScrollBar[i].scrollbox.gid].Width, EXUI_DDSElement[ui.root_Element.listScrollBar[i].scrollbox.gid].Height);
                            string texturePath = TSIFolder + "\\" + EXUI_tsi.listDDS[UI_DDSElement[ui.root_Element.listScrollBar[i].scrollbox.gid].OwnerId].Path;
                            newScrollBox.LoadTextureFromFile(texturePath, ScrollBoxPosition, ScrollBoxSelection);
                        }
                        this.renderControl.AddSprite(newScrollBox);
                    }
                    this.renderControl.AddAera(newScrollBar);
                }
            }
            public void RenderAllTabbedPannes()
            {
                for (int i = 0; i < ui.root_Element.listTabbedPanne.Count; i++)
                {
                    if (ui.root_Element.listTabbedPanne[i].listTab.Count >= 1)
                    {
                        for (int a = 0; a < ui.root_Element.listTabbedPanne[i].listTab[0].listButton.Count;a++ )
                        {
                            Sprite newButton = new Sprite(this.renderControl.GraphicsDevice);
                            Vector2 buttonPosition = new Vector2(ui.root_Element.listTabbedPanne[i].listTab[0].listButton[a].offsetx + ui.root_Element.listTabbedPanne[i].x, ui.root_Element.listTabbedPanne[i].listTab[0].listButton[a].offsety + ui.root_Element.listTabbedPanne[i].y);
                            if (ui.root_Element.listTabbedPanne[i].listTab[0].listButton[a].moduleid == 0)
                            {
                                Rectangle buttonSelection = new Rectangle(UI_DDSElement[ui.root_Element.listTabbedPanne[i].listTab[0].listButton[a].normalgid].X, UI_DDSElement[ui.root_Element.listTabbedPanne[i].listTab[0].listButton[a].normalgid].Y, UI_DDSElement[ui.root_Element.listTabbedPanne[i].listTab[0].listButton[a].normalgid].Width, UI_DDSElement[ui.root_Element.listTabbedPanne[i].listTab[0].listButton[a].normalgid].Height);
                                string texturePath = TSIFolder + "\\" + UI_tsi.listDDS[UI_DDSElement[ui.root_Element.listTabbedPanne[i].listTab[0].listButton[a].normalgid].OwnerId].Path;
                                newButton.LoadTextureFromFile(texturePath, buttonPosition, buttonSelection);

                            }
                            if (ui.root_Element.listButton[i].moduleid == 3)
                            {
                                Rectangle buttonSelection = new Rectangle(EXUI_DDSElement[ui.root_Element.listTabbedPanne[i].listTab[0].listButton[a].normalgid].X, EXUI_DDSElement[ui.root_Element.listTabbedPanne[i].listTab[0].listButton[a].normalgid].Y, EXUI_DDSElement[ui.root_Element.listTabbedPanne[i].listTab[0].listButton[a].normalgid].Width, EXUI_DDSElement[ui.root_Element.listTabbedPanne[i].listTab[0].listButton[a].normalgid].Height);
                                string texturePath = TSIFolder + "\\" + EXUI_tsi.listDDS[EXUI_DDSElement[ui.root_Element.listTabbedPanne[i].listTab[0].listButton[a].normalgid].OwnerId].Path;
                                newButton.LoadTextureFromFile(texturePath, buttonPosition, buttonSelection);

                            }
                            renderControl.AddSprite(newButton);
                        }
                        for (int a = 0; a < ui.root_Element.listTabbedPanne[i].listTab[0].listImage.Count; a++)
                        {
                            Sprite newimage = new Sprite(this.renderControl.GraphicsDevice);
                            Vector2 imagePosition = new Vector2(ui.root_Element.listTabbedPanne[i].listTab[0].listImage[a].x + ui.root_Element.listTabbedPanne[i].x, ui.root_Element.listTabbedPanne[i].listTab[0].listImage[a].y+ ui.root_Element.listTabbedPanne[i].y);

                            if (ui.root_Element.listTabbedPanne[i].listTab[0].listImage[a].moduleid == 0)
                            {
                                Rectangle imageSelection = new Rectangle(UI_DDSElement[ui.root_Element.listTabbedPanne[i].listTab[0].listImage[a].gid].X, UI_DDSElement[ui.root_Element.listTabbedPanne[i].listTab[0].listImage[a].gid].Y, UI_DDSElement[ui.root_Element.listTabbedPanne[i].listTab[0].listImage[a].gid].Width, UI_DDSElement[ui.root_Element.listTabbedPanne[i].listTab[0].listImage[a].gid].Height);
                                string texturePath = TSIFolder + "\\" + UI_tsi.listDDS[UI_DDSElement[ui.root_Element.listTabbedPanne[i].listTab[0].listImage[a].gid].OwnerId].Path;
                                newimage.LoadTextureFromFile(texturePath, imagePosition, imageSelection);

                            }
                            if (ui.root_Element.listTabbedPanne[i].listTab[0].listImage[a].moduleid == 3)
                            {
                                Rectangle imageSelection = new Rectangle(EXUI_DDSElement[ui.root_Element.listTabbedPanne[i].listTab[0].listImage[a].gid].X, EXUI_DDSElement[ui.root_Element.listTabbedPanne[i].listTab[0].listImage[a].gid].Y, EXUI_DDSElement[ui.root_Element.listTabbedPanne[i].listTab[0].listImage[a].gid].Width, EXUI_DDSElement[ui.root_Element.listTabbedPanne[i].listTab[0].listImage[a].gid].Height);
                                string texturePath = TSIFolder + "\\" + EXUI_tsi.listDDS[UI_DDSElement[ui.root_Element.listTabbedPanne[i].listTab[0].listImage[a].gid].OwnerId].Path;
                                newimage.LoadTextureFromFile(texturePath, imagePosition, imageSelection);

                            }
                            renderControl.AddSprite(newimage);
                        }

                    }
                }
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
            #region Event
        private void MouseMove(object sender, EventArgs e)
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
