using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rose_online_UI_Editor.CustomControl
{
    class XmlDockContainer : DevComponents.DotNetBar.DockContainerItem
    {
            private System.Windows.Forms.RichTextBox codeTextBox;
            private DevComponents.DotNetBar.PanelDockContainer codePanelDockContainer;      
            public string XmlPath { get; set; }
            public XmlDockContainer(string Name, string Text)
             {
                 this.Name = Name;
                 this.Text = Text;
            // 
            // codeTextBox
            // 
            codeTextBox = new System.Windows.Forms.RichTextBox();
            codeTextBox.Location = new System.Drawing.Point(3, 1);
            codeTextBox.ScrollBars = RichTextBoxScrollBars.ForcedBoth;
            codeTextBox.Dock = DockStyle.Fill;
            codeTextBox.Name = "richTextBox1";
            codeTextBox.Size = new System.Drawing.Size(657, 423);
            codeTextBox.TabIndex = 0;
            codeTextBox.Text = "";
            
            codePanelDockContainer = new DevComponents.DotNetBar.PanelDockContainer();
            // 
            // codePanelDockContainer
            // 
            codePanelDockContainer.Controls.Add(codeTextBox);
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
           }

           public void LoadXml(string path)
           {
               this.XmlPath = path;

               using (System.IO.StreamReader sr = new System.IO.StreamReader(path))
               {
                   this.codeTextBox.Text = sr.ReadToEnd();
                   sr.Close();
               }        
           }
           public void Reload()
           {
               codeTextBox.Clear();
               LoadXml(XmlPath);
           }                 
           public void Save()
           {

               System.IO.StreamWriter sw = new System.IO.StreamWriter(XmlPath);
               this.codeTextBox.SelectAll();
               sw.Write(this.codeTextBox.Text);
               sw.Close();
           }

        #region Events
           
        #endregion
    }
}
