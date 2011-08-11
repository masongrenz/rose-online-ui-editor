namespace Rose_online_UI_Editor.Forms
{
    partial class NewFileForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("File type :", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "TSI File"}, "(aucun)", System.Drawing.Color.Empty, System.Drawing.SystemColors.Window, null);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Xml File");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewFileForm));
            this.Label = new System.Windows.Forms.Label();
            this.textBox = new System.Windows.Forms.TextBox();
            this.OkButton = new System.Windows.Forms.Button();
            this.listViewFile = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // Label
            // 
            this.Label.AutoSize = true;
            this.Label.Location = new System.Drawing.Point(12, 233);
            this.Label.Name = "Label";
            this.Label.Size = new System.Drawing.Size(60, 13);
            this.Label.TabIndex = 1;
            this.Label.Text = "File Name :";
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(78, 230);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(311, 20);
            this.textBox.TabIndex = 2;
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(395, 226);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(77, 26);
            this.OkButton.TabIndex = 0;
            this.OkButton.Text = "Add";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // listViewFile
            // 
            // 
            // 
            // 
            this.listViewFile.Border.Class = "ListViewBorder";
            listViewGroup1.Header = "File type :";
            listViewGroup1.Name = "File type :";
            this.listViewFile.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1});
            listViewItem1.Group = listViewGroup1;
            listViewItem1.StateImageIndex = 0;
            listViewItem2.Checked = true;
            listViewItem2.Group = listViewGroup1;
            listViewItem2.StateImageIndex = 1;
            this.listViewFile.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.listViewFile.Location = new System.Drawing.Point(12, 8);
            this.listViewFile.MultiSelect = false;
            this.listViewFile.Name = "listViewFile";
            this.listViewFile.Size = new System.Drawing.Size(460, 212);
            this.listViewFile.StateImageList = this.imageList;
            this.listViewFile.TabIndex = 3;
            this.listViewFile.UseCompatibleStateImageBehavior = false;
            
            this.listViewFile.SelectedIndexChanged += new System.EventHandler(this.listViewFile_SelectedIndexChanged);
           
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "xml.png");
            this.imageList.Images.SetKeyName(1, "Vista Monitor.ico");
            // 
            // NewFileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 264);
            this.Controls.Add(this.listViewFile);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.Label);
            this.Controls.Add(this.OkButton);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewFileForm";
            this.Text = "New file :";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Label;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.Button OkButton;
        private DevComponents.DotNetBar.Controls.ListViewEx listViewFile;
        private System.Windows.Forms.ImageList imageList;

    }
}