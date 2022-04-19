using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using System.Text.Json;

namespace FormsLab
{
    public partial class mainForm : Form
    {
        public static int defaultWidth = 10;
        public static int defaultHeight = 10;
        private const int widthWindow = 1000;
        private const int heightWindow = 800;
        private const int sizeTile = 30;

        private Tile[,] tiles;
        private Label[] XLabels;
        private Label[] YLabels;
        private Label WinLabel;
        public bool isNewGame = false;
        public int newWidth;
        public int newHeight;
        private bool isWon = false;
        private bool isCreate = false;
        MainInfo Info;

        public mainForm()
        {
            InitializeComponent();

            NewRandomGame(defaultWidth, defaultHeight);
        }

        private void NewRandomGame(int x, int y)
        {
            Info = new MainInfo(x, y);

            InitializeBoard(true);

            UpdateLists(true);

            UpdateLabels(true);
        }

        private void InitializeBoard(bool randomize)
        {
            tiles = new Tile[Info.widthBoard, Info.heightBoard];
            Random random = new Random();

            for (int i = 0; i < Info.widthBoard; ++i)
            {
                for (int j = 0; j < Info.heightBoard; ++j)
                {
                    tiles[i, j] = new Tile(i, j);

                    tiles[i, j].TileButton.Location = new Point(GetXOffset() + i * sizeTile, GetYOffset() + j * sizeTile);
                    tiles[i, j].TileButton.Name = i.ToString() + "_" + j.ToString();
                    tiles[i, j].TileButton.Size = new Size(sizeTile, sizeTile);
                    tiles[i, j].TileButton.UseVisualStyleBackColor = true;
                    if (!isCreate)
                        tiles[i, j].TileButton.MouseDown += new MouseEventHandler(TileClick);
                    else tiles[i, j].TileButton.MouseDown += new MouseEventHandler(TileCreationClick);
                    tiles[i, j].TileButton.FlatStyle = FlatStyle.Flat;
                    tiles[i, j].TileButton.FlatAppearance.BorderSize = 1;
                    tiles[i, j].TileButton.TabStop = false;

                    if (randomize && random.Next(0, 2) == 0)
                    {
                        tiles[i, j].ValidState = State.FILLED;
                    }

                    tiles[i, j].TileButton.BackColor = Color.White;

                    this.Controls.Add(tiles[i, j].TileButton);
                    tiles[i, j] = tiles[i, j];
                }
            }

        }

        private void UpdateLists(bool all, int x = 0, int y = 0)
        {
            if (all)
                InitializeLists();

            var (rangeX, rangeY) = GetRange(all, x, y);


            UpdateRowColumn(rangeX, Info.heightBoard, Info.XNums, false);
            UpdateRowColumn(rangeY, Info.widthBoard, Info.YNums, true);

        }

        private void UpdateRowColumn(IEnumerable<int> range, int limit, List<int>[] Nums, bool swap)
        {
            foreach (int i in range)
            {
                Nums[i].Clear();
                int counter = 0;
                bool isZero = true;
                for (int j = 0; j < limit; ++j)
                {
                    State valid = swap ? tiles[j, i].ValidState : tiles[i, j].ValidState;
                    if (valid == State.FILLED)
                        ++counter;
                    if (valid == State.EMPTY)
                        if (counter != 0)
                        {
                            isZero = false;
                            Nums[i].Add(counter);
                            counter = 0;
                        }
                }

                if (counter != 0)
                {
                    isZero = false;
                    Nums[i].Add(counter);
                }

                if (isZero)
                    Nums[i].Add(0);
            }
        }

        private void InitializeLists()
        {
            Info.XNums = new List<int>[Info.widthBoard];
            Info.YNums = new List<int>[Info.heightBoard];

            for (int i = 0; i < Info.widthBoard; ++i)
                Info.XNums[i] = new List<int>();

            for (int i = 0; i < Info.heightBoard; ++i)
                Info.YNums[i] = new List<int>();

        }

        private void UpdateLabels(bool all, int x = 0, int y = 0)
        {
            if (all)
                InitializeLabels();

            var (rangeX, rangeY) = GetRange(all, x, y);

            UpdateRowColumnLabel(XLabels, rangeX, Info.XNums, false, all);
            UpdateRowColumnLabel(YLabels, rangeY, Info.YNums, true, all);
        }

