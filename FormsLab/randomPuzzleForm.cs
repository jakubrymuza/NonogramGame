using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FormsLab
{
    public partial class createForm : Form
    {
        private mainForm parent;
        public createForm(mainForm parent, bool isRandom)
        {
            this.parent = parent;
            InitializeComponent();
            this.MinimizeBox = false;
            this.MaximizeBox = false;

            this.Text = isRandom ? "New Random Game" : "Create your own Nonogram puzzle";

        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren())
            {
                parent.newWidth = Int32.Parse(widthTextBox.Text);
                parent.newHeight = Int32.Parse(heightTextBox.Text);
                parent.isNewGame = true;
                this.Close();
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string errorStr = " must be integer in range 2-15";

        private void widthTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (!isValidSize(widthTextBox.Text))
            {
                widthErrorProvider.SetError(widthTextBox, "Width" + errorStr);
                e.Cancel = true;
                return;
            }

            widthErrorProvider.SetError(widthTextBox, string.Empty);
            e.Cancel = false;
        }

        private void heightTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (!isValidSize(heightTextBox.Text))
            {
                heightErrorProvider.SetError(heightTextBox, "Height" + errorStr);
                e.Cancel = true;
                return;
            }

            widthErrorProvider.SetError(heightTextBox, string.Empty);
            e.Cancel = false;
        }

        private bool isValidSize(string text)
        {
            int x;
            try
            {
                x = Int32.Parse(text);
            }
            catch(Exception)
            {
                return false;
            }
            
            if(x<2 || x>15)
                return false;

            return true;
        }
    }
}
