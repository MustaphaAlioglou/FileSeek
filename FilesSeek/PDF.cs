using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using System.Threading;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Reflection.Emit;
using PDFDetective;

namespace WindowsFormsApp8
{
    public partial class PDF : Form
    {
        private List<string> files = new List<string>();

        public PDF()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            files.Clear();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Filter =
             "(*.pdf)|*.pdf|" +
             "All files (*.*)|*.*";
            ofd.Title = "Select one or multiple pdf";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                total.Text = ofd.FileNames.Length.ToString();
                foreach (String file in ofd.FileNames)
                {
                    files.Add(file);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.BackColor = ColorTranslator.FromHtml("#C4C4C4");

            result.Text = "";
            extract.Text = "";
            //---------------------------------------------------------------------

            //---------------------------------------------------------------------
            //if radioButton2 is selected

            var regex = new Regex(searcht.Text);

            foreach (string item in files)
            {
                using (PdfDocument document = PdfDocument.Open(item))
                {
                    for (var i = 0; i < document.NumberOfPages; i++)
                    {
                        // This starts at 1 rather than 0.
                        var page = document.GetPage(i + 1);
                        if (regex.IsMatch(page.Text))
                        {
                            result.Text += "Word Found In Page : " + (i + 1) + "\n";
                            result.Text += "File " + item + "\n";
                            //result.Text += "Total Pages : " + document.NumberOfPages + "\n";
                            result.Text += "------------------------------" + "\n";
                            extract.Text += page.Text + "\n";
                            extract.Text += item + "page :" + i + "-----------------\n";
                        }
                    }
                    if (result.Text == "")
                    {
                        result.Text = "NOT FOUND";
                    }
                }
            }
            this.BackColor = ColorTranslator.FromHtml("#2D2D30");
        }

        private void searcht_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        private void powerPointSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PowerPoint ee = new PowerPoint();
            this.Hide();
            ee.ShowDialog();
            this.Show();
        }

        private void textFilesSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Text ee = new Text();
            this.Hide();
            ee.ShowDialog();
            this.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}