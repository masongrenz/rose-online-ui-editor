using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using Rose_online_UI_Editor.Forms.CustomControls;
using Rose_online_UI_Editor.Forms.CustomControls.GraphicsDeviceControl;
using Rose_online_UI_Editor.Files_Handlers;
using Rose_online_UI_Editor.Content_Manager;
using Rose_online_UI_Editor.Command_Manager;
using Rose_online_UI_Editor.Command_Manager.TSICommand;

namespace Rose_online_UI_Editor.Forms
{
    public partial class MainForm : Form
    {
        #region private Variables
        private DirectoryInfo clientFolder;        
        #endregion

        #region constructors
        public MainForm()
        {
            InitializeComponent();         
            StatusManager.RegisterDelegate(this.logTextBox.AppendText);            
            ContentManager.SetGraphicsDevice(GraphicsDeviceService.AddRef(this.Handle, 50, 50).GraphicsDevice);
            StatusManager.AddLog("ROSE Online UI Editor Vs 2.0 By Jiwan : Ready to use");
        }
        #endregion

        #region GUI Events
        #region Exit , Open , Save
        private void ExitButton_Click(object sender, EventArgs e)
        {
            ContentManager.Instance().ClearAll();
            //Clear spriteManager too
            this.Close();        
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.Description = @"Select your client folder : 
for exemple C:\Game\Rose Online";

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {              
                controlNode.Nodes.Clear();
                ContentManager.Instance().ClearAll();
                //Clear spriteManager too

                clientFolder = new DirectoryInfo(folderBrowserDialog.SelectedPath);
                ContentManager.SetRootPath(folderBrowserDialog.SelectedPath);
                StatusManager.AddLog("Root Folder : "+ folderBrowserDialog.SelectedPath);

                if(Directory.Exists(folderBrowserDialog.SelectedPath + "\\3DDATA\\CONTROL"))
                {
                    StatusManager.AddLog("Control Folder has been found at : " + folderBrowserDialog.SelectedPath + "\\3DDATA\\CONTROL");
                    ContentManager.SetControlFolderPath(folderBrowserDialog.SelectedPath + "\\3DDATA\\CONTROL");
                }
                else
                {
                    StatusManager.AddLog("Control Folder can't be found : it should be at " + folderBrowserDialog.SelectedPath + "\\3DDATA\\CONTROL");
                    return;
                }

                if (Directory.Exists(folderBrowserDialog.SelectedPath + "\\3DDATA\\CONTROL\\RES"))
                {
                    StatusManager.AddLog("Res Folder has been found at : " + folderBrowserDialog.SelectedPath + "\\3DDATA\\CONTROL\\RES");
                    ContentManager.SetResFolderPath(folderBrowserDialog.SelectedPath + "\\3DDATA\\CONTROL\\RES");
                }
                else
                {
                    StatusManager.AddLog("Res Folder can't be found : it should be at " + folderBrowserDialog.SelectedPath + "\\3DDATA\\CONTROL\\RES");
                    return;
                }
                if (Directory.Exists(folderBrowserDialog.SelectedPath + "\\3DDATA\\CONTROL\\XML"))
                {
                    StatusManager.AddLog("XML Folder has been found at : " + folderBrowserDialog.SelectedPath + "\\3DDATA\\CONTROL\\XML");
                    ContentManager.SetXMLFolderPath(folderBrowserDialog.SelectedPath + "\\3DDATA\\CONTROL\\XML");
                }
                else
                {
                    StatusManager.AddLog("XML Folder can't be found : it should be at " + folderBrowserDialog.SelectedPath + "\\3DDATA\\CONTROL\\XML");
                    return;
                }


                DirectoryInfo resFolder = new DirectoryInfo(ContentManager.GetResFolderPath());
                DevComponents.AdvTree.Node resNode = new DevComponents.AdvTree.Node();
                resNode.Image = global::Rose_online_UI_Editor.Properties.Resources.SmallOpenDocument;
                resNode.Text = resFolder.Name;

                StatusManager.AddLog("Creating file tree ...");
                StatusManager.AddLog("Preloading TSI and Sprites");
                foreach (FileInfo file in resFolder.GetFiles())
                {
                    if (file.Extension.ToLower() == ".dds" || file.Extension.ToLower() == ".tsi")
                    {
                        DevComponents.AdvTree.Node newFileNode = new DevComponents.AdvTree.Node();
                        newFileNode.Text = file.Name;
                        newFileNode.Name = file.Extension.ToLower();
                        
                        switch (file.Extension.ToLower())
                        {
                            case ".dds":
                                newFileNode.Image = global::Rose_online_UI_Editor.Properties.Resources.SmallPictureLogo; 
                                //ajouter un sprite dans le spritemanager pour chaque dds trouvé !!!! 
                               break;
                            case ".tsi":
                                newFileNode.Image = global::Rose_online_UI_Editor.Properties.Resources.SmallTSILogo;
                                TSI newTSI = ContentManager.Instance().GetTSI(ContentManager.resFolderPartialPath + "\\" + file.Name);
                                SpriteManager.Instance().GenerateSpritesFromTSI(newTSI);
                                break;
                        }
                        resNode.Nodes.Add(newFileNode);
                    }
                }

                DirectoryInfo xmlFolder = new DirectoryInfo(ContentManager.GetXMLFolderPath());
                DevComponents.AdvTree.Node xmlNode = new DevComponents.AdvTree.Node();
                xmlNode.Image = global::Rose_online_UI_Editor.Properties.Resources.SmallOpenDocument;
                xmlNode.Text = xmlFolder.Name;
                foreach (FileInfo file in xmlFolder.GetFiles())
                {
                    if (file.Extension.ToLower() == ".xml")
                    {
                        DevComponents.AdvTree.Node newFileNode = new DevComponents.AdvTree.Node();
                        newFileNode.Text = file.Name;
                        newFileNode.Name = file.Extension.ToLower();
                        newFileNode.Image = global::Rose_online_UI_Editor.Properties.Resources.SmallxmlLogo;                       
                        xmlNode.Nodes.Add(newFileNode);
                    }

                }                  
                controlNode.Nodes.Add(resNode);
                controlNode.Nodes.Add(xmlNode);              
            }
            
        }

