using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDFDetective
{
    public partial class Text : Form
    {
        private List<string> textFilesPath = new List<string>();
        private int range = 5;
        private int totalfound = 0;

        public Text()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textFilesPath.Clear();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Filter =
             "*.txt|*.txt|" +
             "All files (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (var textfile in ofd.FileNames)
                {
                    textFilesPath.Add(textfile);
                }
            }
            total.Text = textFilesPath.Count().ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            status.ForeColor = Color.Red;
            status.Text = "Scanning";
            this.Refresh();
            totalfound = 0;
            List<string> resultList = new List<string>();
            if (rangee.Text != string.Empty)
                int.TryParse(rangee.Text, out range);

            int currentLine = 0;
            clearlist();
            extract.Clear();
            var regex = new Regex(searcht.Text);
            List<string> lines;
            try
            {
                foreach (var textfilepath in textFilesPath)
                {
                    lines = File.ReadLines(textfilepath).ToList();
                    foreach (var line in lines)
                    {
                        if (regex.IsMatch(line))
                        {
                            totalfound++;
                            resultList.Add(textfilepath);
                            extract.Text += Environment.NewLine + "File (" + Path.GetFileName(textfilepath) + ")" +
                                " ---------------------------------------" + Environment.NewLine;
                            for (int i = -range; i < 0; i++)
                            {
                                extract.Text += lines[currentLine + i] + Environment.NewLine;
                            }
                            extract.Text += line + " <--- Found it! :) Line: " + (currentLine + 1) + Environment.NewLine;
                            for (int i = 1; i <= range; i++)
                            {
                                extract.Text += lines[currentLine + i] + Environment.NewLine + Environment.NewLine;
                            }
                        }
                        currentLine++;
                    }
                    currentLine = 0;
                    lfound.Text = totalfound.ToString();
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Out of range error. Try a smaller sample range", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            HighlightText(extract, "<--- Found it! :) Line:", Color.LightSkyBlue);
            // checkforduplicates
            var distinctResults = resultList.Distinct().ToList();
            foreach (var item in distinctResults)
                result.Items.Add(item);

            status.ForeColor = Color.Green;
            status.Text = "Done";
        }

        private void clearlist()
        {
            for (int i = 0; i < result.Items.Count; i++)
            {
                result.Items.RemoveAt(i);
            }
        }

        private void result_DoubleClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", result.SelectedItem.ToString());
        }

        private void HighlightText(RichTextBox richtextbox, string word, Color color)
        {
            if (word == string.Empty)
                return;

            int s_start = richtextbox.SelectionStart, startIndex = 0, index;

            while ((index = richtextbox.Text.IndexOf(word, startIndex)) != -1)
            {
                richtextbox.Select(index, word.Length);
                richtextbox.SelectionColor = color;

                startIndex = index + word.Length;
            }

            richtextbox.SelectionStart = s_start;
            richtextbox.SelectionLength = 0;
            richtextbox.SelectionColor = Color.Black;
        }
    }
}