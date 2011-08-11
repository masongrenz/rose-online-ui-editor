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


using System.Windows.Media.Imaging;
namespace Rose_online_UI_Editor.CustomControl
{
    class DDSDockContainer : DevComponents.DotNetBar.DockContainerItem
    {
            
            private System.Windows.Forms.PictureBox pictureBox;
            private DevComponents.DotNetBar.PanelDockContainer codePanelDockContainer;
            private string DDSPath;
            public GraphicsDevice graphicsDevice { get; set; }
            public DDSDockContainer(string Name, string Text)
             {
                 this.Name = Name;
                 this.Text = Text;
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
            InitializeDevice();
            }
           public void InitializeDevice()
           {
               PresentationParameters presentationParameters = new PresentationParameters();
               presentationParameters.AutoDepthStencilFormat = DepthFormat.Depth24;
               presentationParameters.BackBufferCount = 1;
               presentationParameters.BackBufferFormat = SurfaceFormat.Color;
               presentationParameters.EnableAutoDepthStencil = true;
               presentationParameters.FullScreenRefreshRateInHz = 0;
               presentationParameters.IsFullScreen = false;
               presentationParameters.MultiSampleQuality = 0;
               presentationParameters.MultiSampleType = MultiSampleType.NonMaskable;
               presentationParameters.PresentationInterval = PresentInterval.One;
               presentationParameters.PresentOptions = PresentOptions.None;
               presentationParameters.RenderTargetUsage = RenderTargetUsage.DiscardContents;
               presentationParameters.SwapEffect = SwapEffect.Discard;
               this.graphicsDevice = new GraphicsDevice(GraphicsAdapter.DefaultAdapter, DeviceType.Hardware, pictureBox.Handle, presentationParameters);
             }
        
           public void LoadDDS(string Path)
            {
                    this.DDSPath = Path;                
                    Texture2D texture = Texture2D.FromFile(this.graphicsDevice , Path);
                    texture.Save("Temporary Files\\" + this.Text + ".png", ImageFileFormat.Png);
                    FileStream fs = new FileStream("Temporary Files\\" + this.Text + ".png", FileMode.Open);
                    this.pictureBox.Image = Image.FromStream(fs);
                    fs.Close();
                    texture.Dispose();
                    GC.Collect();
                    File.Delete("Temporary Files\\" + this.Text + ".png");              

            }
           public void Reload()
           {
               LoadDDS(DDSPath);
           }
        #region Events
           
        #endregion
    
    
    }
}
