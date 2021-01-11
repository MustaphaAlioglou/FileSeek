using Spire.Presentation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WindowsFormsApp8
{
    public partial class PowerPoint : Form
    {
        private List<string> files = new List<string>();

        public PowerPoint()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            files.Clear();
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Filter =
             "(*.ppt,*.pptx)|*.ppt;*.pptx|" +
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
            //this.BackColor = ColorTranslator.FromHtml("#C4C4C4");
            clearlistbox();
            extract.Text = "";
            result.Items.Clear();
            var extractBuilder = new StringBuilder();
            StringBuilder temp = new StringBuilder();
            List<string> resultBuffer = new List<string>();
            Regex ee = new Regex(searcht.Text);
            foreach (var item in files)
            {
                int i = 0;
                Presentation presentation = new Presentation(item, FileFormat.Pptx2013);
                foreach (ISlide slide in presentation.Slides)
                {
                    i++;
                    foreach (IShape shape in slide.Shapes)
                    {
                        if (shape is IAutoShape)
                        {
                            try
                            {
                                foreach (TextParagraph tp in (shape as IAutoShape).TextFrame.Paragraphs)
                                {
                                    temp.Append((tp.Text + Environment.NewLine));
                                }
                            }
                            catch (Exception eef)
                            {
                                MessageBox.Show("Error", "file corrupted : " + item + "\n" + eef.ToString());
                            }

                            if (ee.IsMatch(temp.ToString()))
                            {
                                resultBuffer.Add(item);
                                extractBuilder.Append(item + "page :" + i + "-----------------\n");
                                extractBuilder.Append(temp + Environment.NewLine + Environment.NewLine);
                            }
                        }
                    }

                    temp.Clear();
                }
            }
            if (extractBuilder.ToString() == "")
            {
                result.Items.Add("NOT FOUND");
                extractBuilder.Append("NOT FOUND");
            }
            extract.Text = extractBuilder.ToString();

            foreach (var item in resultBuffer.Distinct())
                result.Items.Add(item);

            this.BackColor = ColorTranslator.FromHtml("#2D2D30");


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

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", result.SelectedItem.ToString());
        }

     
        
    }
}