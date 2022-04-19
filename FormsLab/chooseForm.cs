using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.Json;

namespace FormsLab
{
    public partial class chooseForm : Form
    {
        private string dir;
        private mainForm parent;
        private List<MainInfo> Saves;

        public chooseForm(mainForm parent)
        {
            this.parent = parent;
            Saves = new List<MainInfo>();
            InitializeComponent();
        }

        private void chooseButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browser = new FolderBrowserDialog();

            if (browser.ShowDialog() == DialogResult.OK)
            {
                dir = browser.SelectedPath;
                dirTextBox.Text = dir;
                refreshButton_Click(null, null);
            }
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            if (listView.SelectedItems.Count == 0)
                return;

            string text = listView.SelectedItems[0].Text;

            if (text == string.Empty)
                return;

            parent.loadedInfo = findByTitle(text);
            parent.isLoaded = true;
            this.Close();
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            listView.Items.Clear();
            Saves.Clear();

            foreach (string name in Directory.GetFiles(dir, "*.json"))
            {
                StreamReader sr = new StreamReader(name);
                string s = sr.ReadToEnd();
                sr.Close();
                MainInfo Info = JsonSerializer.Deserialize<MainInfo>(s);

                ListViewItem item = new ListViewItem(Info.Title);
                item.SubItems.Add(Info.widthBoard.ToString());
                item.SubItems.Add(Info.heightBoard.ToString());
                item.SubItems.Add(Info.Difficulty);
                listView.Items.Add(item);
                Saves.Add(Info);
            }
        }

        private MainInfo findByTitle(string title)
        {
            foreach(MainInfo info in Saves)
            {
                if (info.Title == title)
                    return info;
            }

            return null;
        }
    }
}
