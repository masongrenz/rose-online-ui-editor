using System;
using System.Windows.Forms;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Rose_online_UI_Editor.Files_Handlers;
using Rose_online_UI_Editor.Forms.CustomControls.GraphicsDeviceControl;
using Rose_online_UI_Editor.Render;
using Rose_online_UI_Editor.Content_Manager;
using Rose_online_UI_Editor.Forms;
using Rose_online_UI_Editor.Command_Manager;
using Rose_online_UI_Editor.Mouse;

namespace Rose_online_UI_Editor.Forms.CustomControls
{
    public class TSIDockContainer : DevComponents.DotNetBar.DockContainerItem, ICustomControl, IUseCommand
    {
        #region variables
        private RenderControl renderControl;
        private DevComponents.DotNetBar.PanelDockContainer codePanelDockContainer;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private string TSIPath;
        private TSI tsi;
        private DevComponents.AdvTree.AdvTree TSItree;
        private CommandManager cmdManager;
        private int selectedDDSIndex;//DDS won't be reload if you select another element in the same DDS (logic , isn't it :D)
        private SpriteFont font;
        private Text positionText;
        private bool resizing = false;
        private Aera aera;
        private Sprite dds;
        public MouseType mouseType;
        public CommandManager getManager() { return cmdManager; }
        #endregion

        #region constructors
        public TSIDockContainer(string name, string text, DevComponents.AdvTree.AdvTree tree, System.Windows.Forms.PropertyGrid propGrid)
        {
            cmdManager = new CommandManager();
            selectedDDSIndex = -1;
            TSItree = tree;
            propertyGrid = propGrid;
            this.Name = name;
            this.Text = text;
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
            renderControl.MouseUp += new MouseEventHandler(MouseUp);
            renderControl.MouseDown += new MouseEventHandler(MouseDown);

            codePanelDockContainer = new DevComponents.DotNetBar.PanelDockContainer();
            // 
            // codePanelDockContainer
            // 
            codePanelDockContainer.Controls.Add(renderControl);
            codePanelDockContainer.Location = new System.Drawing.Point(3, 28);
            codePanelDockContainer.Dock = DockStyle.Fill;
            codePanelDockContainer.Name = "codePanelDockContainer";
            codePanelDockContainer.Size = new System.Drawing.Size(663, 427);
            codePanelDockContainer.Style.Alignment = System.Drawing.StringAlignment.Center;
            codePanelDockContainer.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            codePanelDockContainer.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            codePanelDockContainer.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            codePanelDockContainer.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            codePanelDockContainer.Style.GradientAngle = 90;
            codePanelDockContainer.TabIndex = 0;
            this.Control = codePanelDockContainer;

            //mouse position render
            font = this.renderControl.Content.Load<SpriteFont>("Arial");
            positionText = new Text(this.renderControl.GraphicsDevice);
            positionText.Set(0 + "," + 0, new Vector2(0, 0), font, Color.Red);
            renderControl.AddText(positionText);
            mouseType = MouseType.MOUSE_POINT;

            renderControl.AddAera(aera);
            renderControl.AddSprite(dds);
        }
        #endregion

        #region methods
        public void Load(string path)
        {
            TSIPath = path;
            tsi = ContentManager.Instance().GetTSI(path);
            GenerateTree();
        }
        public void Reload()
        {
            Load(TSIPath);
        }
        public void Save()
        {
            this.tsi.Save(TSIPath);
        }
        public void Save(string Path)
        {
            this.tsi.Save(Path);
        }

        /// <summary>
        /// This function generate tree from the tsi file 
        /// </summary>
        private void GenerateTree()
        {
            TSItree.Nodes.Clear();
            tsi.listDDS.ForEach(delegate(TSI.DDS dds)
            {
                DevComponents.AdvTree.Node newDDSNode = new DevComponents.AdvTree.Node();
                newDDSNode.Name = dds.Path;
                newDDSNode.Text = dds.Path;

                dds.ListDDS_element.ForEach(delegate(TSI.DDS.DDSElement ddsElement)
                {
                    DevComponents.AdvTree.Node newDDSElementNode = new DevComponents.AdvTree.Node();
                    newDDSElementNode.Name = ddsElement.Name;
                    newDDSElementNode.Text = ddsElement.Name;
                    newDDSNode.Nodes.Add(newDDSElementNode);
                });
                TSItree.Nodes.Add(newDDSNode);
            });
        }

        public TSI.DDS GetDDS(int ddsIndex)
        {
            return tsi.listDDS[ddsIndex];
        }

        public TSI.DDS.DDSElement GetSprite(int ddsIndex, int spriteIndex)
        {
            return tsi.listDDS[ddsIndex].ListDDS_element[spriteIndex];
        }

