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
            radioButton1.Checked = true;
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
            this.BackColor = ColorTranslator.FromHtml("#eb3b5a");

            result.Text = "";
            extract.Text = "";
            //---------------------------------------------------------------------
            //if radioButton1 is selected
            if (radioButton1.Checked == true)
            {
                foreach (string item in files)
                {
                    using (PdfDocument document = PdfDocument.Open(item))
                    {
                        for (var i = 0; i < document.NumberOfPages; i++)
                        {
                            // This starts at 1 rather than 0.
                            var page = document.GetPage(i + 1);

                            foreach (var word in page.GetWords())
                            {
                                if (word.Text == searcht.Text)
                                {
                                    result.Text += "Word Found In Page : " + (i + 1) + "\n";
                                    result.Text += "File " + item + "\n";
                                    result.Text += "Total Pages : " + document.NumberOfPages + "\n";
                                    result.Text += "------------------------------" + "\n";
                                    extract.Text += page.Text + "\n";
                                    extract.Text += item + "page :" + i + "-----------------\n";
                                }
                                if (result.Text == "")
                                {
                                    result.Text = "NOT FOUND";
                                }
                            }
                        }
                    }
                }
                this.BackColor = ColorTranslator.FromHtml("#eb3b5a");
            }
            //---------------------------------------------------------------------
            //if radioButton2 is selected
            if (radioButton2.Checked == true)
            {
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
            }
            this.BackColor = ColorTranslator.FromHtml("#273c75");
        }

        private void solarizedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = ColorTranslator.FromHtml("#273c75");
            result.BackColor = ColorTranslator.FromHtml("#192a56");
            extract.BackColor = ColorTranslator.FromHtml("#192a56");
            searcht.BackColor = ColorTranslator.FromHtml("#40739e");
            searcht.BackColor = ColorTranslator.FromHtml("#40739e");
            total.BackColor = ColorTranslator.FromHtml("#273c75");
            label1.BackColor = ColorTranslator.FromHtml("#273c75");
            label2.BackColor = ColorTranslator.FromHtml("#273c75");
            label3.BackColor = ColorTranslator.FromHtml("#273c75");
            label4.BackColor = ColorTranslator.FromHtml("#273c75");
            button1.BackColor = ColorTranslator.FromHtml("#40739e");

            result.ForeColor = ColorTranslator.FromHtml("#fbc531");
            extract.ForeColor = ColorTranslator.FromHtml("#fbc531");
            searcht.ForeColor = ColorTranslator.FromHtml("#fbc531");
            searcht.ForeColor = ColorTranslator.FromHtml("#fbc531");
            total.ForeColor = ColorTranslator.FromHtml("#fbc531");
            label1.ForeColor = ColorTranslator.FromHtml("#fbc531");
            label2.ForeColor = ColorTranslator.FromHtml("#fbc531");
            label3.ForeColor = ColorTranslator.FromHtml("#fbc531");
            label4.ForeColor = ColorTranslator.FromHtml("#fbc531");
            button1.ForeColor = ColorTranslator.FromHtml("#fbc531");
        }

        private void searcht_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                radioButton2.Checked = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                radioButton1.Checked = false;
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
    }
}