        private void UpdateRowColumnLabel(Label[] labels, IEnumerable<int> range, List<int>[] Nums, bool vert, bool all)
        {
            char sep = vert ? ' ' : '\n';
            foreach (int i in range)
            {
                string text = "";

                foreach (int a in Nums[i])
                {
                    text += a.ToString() + sep;
                }

                string name = "label";
                if (vert)
                {
                    name += "Y";
                    labels[i].Location = new Point(GetXOffset() - Info.YNums[i].Count * 9 - 3, GetYOffset() + i * sizeTile + 5);
                    labels[i].Size = new Size(10, sizeTile);

                }
                else
                {
                    name += "X";
                    labels[i].Location = new Point(GetXOffset() + i * sizeTile + 8, GetYOffset() - sizeTile - Info.XNums[i].Count * 15 + 25);
                    labels[i].Size = new Size(sizeTile, 10);
                }

                labels[i].AutoSize = true;
                labels[i].Name = name + i.ToString();
                labels[i].TextAlign = ContentAlignment.BottomCenter;
                labels[i].Text = text;
                if (all) this.Controls.Add(labels[i]);
            }
        }

        private void InitializeLabels()
        {
            XLabels = new Label[Info.widthBoard];
            YLabels = new Label[Info.heightBoard];

            for (int i = 0; i < Info.widthBoard; ++i)
                XLabels[i] = new Label();

            for (int i = 0; i < Info.heightBoard; ++i)
                YLabels[i] = new Label();

        }

        private (IEnumerable<int>, IEnumerable<int>) GetRange(bool all, int x, int y)
        {
            int startX = x, countX = 1, startY = y, countY = 1;

            if (all)
            {
                startX = startY = 0;
                countX = Info.widthBoard;
                countY = Info.heightBoard;
            }

            return (Enumerable.Range(startX, countX), Enumerable.Range(startY, countY));
        }

        private bool IsSolved() => IsSolvedRowColumn(true) && IsSolvedRowColumn(false);

        private bool IsSolvedRowColumn(bool vert)
        {
            (int limit1, int limit2, List<int>[] Nums) = SolveArgs(vert);

            for (int i = 0; i < limit1; ++i)
            {
                Queue<int> q = new Queue<int>();
                foreach (int x in Nums[i])
                    q.Enqueue(x);

                bool first = false;
                int counter = q.Dequeue();
                for (int j = 0; j < limit2; ++j)
                {
                    State state = vert ? tiles[j, i].CurrentState : tiles[i, j].CurrentState;

                    if (state == State.FILLED)
                    {
                        --counter;
                        first = true;
                    }
                    else
                    {
                        if (counter != 0)
                        {
                            if (first)
                                return false;
                        }
                        else
                        {
                            first = false;
                            if (q.Count > 0)
                                counter = q.Dequeue();
                            else counter = 0;
                        }

                    }

                    if (counter < 0)
                        return false;
                }

                if (counter != 0)
                    return false;


            }

            return true;
        }

        private (int, int, List<int>[]) SolveArgs(bool vert)
        {
            if (vert)
                return (Info.heightBoard, Info.widthBoard, Info.YNums);
            return (Info.widthBoard, Info.heightBoard, Info.XNums);
        }

        private void TileClick(object sender, EventArgs e)
        {
            UpdateTile((Button)sender, (MouseEventArgs)e, false);

            if (!isCreate && IsSolved())
                GameWon();

        }

        private void UpdateTile(Button sender, MouseEventArgs e, bool modValid)
        {
            (int i, int j) = GetCoords(sender.Name);

            if (e.Button == MouseButtons.Left)
                LeftClick(i, j);
            else if (e.Button == MouseButtons.Right)
                RightClick(i, j);

            if (modValid)
                tiles[i, j].ValidState = tiles[i, j].CurrentState == State.FILLED ? State.FILLED : State.EMPTY;
        }