        private void SaveButton2_Click(object sender, EventArgs e)
        {
            if (MainBar.SelectedDockTab >= 0)
            {
                if (MainBar.Items[MainBar.SelectedDockTab].GetType() == typeof(ICustomControl)) //avoid error with "about tab" which isn't a ICustomControl
                {
                    ICustomControl selectedDockContainer = (ICustomControl)this.MainBar.Items[MainBar.SelectedDockTab];
                    selectedDockContainer.Save();
                }
            }
        }
        #endregion

        private void SolutionTree_AfterNodeSelect(object sender, DevComponents.AdvTree.AdvTreeNodeEventArgs e)
        {
            switch (e.Node.Name)
            {
                case ".dds":
                    SolutionTree.ContextMenuStrip = DDSandTSIMenuStrip;
                    break;
                case ".tsi":
                    SolutionTree.ContextMenuStrip = DDSandTSIMenuStrip;
                    break;
                case ".xml":
                    SolutionTree.ContextMenuStrip = XmlMenuStrip;
                    break;                
            }
        }
               
        private void MainBar_DockTabChange(object sender, DevComponents.DotNetBar.DockTabChangeEventArgs e)
        {
            TreeTSI.Nodes.Clear();
            propertyGrid.SelectedObject = null;
            if (e.NewTab.GetType() == typeof(XmlDockContainer))
            {               
                XmlDockContainer SelectXmlDockContainer = (XmlDockContainer)e.NewTab;
                SelectXmlDockContainer.Reload();
                treeTSIMenuStrip.Enabled = false;            
            }
            else if (e.NewTab.GetType() == typeof(DDSDockContainer))
            {               
                DDSDockContainer SelectDDSDockContainer = (DDSDockContainer)e.NewTab;
                SelectDDSDockContainer.Reload();
                treeTSIMenuStrip.Enabled = false;               
            }
            else if (e.NewTab.GetType() == typeof(TSIDockContainer))
            {
                TSIDockContainer SelectTSIDockContainer = (TSIDockContainer)e.NewTab;
                SelectTSIDockContainer.Reload();
                tabControl.SelectedTab = tabPageTSI;
                treeTSIMenuStrip.Enabled = true;
            }
            else
            {
                treeTSIMenuStrip.Enabled = false;
            }
            
            
            if (e.OldTab.GetType() == typeof(ICustomControl)) //avoid error with "about tab" which isn't a ICustomControl
            {           
            ICustomControl oldDock = (ICustomControl)e.OldTab;
            oldDock.Save();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.MainBar.SelectedDockTab >= 0)
            {
                if (MainBar.Items[MainBar.SelectedDockTab].GetType() == typeof(ICustomControl)) //avoid error with "about tab" which isn't a ICustomControl
                {
                    ICustomControl selectedDockContainer = (ICustomControl)MainBar.Items[MainBar.SelectedDockTab];
                    selectedDockContainer.Save();
                }
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.MainBar.SelectedDockTab >= 0)
            {
                Type type = MainBar.Items[MainBar.SelectedDockTab].GetType();
                if (MainBar.Items[MainBar.SelectedDockTab].GetType().Equals(typeof(ICustomControl))) //avoid error with "about tab" which isn't a ICustomControl
                {
                    ICustomControl selectedDockContainer = (ICustomControl)MainBar.Items[MainBar.SelectedDockTab];
                    selectedDockContainer.Save();
                    this.MainBar.Items.RemoveAt(this.MainBar.SelectedDockTab);
                }
            }
        }

