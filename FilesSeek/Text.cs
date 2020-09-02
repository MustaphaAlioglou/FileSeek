using Spire.Pdf.Exporting.XPS.Schema;
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
        private IEnumerable<string> FolderFiles;
        private List<string> files = new List<string>();

        public Text()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            files.Clear();

            FolderBrowserDialog ee = new FolderBrowserDialog();
            if (ee.ShowDialog() == DialogResult.OK)
                FolderFiles = Directory.EnumerateFiles(ee.SelectedPath);
            foreach (var item in FolderFiles)
            {
                if (item.EndsWith(".txt"))
                {
                    files.Add(item);
                }
            }
            total.Text = files.Count.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            extract.Clear();
            Regex rg = new Regex(@searcht.Text);
            foreach (var item in files)
            {
                string fileText = File.ReadAllText(item);
                MatchCollection shit = rg.Matches(fileText);
                foreach (var email in shit)
                {
                    result.Text += item + "\n";
                    extract.Text += email + "\n";
                }
            }
        }
    }
}