        private void LeftClick(int i, int j)
        {
            switch (tiles[i, j].CurrentState)
            {
                case State.CROSSED:
                    tiles[i, j].TileButton.BackgroundImage = null;
                    goto case State.EMPTY;

                case State.EMPTY:
                    tiles[i, j].TileButton.BackColor = Color.Black;
                    tiles[i, j].CurrentState = State.FILLED;
                    break;

                case State.FILLED:
                    tiles[i, j].TileButton.BackColor = Color.White;
                    tiles[i, j].CurrentState = State.EMPTY;
                    break;

            }
        }

        private void RightClick(int i, int j)
        {
            switch (tiles[i, j].CurrentState)
            {
                case State.EMPTY:
                    goto case State.FILLED;

                case State.FILLED:
                    tiles[i, j].CurrentState = State.CROSSED;
                    tiles[i, j].TileButton.BackgroundImage = Properties.Resources.crossed;
                    break;

                case State.CROSSED:
                    tiles[i, j].TileButton.BackgroundImage = null;
                    tiles[i, j].TileButton.BackColor = Color.White;
                    tiles[i, j].CurrentState = State.EMPTY;
                    break;

            }
        }

        private void GameWon()
        {
            for (int i = 0; i < Info.widthBoard; ++i)
            {
                for (int j = 0; j < Info.heightBoard; ++j)
                {
                    tiles[i, j].TileButton.Enabled = false;
                }
            }

            int winSize = 100;

            WinLabel = new Label();
            WinLabel.AutoSize = true;
            WinLabel.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            WinLabel.Location = new Point(widthWindow / 2 - winSize, heightWindow - winSize);
            WinLabel.Name = "WinLabel";
            WinLabel.Size = new Size(winSize, winSize);
            WinLabel.TextAlign = ContentAlignment.MiddleCenter;
            WinLabel.Text = "Congratulations!";
            this.Controls.Add(WinLabel);
            isWon = true;

        }

        private class Tile
        {
            public Button TileButton;
            public State CurrentState;
            public State ValidState;

            public Tile(int x, int y)
            {
                CurrentState = ValidState = State.EMPTY;
                TileButton = new Button();
            }
        }

        private enum State
        {
            EMPTY, FILLED, CROSSED
        }

        // nazwy przycisków to: "i_j"
        private (int, int) GetCoords(string name)
        {
            string[] sub = name.Split("_");

            return (Int32.Parse(sub[0]), Int32.Parse(sub[1]));
        }

        private int GetXOffset() => (widthWindow - Info.widthBoard * sizeTile) / 2 + (isCreate ? 200 : 0);

        private int GetYOffset() => (heightWindow - Info.heightBoard * sizeTile) / 2;

        private void DestroyBoard()
        {

            for (int i = 0; i < Info.widthBoard; ++i)
            {
                for (int j = 0; j < Info.heightBoard; ++j)
                {
                    this.Controls.Remove(tiles[i, j].TileButton);
                }
            }

            for (int i = 0; i < Info.widthBoard; ++i)
            {
                this.Controls.Remove(XLabels[i]);
            }

            for (int i = 0; i < Info.heightBoard; ++i)
            {
                this.Controls.Remove(YLabels[i]);
            }

            if (isWon)
                this.Controls.Remove(WinLabel);
            isWon = false;

            this.Controls.Remove(settingsGroupBox);


        }

        private delegate void newGameDeleg(int x, int y);
        private void newGameStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "Random":
                    createNewGame(NewRandomGame);
                    break;

                case "Load Puzzle":
                    LoadPuzzle();
                    break;