        private void OpenDDSTSIMenuItem_Click(object sender, EventArgs e)
        {
           
            if (SolutionTree.SelectedNode.Name == ".dds")
            {
                DDSDockContainer newDDSDockContainer = new DDSDockContainer(SolutionTree.SelectedNode.Text, SolutionTree.SelectedNode.Text);
                newDDSDockContainer.Load(ContentManager.resFolderPartialPath+"\\" + SolutionTree.SelectedNode.Text);
                AddDockContainer(newDDSDockContainer);

            }            
            else if (SolutionTree.SelectedNode.Name == ".tsi")
            {                              
                TSIDockContainer newTSIDockContainer = new TSIDockContainer(SolutionTree.SelectedNode.Text, SolutionTree.SelectedNode.Text, TreeTSI,propertyGrid);
                newTSIDockContainer.Load(ContentManager.resFolderPartialPath + "\\" + SolutionTree.SelectedNode.Text);
                AddDockContainer(newTSIDockContainer);
            }            
        }

        private void NewButton_Click(object sender, EventArgs e)
        {

        }

        private void NewButton_Click_1(object sender, EventArgs e)
        {
            NewFileForm newFileForm = new NewFileForm();
            if (newFileForm.ShowDialog() == DialogResult.OK)
            {
            
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists("Temporary Files")) Directory.CreateDirectory("Temporary Files");            
                        
        }            

