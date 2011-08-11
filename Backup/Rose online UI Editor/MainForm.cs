using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Rose_online_UI_Editor.CustomControl;
using Rose_online_UI_Editor.Files_Handlers;
namespace Rose_online_UI_Editor
{
    public partial class MainForm : Form
    {
        #region Variables
        private DirectoryInfo ClientFolder;
        
        #endregion
        
        public MainForm()
        {
            InitializeComponent();
        }
        
        #region Events
        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                logTextBox.Clear();
                ControlNode.Nodes.Clear();                
                
                ClientFolder = new DirectoryInfo(folderBrowserDialog.SelectedPath);
               
                foreach (DirectoryInfo directory in ClientFolder.GetDirectories())
                {
                    DevComponents.AdvTree.Node newDirectoryNode = new DevComponents.AdvTree.Node();
                    newDirectoryNode.Image = global::Rose_online_UI_Editor.Properties.Resources.SmallOpenDocument;
                    newDirectoryNode.Text = directory.Name;
                
                foreach (FileInfo file in directory.GetFiles())
                {
                    
                    DevComponents.AdvTree.Node newFileNode = new DevComponents.AdvTree.Node();
                    newFileNode.Text = file.Name;
                    newFileNode.Name = file.Extension.ToLower();
                    switch (file.Extension.ToLower())
                    {
                        case ".dds" :
                            newFileNode.Image = global::Rose_online_UI_Editor.Properties.Resources.SmallPictureLogo;
                            break;
                        case ".xml" :
                            newFileNode.Image = global::Rose_online_UI_Editor.Properties.Resources.SmallxmlLogo;
                            break;
                        case ".tsi" :
                            newFileNode.Image = global::Rose_online_UI_Editor.Properties.Resources.SmallTSILogo;
                                break;


                    }
                    newDirectoryNode.Nodes.Add(newFileNode);

                }
                ControlNode.Nodes.Add(newDirectoryNode);
                     
                }
                AddLog(ClientFolder.FullName + " opened", LogType.MSG_NONE);
            }
        }

        private void SolutionTree_AfterNodeSelect(object sender, DevComponents.AdvTree.AdvTreeNodeEventArgs e)
        {
            switch (e.Node.Name)
            {
                case ".dds":
                    SolutionTree.ContextMenuStrip = DDSandTSIMenuStrip;
                    break;
                case ".xml":
                    SolutionTree.ContextMenuStrip = XmlMenuStrip;
                    break;
                case ".tsi":
                    SolutionTree.ContextMenuStrip = DDSandTSIMenuStrip;
                    break;
            }
        }
               
        private void SaveButton2_Click(object sender, EventArgs e)
        {
            if (this.MainBar.SelectedDockTab >= 0)
            {
               if(this.MainBar.Items[MainBar.SelectedDockTab].GetType() == typeof(XmlDockContainer))
                {
                XmlDockContainer SelectXmlDockContainer = (XmlDockContainer)this.MainBar.Items[MainBar.SelectedDockTab];
                  try
                  {
                   SelectXmlDockContainer.Save();

                   this.AddLog(SelectXmlDockContainer.XmlPath + " succefully saved", LogType.MSG_INFO);
                  }
                   catch
                  {
                      this.AddLog(SelectXmlDockContainer.XmlPath + " can't be save", LogType.MSG_ERROR);
                   }
                }
               else if (this.MainBar.Items[MainBar.SelectedDockTab].GetType() == typeof(TSIDockContainer))
               {
                   TSIDockContainer SelectTSIDockContainer = (TSIDockContainer)this.MainBar.Items[MainBar.SelectedDockTab];
                   try
                   {
                       SelectTSIDockContainer.Save();
                       this.AddLog(SelectTSIDockContainer.TSIPath + " succefully saved", LogType.MSG_INFO);
                   }
                   catch
                   {
                       this.AddLog(SelectTSIDockContainer.TSIPath + " can't be save", LogType.MSG_ERROR);
                   }
               }

            }
        }

        private void showCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XmlDockContainer newXmlDockContainer = new XmlDockContainer(SolutionTree.SelectedNode.Text, SolutionTree.SelectedNode.Text);
            string XmlPath;
           
             XmlPath = this.ClientFolder.FullName + "\\" + SolutionTree.SelectedNode.Parent.Text + "\\" + SolutionTree.SelectedNode.Text;
            
            try
            {
                newXmlDockContainer.LoadXml(XmlPath);
                AddDockContainer(newXmlDockContainer);
                AddLog(XmlPath + " succefully opened", LogType.MSG_INFO);
            }
            catch
            {
                AddLog(XmlPath + " can't be open", LogType.MSG_ERROR);
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
                TreeTSIMenuStrip.Enabled = false;
            
            }
            else if (e.NewTab.GetType() == typeof(DDSDockContainer))
            {
               
                DDSDockContainer SelectDDSDockContainer = (DDSDockContainer)e.NewTab;
                SelectDDSDockContainer.Reload();
                TreeTSIMenuStrip.Enabled = false;
               
            }
            else if (e.NewTab.GetType() == typeof(TSIDockContainer))
            {
                TSIDockContainer SelectTSIDockContainer = (TSIDockContainer)e.NewTab;
                SelectTSIDockContainer.tsi.Reload();
                LoadTSITree(SelectTSIDockContainer);
                tabControl.SelectedTab = tabPageTSI;
                TreeTSIMenuStrip.Enabled = true;
            }
            else
            {
                TreeTSIMenuStrip.Enabled = false;
            }


            if (e.OldTab.GetType() == typeof(XmlDockContainer))
            {
                XmlDockContainer OldtXmlDockContainer = (XmlDockContainer)e.OldTab;
                OldtXmlDockContainer.Save();
            }
            else if (e.OldTab.GetType() == typeof(TSIDockContainer))
            {
                TSIDockContainer OldTSIDockContainer = (TSIDockContainer)e.OldTab;
                OldTSIDockContainer.tsi.Save();
            }

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.MainBar.SelectedDockTab >= 0)
            {
                if (this.MainBar.Items[MainBar.SelectedDockTab].GetType() == typeof(XmlDockContainer))
                {
                    XmlDockContainer SelectXmlDockContainer = (XmlDockContainer)this.MainBar.Items[MainBar.SelectedDockTab];
                    try
                    {
                        SelectXmlDockContainer.Save();

                        this.AddLog(SelectXmlDockContainer.XmlPath + " succefully saved", LogType.MSG_INFO);
                    }
                    catch
                    {
                        this.AddLog(SelectXmlDockContainer.XmlPath + " can't be save", LogType.MSG_ERROR);
                    }
                }
                else if (this.MainBar.Items[MainBar.SelectedDockTab].GetType() == typeof(TSIDockContainer))
                {
                    TSIDockContainer SelectTSIDockContainer = (TSIDockContainer)this.MainBar.Items[MainBar.SelectedDockTab];
                    try
                    {
                        SelectTSIDockContainer.Save();
                        this.AddLog(SelectTSIDockContainer.TSIPath + " succefully saved", LogType.MSG_INFO);
                    }
                    catch
                    {
                        this.AddLog(SelectTSIDockContainer.TSIPath + " can't be save", LogType.MSG_ERROR);
                    }
                }

            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.MainBar.SelectedDockTab >= 0)
            {
                if (this.MainBar.Items[MainBar.SelectedDockTab].GetType() == typeof(XmlDockContainer))
                {
                    XmlDockContainer SelectXmlDockContainer = (XmlDockContainer)this.MainBar.Items[MainBar.SelectedDockTab];
                    try
                    {
                        SelectXmlDockContainer.Save();

                        this.AddLog(SelectXmlDockContainer.XmlPath + " succefully saved", LogType.MSG_INFO);
                    }
                    catch
                    {
                        this.AddLog(SelectXmlDockContainer.XmlPath + " can't be save", LogType.MSG_ERROR);
                    }
                }

                this.MainBar.Items.RemoveAt(this.MainBar.SelectedDockTab);
            }
        }

        private void OpenDDSTSIMenuItem_Click(object sender, EventArgs e)
        {
             if (SolutionTree.SelectedNode.Name == ".dds")
            {
                string DDSPath = this.ClientFolder.FullName + "\\" + SolutionTree.SelectedNode.Parent.Text + "\\" + SolutionTree.SelectedNode.Text;
                DDSDockContainer newDDSDockContainer = new DDSDockContainer(SolutionTree.SelectedNode.Text, SolutionTree.SelectedNode.Text);
                try
                {
                    newDDSDockContainer.LoadDDS(DDSPath);
                    AddDockContainer(newDDSDockContainer);
                    AddLog(DDSPath + " succefully opened", LogType.MSG_INFO);
                }
                catch
                {
                    AddLog(DDSPath + " can't be open", LogType.MSG_ERROR);
                }


            }
            else if (SolutionTree.SelectedNode.Name == ".tsi")
            {
                string TSIPath = this.ClientFolder.FullName + "\\" + SolutionTree.SelectedNode.Parent.Text + "\\" + SolutionTree.SelectedNode.Text;
                string TSIFolder = this.ClientFolder.FullName + "\\" + SolutionTree.SelectedNode.Parent.Text;
                    TSIDockContainer newTSIDockContainer = new TSIDockContainer(SolutionTree.SelectedNode.Text, SolutionTree.SelectedNode.Text , TSIFolder);
               
                    newTSIDockContainer.LoadTSI(TSIPath);
                    AddDockContainer(newTSIDockContainer);
                    LoadTSITree(newTSIDockContainer);
                    newTSIDockContainer.renderControl.MouseUp += new MouseEventHandler(TSIRenderMouseUp);
                    newTSIDockContainer.renderControl.MouseDown += new MouseEventHandler(TSIRenderMouseDown);
                    newTSIDockContainer.renderControl.MouseMove += new MouseEventHandler(TSIRenderMouseMove);
                    AddLog(TSIPath + " succefully opened", LogType.MSG_INFO);
                
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
                TSIDockContainer selectTSIDockContainer = (TSIDockContainer)MainBar.SelectedDockContainerItem;
                propertyGrid.SelectedObject = selectTSIDockContainer.tsi.listDDS[e.Node.Index];
                try
                {                    
                    selectTSIDockContainer.DrawDDS(e.Node.Index);
                }
                catch
                {
                    AddLog(String.Format("Error , {0} can't be load", selectTSIDockContainer.tsi.listDDS[e.Node.Index].Path), LogType.MSG_ERROR);                
                }
            }
            if (e.Node.Level == 1)
            {
                addDDSToolStripMenuItem.Enabled = false;
                removeDDSToolStripMenuItem.Enabled = false;
                addElementToolStripMenuItem.Enabled = true;
                removeElementToolStripMenuItem.Enabled = true;
                copyTreeTSIMenuItem.Enabled = true;
                pasteTreeTSIMenuItem.Enabled = true;
                TSIDockContainer selectTSIDockContainer = (TSIDockContainer)MainBar.SelectedDockContainerItem;
                propertyGrid.SelectedObject = selectTSIDockContainer.tsi.listDDS[e.Node.Parent.Index].ListDDS_element[e.Node.Index];
                try
                {
                    selectTSIDockContainer.DrawDDS(e.Node.Parent.Index);
                    selectTSIDockContainer.DrawAera(e.Node.Parent.Index, e.Node.Index);
                }
                catch
                {
                    AddLog(String.Format("Error , {0} can't be load", selectTSIDockContainer.tsi.listDDS[e.Node.Index].Path), LogType.MSG_ERROR);
                }

            }
            else
            {
            
            }
        }
        

        private void addDDSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TSIDockContainer selectTSIDockContainer = (TSIDockContainer)MainBar.SelectedDockContainerItem;
            OpenFileDialog DDSdialog = new OpenFileDialog();
            DDSdialog.Filter = "DDS Files (*.dds)|*.dds";
            if (DDSdialog.ShowDialog() == DialogResult.OK)
            {
                TSI.DDS newDDS = new TSI.DDS();
                newDDS.ColourKey = 0;
                newDDS.Path = System.IO.Path.GetFileName(DDSdialog.FileName);
                selectTSIDockContainer.tsi.listDDS.Add(newDDS);
                DevComponents.AdvTree.Node newDDSNode = new DevComponents.AdvTree.Node();
                newDDSNode.Name = System.IO.Path.GetFileName(DDSdialog.FileName);
                newDDSNode.Text = System.IO.Path.GetFileName(DDSdialog.FileName);
                TreeTSI.Nodes.Add(newDDSNode);
            }
        }

        private void removeDDSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DevComponents.AdvTree.Node selectNode = TreeTSI.SelectedNode;
            TSIDockContainer selectTSIDockContainer = (TSIDockContainer)MainBar.SelectedDockContainerItem;
            if (selectNode.Level == 0)
            {
                int DDSIndex = selectNode.Index;
                selectTSIDockContainer.tsi.listDDS.RemoveAt(DDSIndex);
                TreeTSI.Nodes.RemoveAt(DDSIndex);
               
            }
        }
                
        private void addElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DevComponents.AdvTree.Node selectNode = TreeTSI.SelectedNode;
            TSIDockContainer selectTSIDockContainer = (TSIDockContainer)MainBar.SelectedDockContainerItem;
            if (selectNode.Level == 1)
            {
                int DDSIndex = selectNode.Parent.Index;
                int ElementIndex = selectNode.Index;
                
                TSI.DDS.DDSElement newDDSElement = new TSI.DDS.DDSElement((short)DDSIndex, 0, 0, 10, 10, 0, "New Element");
                selectTSIDockContainer.tsi.listDDS[DDSIndex].ListDDS_element.Add(newDDSElement);
                DevComponents.AdvTree.Node newDDSElementNode = new DevComponents.AdvTree.Node();
                newDDSElementNode.Name = "New Element";
                newDDSElementNode.Text = "New Element";
                selectNode.Parent.Nodes.Add(newDDSElementNode);
            }
            else if (selectNode.Level == 0)
            {
                int DDSIndex = selectNode.Index;
                
                TSI.DDS.DDSElement newDDSElement = new TSI.DDS.DDSElement((short)DDSIndex, 0, 0, 10, 10, 0, "New Element");
                selectTSIDockContainer.tsi.listDDS[DDSIndex].ListDDS_element.Add(newDDSElement);
                DevComponents.AdvTree.Node newDDSElementNode = new DevComponents.AdvTree.Node();
                newDDSElementNode.Name = "New Element";
                newDDSElementNode.Text = "New Element";
                selectNode.Nodes.Add(newDDSElementNode);
            }
        }

        private void removeElementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DevComponents.AdvTree.Node selectNode = TreeTSI.SelectedNode;
            TSIDockContainer selectTSIDockContainer = (TSIDockContainer)MainBar.SelectedDockContainerItem;
            if (selectNode.Level == 1)
            {
                int DDSIndex = selectNode.Parent.Index;
                int ElementIndex = selectNode.Index;
                selectTSIDockContainer.tsi.listDDS[DDSIndex].ListDDS_element.RemoveAt(ElementIndex);
                TreeTSI.Nodes[DDSIndex].Nodes.RemoveAt(ElementIndex);

            }
        }

        private void copyTreeTSIMenuItem_Click(object sender, EventArgs e)
        {
            DevComponents.AdvTree.Node selectNode = TreeTSI.SelectedNode;
            TSIDockContainer selectTSIDockContainer = (TSIDockContainer)MainBar.SelectedDockContainerItem;
            if (selectNode.Level == 1)
            {
                int DDSIndex = selectNode.Parent.Index;
                int ElementIndex = selectNode.Index;
                
                Copied_TSI_DDSElement = selectTSIDockContainer.tsi.listDDS[DDSIndex].ListDDS_element[ElementIndex];

            }
            else if (selectNode.Level == 0)
            {
                int DDSIndex = selectNode.Index;                
                Copied_TSI_DDS = selectTSIDockContainer.tsi.listDDS[DDSIndex];
            }
        }

        private void pasteTreeTSIMenuItem_Click(object sender, EventArgs e)
        {
            DevComponents.AdvTree.Node selectNode = TreeTSI.SelectedNode;
            TSIDockContainer selectTSIDockContainer = (TSIDockContainer)MainBar.SelectedDockContainerItem;
            if (selectNode.Level == 1)
            {
                if(Copied_TSI_DDSElement != null)
                {
                int DDSIndex = selectNode.Parent.Index;
                int ElementIndex = selectNode.Index;
                selectTSIDockContainer.tsi.listDDS[DDSIndex].ListDDS_element[ElementIndex] = Copied_TSI_DDSElement;
                selectNode.Name = Copied_TSI_DDSElement.Name;
                selectNode.Text = Copied_TSI_DDSElement.Name;
                }
            }
            else if (selectNode.Level == 0)
            {
                int DDSIndex = selectNode.Index;
                if(Copied_TSI_DDS != null)
                {
                    selectTSIDockContainer.tsi.listDDS[DDSIndex] = Copied_TSI_DDS;
                    selectNode.Nodes.Clear();
                    selectNode.Name = Copied_TSI_DDS.Path;
                    selectNode.Text = Copied_TSI_DDS.Path;
                    Copied_TSI_DDS.ListDDS_element.ForEach(delegate(TSI.DDS.DDSElement ddsElement)
                    {
                        DevComponents.AdvTree.Node newDDSElementNode = new DevComponents.AdvTree.Node();
                        newDDSElementNode.Name = ddsElement.Name;
                        newDDSElementNode.Text = ddsElement.Name;
                        selectNode.Nodes.Add(newDDSElementNode);
                    });

                }
            }
           
        }
        
        private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (propertyGrid.SelectedObject.GetType() == typeof(TSI.DDS))
            {
                DevComponents.AdvTree.Node selectNode = TreeTSI.SelectedNode;
                TSI.DDS selectDDS = (TSI.DDS)propertyGrid.SelectedObject;
                selectNode.Name = selectDDS.Path;
                selectNode.Text = selectDDS.Path;
            }
            else if (propertyGrid.SelectedObject.GetType() == typeof(TSI.DDS.DDSElement))
            {
                DevComponents.AdvTree.Node selectNode = TreeTSI.SelectedNode;
                TSI.DDS.DDSElement selectDDSElement = (TSI.DDS.DDSElement)propertyGrid.SelectedObject;
                selectNode.Name = selectDDSElement.Name;
                selectNode.Text = selectDDSElement.Name;
                TSIDockContainer selectTSIDockContainer = (TSIDockContainer)MainBar.SelectedDockContainerItem;
                selectTSIDockContainer.DrawAera(TreeTSI.SelectedNode.Parent.Index, TreeTSI.SelectedNode.Index);
            }
        }
        private void ribbonTabItem1_Click(object sender, EventArgs e)
        {

        }

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
            string UIPath = this.ClientFolder.FullName + "\\" + SolutionTree.SelectedNode.Parent.Text + "\\" + SolutionTree.SelectedNode.Text;
            string UIFolder = this.ClientFolder.FullName + "\\" + SolutionTree.SelectedNode.Parent.Text;
            UIDockContainer newUIDockContainer = new UIDockContainer(SolutionTree.SelectedNode.Text, SolutionTree.SelectedNode.Text, UIFolder , ClientFolder.FullName + "\\RES");

            newUIDockContainer.LoadUI(UIPath);
            
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

       
       

        



    }
}
