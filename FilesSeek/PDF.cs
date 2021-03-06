﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using System.Threading;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Reflection.Emit;
using PDFDetective;
using System.Linq;

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
            status.ForeColor = Color.Red;
            status.Text = "Scanning";
            this.Refresh();
            result.Items.Clear();
            var resultBuffer = new List<string>();
            // this.BackColor = ColorTranslator.FromHtml("#C4C4C4");

            clearlistbox();
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
                            resultBuffer.Add(item);
                            extract.Text += item + "page :" + i + "-----------------\n";
                            extract.Text += page.Text + "\n\n";
                        }
                    }
                }
            }
            if (extract.Text == "")
            {
                resultBuffer.Add("NOT FOUND");
                extract.Text = "NOT FOUND";
            }
            foreach (var item in resultBuffer.Distinct())
                result.Items.Add(item);
            //this.BackColor = ColorTranslator.FromHtml("#2D2D30");
            //result.Items.Add()
            //distrinct
            status.ForeColor = Color.Green;
            status.Text = "Done";
        }

        private void clearlistbox()
        {
            for (int i = 0; i < result.Items.Count; i++)
            {
                result.Items.RemoveAt(i);
            }
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

        private void result_DoubleClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", result.SelectedItem.ToString());
        }
    }
}