        private void TreeTSI_NodeClick(object sender, DevComponents.AdvTree.TreeNodeMouseEventArgs e)
        {            
            if (e.Node.Level == 0)
            {
                addDDSToolStripMenuItem.Enabled = true;
                removeDDSToolStripMenuItem.Enabled = true;
                addElementToolStripMenuItem.Enabled = true;
                removeElementToolStripMenuItem.Enabled = false;
                copyTreeTSIMenuItem.Enabled = true;
                pasteTreeTSIMenuItem.Enabled = true;
                TSIDockContainer selectedTSIDockContainer = (TSIDockContainer)MainBar.SelectedDockContainerItem;                                               
                selectedTSIDockContainer.SelectDDS(e.Node.Index);   
                oldSelectedObject = selectedTSIDockContainer.GetDDS(e.Node.Index).
            }
            if (e.Node.Level == 1)
            {
                addDDSToolStripMenuItem.Enabled = false;
                removeDDSToolStripMenuItem.Enabled = false;
                addElementToolStripMenuItem.Enabled = true;
                removeElementToolStripMenuItem.Enabled = true;
                copyTreeTSIMenuItem.Enabled = true;
                pasteTreeTSIMenuItem.Enabled = true;
                TSIDockContainer selectedTSIDockContainer = (TSIDockContainer)MainBar.SelectedDockContainerItem;
                selectedTSIDockContainer.SelectSprite(e.Node.Parent.Index, e.Node.Index);                
            }
        }

        private void addDDSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TSIDockContainer selectedTSIDockContainer = (TSIDockContainer)MainBar.SelectedDockContainerItem;
            OpenFileDialog DDSdialog = new OpenFileDialog();
            DDSdialog.Filter = "DDS Files (*.dds)|*.dds";
            if (DDSdialog.ShowDialog() == DialogResult.OK)
            {
                CmdAddDDS cmd = new CmdAddDDS();
                cmd.Control = selectedTSIDockContainer;
                cmd.DDSname = System.IO.Path.GetFileName(DDSdialog.FileName);
                selectedTSIDockContainer.getManager().executeCommand(cmd);
            }
        }