        #region Commands
        /// <summary>
        /// Selects the DDS.
        /// </summary>
        /// <param name="DDSIndex">Index of the DDS.</param>
        public void SelectDDS(int DDSIndex)
        {
            propertyGrid.SelectedObject = tsi.listDDS[DDSIndex];
            if (selectedDDSIndex != DDSIndex)
            {
                selectedDDSIndex = DDSIndex;
                Sprite DDSSprite = SpriteManager.Instance().GetSprite(tsi.listDDS[DDSIndex].Path);
                renderControl.ClearSprites();
                renderControl.AddSprite(DDSSprite);
            }
            renderControl.ClearAeras();
        }

        public void SelectSprite(int DDSIndex, int spriteIndex)
        {
            propertyGrid.SelectedObject = tsi.listDDS[DDSIndex].ListDDS_element[spriteIndex];
            if (selectedDDSIndex != DDSIndex)
            {
                selectedDDSIndex = DDSIndex;
                Sprite DDSSprite = SpriteManager.Instance().GetSprite(tsi.listDDS[DDSIndex].Path);
                renderControl.ClearSprites();
                renderControl.AddSprite(DDSSprite);
            }

            Rectangle rectangle = new Rectangle(this.tsi.listDDS[DDSIndex].ListDDS_element[spriteIndex].X,
                this.tsi.listDDS[DDSIndex].ListDDS_element[spriteIndex].Y,
                this.tsi.listDDS[DDSIndex].ListDDS_element[spriteIndex].Width,
                this.tsi.listDDS[DDSIndex].ListDDS_element[spriteIndex].Height);
            Aera aera = new Aera(this.renderControl.GraphicsDevice, rectangle, Color.Red);
            this.renderControl.ClearAeras();
            this.renderControl.AddAera(aera);

        }

        /// <summary>
        ///   Add DDS called "name" in both TSI and the tree and return the index of this new DDS
        /// </summary>
        public int AddDDS(string name)
        {
            TSI.DDS newDDS = new TSI.DDS();
            newDDS.Path = name;
            tsi.listDDS.Add(newDDS);
            GenerateTree();
            return TSItree.Nodes.Count - 1;
        }

        /// <summary>
        ///   Add a specified DDS in both TSI and the tree and return the index of this new DDS
        /// </summary>
        /// <param name="dds">a specified dds</param>
        public int AddDDS(TSI.DDS dds, int index)
        {
            tsi.listDDS.Insert(index, dds);
            GenerateTree();
            return TSItree.Nodes.Count - 1;
        }

        /// <summary>
        ///   Remove the DDS at position "index" then return this DDS for CommandManager
        /// </summary>
        public TSI.DDS RemoveDDS(int index)
        {
            TSI.DDS removedTSI = tsi.listDDS[index];
            tsi.listDDS.RemoveAt(index);
            GenerateTree();
            return removedTSI;
        }

        /// <summary>
        ///   Add a sprite called "spriteName" in a DDS (ref DDSIndex) at "spriteIndex"
        /// </summary>
        public void AddSprite(string spriteName, int DDSIndex, int spriteIndex)
        {
            tsi.listDDS[DDSIndex].ListDDS_element.Insert(spriteIndex, new TSI.DDS.DDSElement((short)DDSIndex, 0, 0, 5, 5, 0, spriteName));
            TSItree.Nodes[DDSIndex].Nodes.Insert(spriteIndex, new DevComponents.AdvTree.Node() { Text = spriteName, Name = spriteName });
        }

        /// <summary>
        ///   Remove a Sprite in DDS (ref DDSIndex) at "spriteIndex"
        /// </summary>
        public TSI.DDS.DDSElement RemoveSprite(int DDSIndex, int spriteIndex)
        {
            TSI.DDS.DDSElement sprite = tsi.listDDS[DDSIndex].ListDDS_element[spriteIndex];
            tsi.listDDS[DDSIndex].ListDDS_element.RemoveAt(spriteIndex);
            TSItree.Nodes[DDSIndex].Nodes.RemoveAt(spriteIndex);
            return sprite;
        }

        /// <summary>
        ///   Add "sprite" in DDS (ref DDSIndex) at "spriteIndex"
        /// </summary>
        public void AddSprite(TSI.DDS.DDSElement sprite, int DDSIndex, int spriteIndex)
        {
            tsi.listDDS[DDSIndex].ListDDS_element.Insert(spriteIndex, sprite);
            TSItree.Nodes[DDSIndex].Nodes.Insert(spriteIndex, new DevComponents.AdvTree.Node() { Text = sprite.Name, Name = sprite.Name });
        }

        /// <summary>
        ///   Set "sprite" in DDS (ref DDSIndex) at "spriteIndex"
        /// </summary>
        public void SetSprite(TSI.DDS.DDSElement sprite, int DDSIndex, int spriteIndex)
        {
            tsi.listDDS[DDSIndex].ListDDS_element[spriteIndex] = sprite;
            TSItree.Nodes[DDSIndex].Nodes[spriteIndex].Name = sprite.Name;
            TSItree.Nodes[DDSIndex].Nodes[spriteIndex].Text = sprite.Name;
        }

