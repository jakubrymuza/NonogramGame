namespace FormsLab
{
    partial class chooseForm
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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("ListViewGroup", System.Windows.Forms.HorizontalAlignment.Left);
            this.dirTextBox = new System.Windows.Forms.TextBox();
            this.chooseButton = new System.Windows.Forms.Button();
            this.listView = new System.Windows.Forms.ListView();
            this.titleColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.widthColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.heightColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.difficultyColumnHeader = new System.Windows.Forms.ColumnHeader();
            this.loadButton = new System.Windows.Forms.Button();
            this.refreshButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dirTextBox
            // 
            this.dirTextBox.Location = new System.Drawing.Point(13, 13);
            this.dirTextBox.Name = "dirTextBox";
            this.dirTextBox.ReadOnly = true;
            this.dirTextBox.Size = new System.Drawing.Size(536, 23);
            this.dirTextBox.TabIndex = 0;
            // 
            // chooseButton
            // 
            this.chooseButton.Location = new System.Drawing.Point(555, 13);
            this.chooseButton.Name = "chooseButton";
            this.chooseButton.Size = new System.Drawing.Size(117, 23);
            this.chooseButton.TabIndex = 1;
            this.chooseButton.Text = "Choose directory";
            this.chooseButton.UseVisualStyleBackColor = true;
            this.chooseButton.Click += new System.EventHandler(this.chooseButton_Click);
            // 
            // listView
            // 
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.titleColumnHeader,
            this.widthColumnHeader,
            this.heightColumnHeader,
            this.difficultyColumnHeader});
            this.listView.HideSelection = false;
            this.listView.Location = new System.Drawing.Point(12, 42);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(660, 278);
            this.listView.TabIndex = 2;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            // 
            // titleColumnHeader
            // 
            this.titleColumnHeader.Text = "Title";
            this.titleColumnHeader.Width = 160;
            // 
            // widthColumnHeader
            // 
            this.widthColumnHeader.Text = "Width";
            // 
            // heightColumnHeader
            // 
            this.heightColumnHeader.Text = "Height";
            // 
            // difficultyColumnHeader
            // 
            this.difficultyColumnHeader.Text = "Difficulty";
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(555, 326);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(117, 23);
            this.loadButton.TabIndex = 3;
            this.loadButton.Text = "Load puzzle";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(12, 326);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(75, 23);
            this.refreshButton.TabIndex = 4;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // chooseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 361);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.chooseButton);
            this.Controls.Add(this.dirTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "chooseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Choose one of the puzzles";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox dirTextBox;
        private System.Windows.Forms.Button chooseButton;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.ColumnHeader titleColumnHeader;
        private System.Windows.Forms.ColumnHeader widthColumnHeader;
        private System.Windows.Forms.ColumnHeader heightColumnHeader;
        private System.Windows.Forms.ColumnHeader difficultyColumnHeader;
    }
}