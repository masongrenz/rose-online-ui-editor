using System;
using System.Windows.Forms;

using Rose_online_UI_Editor.Forms.CustomControls;
using Rose_online_UI_Editor.Files_Handlers;
using Rose_online_UI_Editor.Content_Manager;

namespace Rose_online_UI_Editor.Forms
{
    partial class MainForm : Form
    {
        private void showCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XmlDockContainer newXmlDockContainer = new XmlDockContainer(SolutionTree.SelectedNode.Text, SolutionTree.SelectedNode.Text);
            string XmlPath = ContentManager.GetRootPath()+ "\\"+ ContentManager.xmlFolderPartialPath+"\\"+SolutionTree.SelectedNode.Text;            
            newXmlDockContainer.Load(XmlPath);
            AddDockContainer(newXmlDockContainer);
        }
    }
}