        /// <summary>
        ///   Set "dds" at index "DDSIndex"
        /// </summary>
        public void SetDDS(TSI.DDS dds, int DDSIndex)
        {
            tsi.listDDS[DDSIndex] = dds;
            TSItree.Nodes[DDSIndex].Name = dds.Path;
            TSItree.Nodes[DDSIndex].Text = dds.Path;
            TSItree.Nodes[DDSIndex].Nodes.Clear();

            dds.ListDDS_element.ForEach(delegate(TSI.DDS.DDSElement element)
            {
                DevComponents.AdvTree.Node newElementNode = new DevComponents.AdvTree.Node();
                newElementNode.Name = element.Name;
                newElementNode.Text = element.Name;
                TSItree.Nodes[DDSIndex].Nodes.Add(newElementNode);
            });
        }

        public void ElementChanged(int DDSIndex, int spriteIndex)
        {
            TSItree.Nodes[DDSIndex].Nodes[spriteIndex].Name = tsi.listDDS[DDSIndex].ListDDS_element[spriteIndex].Name;
            TSItree.Nodes[DDSIndex].Nodes[spriteIndex].Text = tsi.listDDS[DDSIndex].ListDDS_element[spriteIndex].Name;

            Rectangle rectangle = new Rectangle(this.tsi.listDDS[DDSIndex].ListDDS_element[spriteIndex].X,
                this.tsi.listDDS[DDSIndex].ListDDS_element[spriteIndex].Y,
                this.tsi.listDDS[DDSIndex].ListDDS_element[spriteIndex].Width,
                this.tsi.listDDS[DDSIndex].ListDDS_element[spriteIndex].Height);
            Aera aera = new Aera(this.renderControl.GraphicsDevice, rectangle, Color.Red);
            this.renderControl.ClearAeras();
            this.renderControl.AddAera(aera);
        }

        public void DDSChanged(int DDSIndex)
        {
            TSItree.Nodes[DDSIndex].Name = tsi.listDDS[DDSIndex].Path;
            TSItree.Nodes[DDSIndex].Text = tsi.listDDS[DDSIndex].Path;

            Sprite DDSSprite = SpriteManager.Instance().GetSprite(tsi.listDDS[DDSIndex].Path);
            renderControl.ClearSprites();
            renderControl.AddSprite(DDSSprite);
        }

        public void SetMouseType(MouseType mType)
        {
            mouseType = mType;
        }
        #endregion
        #endregion

        #region Event
        private new void MouseMove(object sender, MouseEventArgs e)
        {
            System.Drawing.Point MousePosition = renderControl.PointToClient(Cursor.Position);
            positionText.Set(MousePosition.X + "," + MousePosition.Y, new Vector2(0, 0), font, Color.Red);
            if(resizing==true)
            {
                if (mouseType == MouseType.MOUSE_POINT)
                {
                    if (TSItree.SelectedNode.Level == 1)
                    {
                        if((e.X - tsi.listDDS[TSItree.SelectedNode.Parent.Index].ListDDS_element[TSItree.SelectedNode.Index].X > 0) && (e.Y - tsi.listDDS[TSItree.SelectedNode.Parent.Index].ListDDS_element[TSItree.SelectedNode.Index].Y >0))
                        {
                        tsi.listDDS[TSItree.SelectedNode.Parent.Index].ListDDS_element[TSItree.SelectedNode.Index].Width = e.X - tsi.listDDS[TSItree.SelectedNode.Parent.Index].ListDDS_element[TSItree.SelectedNode.Index].X;
                        tsi.listDDS[TSItree.SelectedNode.Parent.Index].ListDDS_element[TSItree.SelectedNode.Index].Height = e.Y - tsi.listDDS[TSItree.SelectedNode.Parent.Index].ListDDS_element[TSItree.SelectedNode.Index].Y;
                        ElementChanged(TSItree.SelectedNode.Parent.Index, TSItree.SelectedNode.Index);
                        }
                    }
                }
            }
        }
        private new void MouseUp(object sender, MouseEventArgs e)
        {
            if (mouseType == MouseType.MOUSE_POINT)
            {
                if (TSItree.SelectedNode.Level == 1)
                {
                    resizing = false;
                }
            }
        }
        private new void MouseDown(object sender, MouseEventArgs e)
        {
            if (mouseType == MouseType.MOUSE_POINT)
            {
                if (TSItree.SelectedNode.Level == 1)
                {
                    tsi.listDDS[TSItree.SelectedNode.Parent.Index].ListDDS_element[TSItree.SelectedNode.Index].X = e.X;
                    tsi.listDDS[TSItree.SelectedNode.Parent.Index].ListDDS_element[TSItree.SelectedNode.Index].Y = e.Y;
                    ElementChanged(TSItree.SelectedNode.Parent.Index, TSItree.SelectedNode.Index);
                    resizing = true;
                }
            }
        }
        #endregion
    }
}
