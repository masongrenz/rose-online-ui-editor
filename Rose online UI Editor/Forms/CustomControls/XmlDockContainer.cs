using System;
using System.Collections.Generic;
using System.Windows.Forms;


using Rose_online_UI_Editor.Mouse;
using Rose_online_UI_Editor.Forms;

namespace Rose_online_UI_Editor.Forms.CustomControls
{
    class XmlDockContainer : DevComponents.DotNetBar.DockContainerItem , ICustomControl
    {
        #region variables
        private System.Windows.Forms.RichTextBox codeTextBox;
            private DevComponents.DotNetBar.PanelDockContainer codePanelDockContainer;      
            public string xmlPath { get; set; }
        #endregion
            #region constructors
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
            #endregion

            #region methods
            public void Load(string path)
           {
               try
               {
                   this.xmlPath = path;
                   using (System.IO.StreamReader sr = new System.IO.StreamReader(path))
                   {
                       this.codeTextBox.Text = sr.ReadToEnd();
                       sr.Close();
                   }
                   StatusManager.AddLog("XML successfully loaded : \"" + path+"\"");
               }
                catch(Exception e)
               {
                   StatusManager.AddLog("XML can't be load : " + e.Message);
               }
           }
           public void Reload()
           {
               codeTextBox.Clear();
               Load(xmlPath);
           }                 
           public void Save()
           {

               System.IO.StreamWriter sw = new System.IO.StreamWriter(xmlPath);
               this.codeTextBox.SelectAll();
               sw.Write(this.codeTextBox.Text);
               sw.Close();
           }
           public void SetMouseType(MouseType mType)
           {

           }
            #endregion

        #region Events

        #endregion
    }
}