        private void removeDDSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DevComponents.AdvTree.Node selectNode = TreeTSI.SelectedNode;
            TSIDockContainer selectedTSIDockContainer = (TSIDockContainer)MainBar.SelectedDockContainerItem;
            if (selectNode.Level == 0)
            {
                CmdRemoveDDS cmd = new CmdRemoveDDS();
                cmd.Control = selectedTSIDockContainer;
                cmd.DDSIndex = selectNode.Index;
                selectedTSIDockContainer.getManager().executeCommand(cmd); 
            }            
        }
                
        private void addElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DevComponents.AdvTree.Node selectedNode = TreeTSI.SelectedNode;
            TSIDockContainer selectedTSIDockContainer = (TSIDockContainer)MainBar.SelectedDockContainerItem;
            if (selectedNode.Level == 1)
            {
                int DDSIndex = selectedNode.Parent.Index;
                int elementIndex = selectedNode.Index;
                CmdAddElement cmd = new CmdAddElement();
                cmd.Control = selectedTSIDockContainer;
                cmd.DDSIndex = DDSIndex;
                cmd.elementIndex = elementIndex+1;
                cmd.elementName = "New Element";
                selectedTSIDockContainer.getManager().executeCommand(cmd);
            }
            else if (selectedNode.Level == 0)
            {
                int DDSIndex = selectedNode.Index;
                
                CmdAddElement cmd = new CmdAddElement();
                cmd.Control = selectedTSIDockContainer;
                cmd.DDSIndex = DDSIndex;
                cmd.elementIndex = selectedNode.Nodes.Count;// add element at the last position
                cmd.elementName = "New Element";
                selectedTSIDockContainer.getManager().executeCommand(cmd);                
            }
        }

        private void removeElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
            DevComponents.AdvTree.Node selectedNode = TreeTSI.SelectedNode;
            TSIDockContainer selectedTSIDockContainer = (TSIDockContainer)MainBar.SelectedDockContainerItem;
            if (selectedNode.Level == 1)
            {
                int DDSIndex = selectedNode.Parent.Index;
                int elementIndex = selectedNode.Index;
                CmdRemoveElement cmd = new CmdRemoveElement();
                cmd.Control = selectedTSIDockContainer;
                cmd.DDSIndex = DDSIndex;
                cmd.elementIndex = elementIndex;
                selectedTSIDockContainer.getManager().executeCommand(cmd);
            }
           
        }

        private void copyTreeTSIMenuItem_Click(object sender, EventArgs e)
        {
            DevComponents.AdvTree.Node selectedNode = TreeTSI.SelectedNode;
            TSIDockContainer selectTSIDockContainer = (TSIDockContainer)MainBar.SelectedDockContainerItem;
            if (selectedNode.Level == 1)
            {
                int DDSIndex = selectedNode.Parent.Index;
                int elementIndex = selectedNode.Index;
                copiedSprite = selectTSIDockContainer.GetSprite(DDSIndex,elementIndex);
            }
            else if (selectedNode.Level == 0)
            {
                int DDSIndex = selectedNode.Index;
                copiedDDS = selectTSIDockContainer.GetDDS(DDSIndex);
            }
        }

        private void pasteTreeTSIMenuItem_Click(object sender, EventArgs e)
        {
            DevComponents.AdvTree.Node selectedNode = TreeTSI.SelectedNode;
            TSIDockContainer selectedTSIDockContainer = (TSIDockContainer)MainBar.SelectedDockContainerItem;
            if (selectedNode.Level == 1)
            {
                if(copiedSprite != null)
                {
                int DDSIndex = selectedNode.Parent.Index;
                int elementIndex = selectedNode.Index;
                CmdPasteElement cmd = new CmdPasteElement();
                cmd.Control = selectedTSIDockContainer;
                cmd.DDSIndex = DDSIndex;
                cmd.elementIndex = elementIndex;
                cmd.newElement = copiedSprite;
                selectedTSIDockContainer.getManager().executeCommand(cmd);
                }
            }
            else if (selectedNode.Level == 0)
            {
                if(copiedDDS != null)
                {
                    int DDSIndex = selectedNode.Index;
                    CmdPasteDDS cmd = new CmdPasteDDS();
                    cmd.Control = selectedTSIDockContainer;
                    cmd.DDSIndex = DDSIndex;
                    cmd.newDDS = copiedDDS;
                    selectedTSIDockContainer.getManager().executeCommand(cmd);
                }
            }
           
        }

        #region propertyGrid events
        private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (propertyGrid.SelectedObject.GetType() == typeof(TSI.DDS))
            {
                TSIDockContainer selectedTSIDockContainer = (TSIDockContainer)MainBar.SelectedDockContainerItem;
                DevComponents.AdvTree.Node selectedNode = TreeTSI.SelectedNode;
                CmdDDSChanged cmd = new CmdDDSChanged();
                cmd.Control = selectedTSIDockContainer;
                cmd.DDSIndex = selectedNode.Parent.Index;
                cmd.oldDDS = (TSI.DDS)oldSelectedObject;
                selectedTSIDockContainer.getManager().executeCommand(cmd);
            }
            else if (propertyGrid.SelectedObject.GetType() == typeof(TSI.DDS.DDSElement))
            {
                TSIDockContainer selectedTSIDockContainer = (TSIDockContainer)MainBar.SelectedDockContainerItem;
                DevComponents.AdvTree.Node selectedNode = TreeTSI.SelectedNode;
                CmdElementChanged cmd = new CmdElementChanged();
                cmd.Control = selectedTSIDockContainer;
                cmd.DDSIndex = selectedNode.Parent.Index;
                cmd.elementIndex = selectedNode.Index;
                cmd.oldElement = (TSI.DDS.DDSElement)oldSelectedObject;
                selectedTSIDockContainer.getManager().executeCommand(cmd);
             }
        }  
        #endregion

        private void CheckBoxFiles_Click(object sender, EventArgs e)
        {

        }
        
        private void CloseButton_Click(object sender, EventArgs e)
        {
            TreeTSI.Nodes.Clear();
            SolutionTree.Nodes[0].Nodes.Clear();
            SolutionTree.Refresh();
            this.MainBar.Items.Clear();
        }

        private void SaveAllButton_Click(object sender, EventArgs e)
        {

        }
        #endregion

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string UIPath = this.clientFolder.FullName + "\\" + SolutionTree.SelectedNode.Parent.Text + "\\" + SolutionTree.SelectedNode.Text;
            string UIFolder = this.clientFolder.FullName + "\\" + SolutionTree.SelectedNode.Parent.Text;
            UIDockContainer newUIDockContainer = new UIDockContainer(SolutionTree.SelectedNode.Text, SolutionTree.SelectedNode.Text, UIFolder , clientFolder.FullName + "\\RES");

            newUIDockContainer.Load(UIPath);
            
            AddDockContainer(newUIDockContainer);
            newUIDockContainer.RenderUI();
            newUIDockContainer.renderControl.MouseMove += new MouseEventHandler(UIRenderMouseMove);
            newUIDockContainer.renderControl.MouseUp += new MouseEventHandler(UIRenderMouseUp);
            newUIDockContainer.renderControl.MouseDown += new MouseEventHandler(UIRenderMouseDown);
            AddLog(UIPath + " succefully opened", LogType.MSG_INFO);
        }

        private void ZoomSlider_ValueChanged(object sender, EventArgs e)
        {
            if (this.MainBar.SelectedDockTab >= 0)
            {
                if (this.MainBar.Items[MainBar.SelectedDockTab].GetType() == typeof(UIDockContainer))
                {
                    UIDockContainer selectUIDockContainer = (UIDockContainer)this.MainBar.Items[MainBar.SelectedDockTab];
                    selectUIDockContainer.SetZoom(this.ZoomSlider.Value*0.5f);
                }
            }
        }

        private void grabButton_Click(object sender, EventArgs e)
        {
            if (this.MainBar.Items[MainBar.SelectedDockTab].GetType() == typeof(UIDockContainer))
            {

                UIDockContainer SelectUIDockContainer = (UIDockContainer)this.MainBar.Items[MainBar.SelectedDockTab];
                SelectUIDockContainer.renderControl.Cursor = Cursors.Hand;
            }
        }

        private void NormalMouseButton_Click(object sender, EventArgs e)
        {
            if (this.MainBar.Items[MainBar.SelectedDockTab].GetType() == typeof(UIDockContainer))
            {

                UIDockContainer SelectUIDockContainer = (UIDockContainer)this.MainBar.Items[MainBar.SelectedDockTab];
                SelectUIDockContainer.renderControl.Cursor = Cursors.Default;
            }
        }

        private void MoveMouseButton_Click(object sender, EventArgs e)
        {
            if (this.MainBar.Items[MainBar.SelectedDockTab].GetType() == typeof(UIDockContainer))
            {

                UIDockContainer SelectUIDockContainer = (UIDockContainer)this.MainBar.Items[MainBar.SelectedDockTab];
                SelectUIDockContainer.renderControl.Cursor = Cursors.Default;
            }
        }

        #region Undo Redo Button Event
        private void buttonUndo_Click(object sender, EventArgs e)
        {
            //vérifier si on est bien sur un IUseCommand (par exemple DDSContainer n'en est pas un)
            IUseCommand selectedDockContainer = (IUseCommand)MainBar.Items[MainBar.SelectedDockTab];
            selectedDockContainer.getManager().undoCommand();
        }
        private void buttonRedo_Click(object sender, EventArgs e)
        {
            //vérifier si on est bien sur un IUseCommand (par exemple DDSContainer n'en est pas un)
            IUseCommand selectedDockContainer = (IUseCommand)MainBar.Items[MainBar.SelectedDockTab];
            selectedDockContainer.getManager().recoCommand();
        }
        #endregion       

       

    }
}
