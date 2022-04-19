namespace FormsLab
{
    partial class mainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mainStrip = new System.Windows.Forms.MenuStrip();
            this.newGame = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.random = new System.Windows.Forms.ToolStripMenuItem();
            this.choosePuzzle = new System.Windows.Forms.ToolStripMenuItem();
            this.loadPuzzle = new System.Windows.Forms.ToolStripMenuItem();
            this.create = new System.Windows.Forms.ToolStripMenuItem();
            this.createStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.createPuzzle = new System.Windows.Forms.ToolStripMenuItem();
            this.mainStrip.SuspendLayout();
            this.newGameStrip.SuspendLayout();
            this.createStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainStrip
            // 
            this.mainStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGame,
            this.create});
            this.mainStrip.Location = new System.Drawing.Point(0, 0);
            this.mainStrip.Name = "mainStrip";
            this.mainStrip.Size = new System.Drawing.Size(984, 24);
            this.mainStrip.TabIndex = 0;
            this.mainStrip.Text = "menuStrip1";
            // 
            // newGame
            // 
            this.newGame.DropDown = this.newGameStrip;
            this.newGame.Name = "newGame";
            this.newGame.Size = new System.Drawing.Size(77, 20);
            this.newGame.Text = "New Game";
            // 
            // newGameStrip
            // 
            this.newGameStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.random,
            this.choosePuzzle,
            this.loadPuzzle});
            this.newGameStrip.Name = "newGameStrip";
            this.newGameStrip.Size = new System.Drawing.Size(151, 70);
            this.newGameStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.newGameStrip_ItemClicked);
            // 
            // random
            // 
            this.random.Name = "random";
            this.random.Size = new System.Drawing.Size(150, 22);
            this.random.Text = "Random";
            // 
            // choosePuzzle
            // 
            this.choosePuzzle.Name = "choosePuzzle";
            this.choosePuzzle.Size = new System.Drawing.Size(150, 22);
            this.choosePuzzle.Text = "Choose Puzzle";
            // 
            // loadPuzzle
            // 
            this.loadPuzzle.Name = "loadPuzzle";
            this.loadPuzzle.Size = new System.Drawing.Size(150, 22);
            this.loadPuzzle.Text = "Load Puzzle";
            // 
            // create
            // 
            this.create.DropDown = this.createStrip;
            this.create.Name = "create";
            this.create.Size = new System.Drawing.Size(62, 20);
            this.create.Text = "Create...";
            // 
            // createStrip
            // 
            this.createStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createPuzzle});
            this.createStrip.Name = "createStrip";
            this.createStrip.OwnerItem = this.create;
            this.createStrip.Size = new System.Drawing.Size(145, 26);
            this.createStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.createStrip_ItemClicked);
            // 
            // createPuzzle
            // 
            this.createPuzzle.Name = "createPuzzle";
            this.createPuzzle.Size = new System.Drawing.Size(144, 22);
            this.createPuzzle.Text = "Create Puzzle";
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 761);
            this.Controls.Add(this.mainStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.mainStrip;
            this.MaximizeBox = false;
            this.Name = "mainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nonogram";
            this.mainStrip.ResumeLayout(false);
            this.mainStrip.PerformLayout();
            this.newGameStrip.ResumeLayout(false);
            this.createStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainStrip;
        private System.Windows.Forms.ToolStripMenuItem newGame;
        private System.Windows.Forms.ToolStripMenuItem create;
        private System.Windows.Forms.ContextMenuStrip newGameStrip;
        private System.Windows.Forms.ToolStripMenuItem random;
        private System.Windows.Forms.ToolStripMenuItem choosePuzzle;
        private System.Windows.Forms.ToolStripMenuItem loadPuzzle;
        private System.Windows.Forms.ContextMenuStrip createStrip;
        private System.Windows.Forms.ToolStripMenuItem createPuzzle;
    }
}

