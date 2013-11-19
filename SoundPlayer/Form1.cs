using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace SoundPlayer
{
    public partial class Form1 : Form
    {
        String baseFolder;
        List<String> filenames;
        List<String> filepaths;

        public Form1()
        {
            InitializeComponent();

            baseFolder = "";
            filenames = new List<string>();
            filepaths = new List<string>();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                baseFolder = folderBrowserDialog1.SelectedPath;
                textBox1.Text = baseFolder;
                loadSamples(baseFolder);
            }
        }

        private void loadSamples(string path)
        {
            foreach (string file in Directory.EnumerateFiles(path, "*.wav", SearchOption.AllDirectories))
            {
                filepaths.Add(file);
                filenames.Add(file.Substring(file.LastIndexOf('\\')+1));
            }

            listBox1.DataSource = filenames;
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = listBox1.SelectedIndex;
            // MessageBox.Show("" + index + ": " + filepaths[index]);
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(filepaths[index]);
            try
            {
                if (Control.ModifierKeys == Keys.Alt)
                    player.Play();
                else
                    player.Play();
            }
            catch (Exception exception)
            {
                filenames[index] += "(Broken?)";
                listBox1.DataSource = filenames;
            }
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            label1_Click(null, null);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control) 
            {
                int index = listBox1.SelectedIndex;
                Process.Start("explorer.exe", "/select," + filepaths[index]);
            }
        }
    }
}