                case "Choose Puzzle":
                    ChoosePuzzle();
                    break;
            }
        }

        private void createNewGame(newGameDeleg mode)
        {
            isCreate = false;
            createForm random = new createForm(this, true);
            random.ShowDialog();

            if (isNewGame)
            {
                DestroyBoard();
                mode(newWidth, newHeight);
            }

            isNewGame = false;
        }

        private void createStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            createNewGame(NewCreateGame);
        }

        private void NewCreateGame(int x, int y)
        {
            Info.widthBoard = x;
            Info.heightBoard = y;
            isCreate = true;

            InitializeSettings();

            InitializeBoard(false);

            UpdateLists(true);

            UpdateLabels(true);
        }

        private void TileCreationClick(object sender, EventArgs e)
        {
            UpdateTile((Button)sender, (MouseEventArgs)e, true);

            (int i, int j) = GetCoords(((Button)sender).Name);

            UpdateLists(false, i, j);
            UpdateLabels(false, i, j);
        }

        private System.Windows.Forms.GroupBox settingsGroupBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label difficultyLabel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.TextBox difficultyTextBox;
        private System.Windows.Forms.TextBox titleTextBox;
        private void InitializeSettings()
        {
            // wygenerowane przez designer

            this.settingsGroupBox = new System.Windows.Forms.GroupBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.difficultyLabel = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.difficultyTextBox = new System.Windows.Forms.TextBox();
            this.titleTextBox = new System.Windows.Forms.TextBox();
            this.Controls.Add(settingsGroupBox);
            // 
            // settingsGroupBox
            // 
            this.settingsGroupBox.Controls.Add(this.saveButton);
            this.settingsGroupBox.Controls.Add(this.difficultyLabel);
            this.settingsGroupBox.Controls.Add(this.titleLabel);
            this.settingsGroupBox.Controls.Add(this.difficultyTextBox);
            this.settingsGroupBox.Controls.Add(this.titleTextBox);
            this.settingsGroupBox.Location = new System.Drawing.Point(12, 27);
            this.settingsGroupBox.Name = "settingsGroupBox";
            this.settingsGroupBox.Size = new System.Drawing.Size(200, 118);
            this.settingsGroupBox.TabIndex = 2;
            this.settingsGroupBox.TabStop = false;
            this.settingsGroupBox.Text = "Puzzle Settings";
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new System.Drawing.Point(94, 22);
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new System.Drawing.Size(100, 23);
            this.titleTextBox.TabIndex = 0;
            // 
            // difficultyTextBox
            // 
            this.difficultyTextBox.Location = new System.Drawing.Point(94, 51);
            this.difficultyTextBox.Name = "difficultyTextBox";
            this.difficultyTextBox.Size = new System.Drawing.Size(100, 23);
            this.difficultyTextBox.TabIndex = 1;
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Location = new System.Drawing.Point(23, 25);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(65, 15);
            this.titleLabel.TabIndex = 2;
            this.titleLabel.Text = "Puzzle Title";
            // 
            // difficultyLabel
            // 
            this.difficultyLabel.AutoSize = true;
            this.difficultyLabel.Location = new System.Drawing.Point(33, 54);
            this.difficultyLabel.Name = "difficultyLabel";
            this.difficultyLabel.Size = new System.Drawing.Size(55, 15);
            this.difficultyLabel.TabIndex = 3;
            this.difficultyLabel.Text = "Difficulty";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(119, 80);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 4;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Info.Title = titleTextBox.Text;
            Info.Difficulty = difficultyTextBox.Text;

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "JSON files (*.json)|*.json";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string s = JsonSerializer.Serialize<MainInfo>(Info);
                StreamWriter sw = new StreamWriter(dialog.FileName);
                sw.Write(s);
                sw.Close();
            }


        }

        private void LoadPuzzle()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "JSON files (*.json)|*.json";

            if (dialog.ShowDialog() == DialogResult.OK)
                LoadPuzzle(dialog.FileName);

        }

        private void LoadPuzzle(string name)
        {
            DestroyBoard();
            isCreate = false;
            StreamReader sr = new StreamReader(name);
            string s = sr.ReadToEnd();
            sr.Close();
            Info = JsonSerializer.Deserialize<MainInfo>(s);

            InitializeBoard(false);
            UpdateLabels(true);
        }

        public MainInfo loadedInfo;
        public bool isLoaded;

        private void ChoosePuzzle()
        {
            chooseForm choose = new chooseForm(this);
            choose.ShowDialog();
            isCreate = false;

            if (isLoaded)
            {
                DestroyBoard();
                Info = loadedInfo;
                InitializeBoard(false);
                UpdateLabels(true);
            }

            isLoaded = false;
        }

    }

    public class MainInfo
    {
        public MainInfo(int x, int y)
        {
            widthBoard = x;
            heightBoard = y;
        }

        public MainInfo()
        {
        }

        public List<int>[] XNums { get; set; }
        public List<int>[] YNums { get; set; }
        public int widthBoard { get; set; }
        public int heightBoard { get; set; }
        public string Title { get; set; }
        public string Difficulty { get; set; }
    }

}
