using System;
using System.Numeric;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Xna;
using Microsoft.Xna.Framework;
using Rose_online_UI_Editor.Forms.CustomControls;
using Rose_online_UI_Editor.Files_Handlers;

namespace Rose_online_UI_Editor.Forms
{
    partial class MainForm : Form
    {
        private void showCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XmlDockContainer newXmlDockContainer = new XmlDockContainer(SolutionTree.SelectedNode.Text, SolutionTree.SelectedNode.Text);
            string XmlPath;

            XmlPath = this.clientFolder.FullName + "\\" + SolutionTree.SelectedNode.Parent.Text + "\\" + SolutionTree.SelectedNode.Text;

            try
            {
                newXmlDockContainer.Load(XmlPath);
                AddDockContainer(newXmlDockContainer);
                AddLog(XmlPath + " succefully opened", LogType.MSG_INFO);
            }
            catch
            {
                AddLog(XmlPath + " can't be open", LogType.MSG_ERROR);
            }

        }
    }
}