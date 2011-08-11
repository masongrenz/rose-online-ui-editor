using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Rose_online_UI_Editor.Forms
{
    public partial class NewFileForm : Form
    {
        public NewFileForm()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;

        }
      
      
        private void listViewFile_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (listViewFile.SelectedItems[0].Text == "TSI File")
                {
                    textBox.Text = ".tsi";
                }
                else if (listViewFile.SelectedItems[0].Text == "Xml File")
                {
                    textBox.Text = ".xml";
                }
            }    
            catch
            {
             }
        }
            
       }

      
    }

