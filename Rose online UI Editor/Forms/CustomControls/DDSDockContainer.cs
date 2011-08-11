using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Rose_online_UI_Editor.Content_Manager;

using System.Windows.Media.Imaging;
namespace Rose_online_UI_Editor.Forms.CustomControls
{
    class DDSDockContainer : DevComponents.DotNetBar.DockContainerItem, ICustomControl
    {

        #region private variables
        private System.Windows.Forms.PictureBox pictureBox;
        private DevComponents.DotNetBar.PanelDockContainer codePanelDockContainer;
        private string DDSPath;
        #endregion

        #region constructors
        public DDSDockContainer(string name, string text)
        {
            this.Name = name;
            this.Text = text;
            // 
            // codeTextBox
            // 
            pictureBox = new System.Windows.Forms.PictureBox();
            pictureBox.Location = new System.Drawing.Point(3, 1);
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new System.Drawing.Size(657, 423);
            pictureBox.BackColor = System.Drawing.Color.Black;
            pictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox.TabIndex = 0;


            codePanelDockContainer = new DevComponents.DotNetBar.PanelDockContainer();
            // 
            // codePanelDockContainer
            // 
            codePanelDockContainer.Controls.Add(pictureBox);
            codePanelDockContainer.Location = new System.Drawing.Point(3, 28);
            codePanelDockContainer.Dock = DockStyle.Fill;
            codePanelDockContainer.Name = "codePanelDockContainer1";
            codePanelDockContainer.Size = new System.Drawing.Size(663, 427);
            codePanelDockContainer.AutoScroll = true;
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
                this.DDSPath = path;
                Texture2D texture = ContentManager.Instance().GetTexture(path);
                texture.Save("Temporary Files\\" + this.Text + ".png", ImageFileFormat.Png);
                FileStream fs = new FileStream("Temporary Files\\" + this.Text + ".png", FileMode.Open);
                pictureBox.Image = Image.FromStream(fs);
                fs.Close();
                File.Delete("Temporary Files\\" + this.Text + ".png");
                GC.Collect();
                StatusManager.AddLog("DDS successfully loaded : '" + path + "'");
            }
            catch(Exception e)
            {
                StatusManager.AddLog("Error to open : '" + path + "' ["+e.Message+"]");
            }

        }
        public void Reload()
        {
            Load(DDSPath);
        }
        public void Save()
        {
            //nothing here :p
        }
        #endregion

    }
}
