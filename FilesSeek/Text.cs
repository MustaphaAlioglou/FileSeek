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
            totalfound = 0;
            if (rangee.Text != string.Empty)
                int.TryParse(rangee.Text, out range);

            int currentLine = 0;
            result.Clear();
            extract.Clear();
            var regex = new Regex(searcht.Text);
            List<string> lines;
            foreach (var textfilepath in textFilesPath)
            {
                lines = File.ReadLines(textfilepath).ToList();
                foreach (var line in lines)
                {
                    if (regex.IsMatch(line))
                    {
                        totalfound++;
                        result.Text += textfilepath + Environment.NewLine + " ---------------------------------------" + Environment.NewLine;
                        extract.Text += Environment.NewLine + "File (" + Path.GetFileName(textfilepath) + ")" +
                            " ---------------------------------------" + Environment.NewLine;
                        for (int i = -range; i < 0; i++)
                        {
                            extract.Text += lines[currentLine + i] + Environment.NewLine;
                        }
                        extract.Text += line + Environment.NewLine;
                        for (int i = 1; i <= range; i++)
                        {
                            extract.Text += lines[currentLine + i] + Environment.NewLine;
                        }
                    }
                    currentLine++;
                }
                currentLine = 0;
            }
            lfound.Text = totalfound.ToString();
        }
    }
}