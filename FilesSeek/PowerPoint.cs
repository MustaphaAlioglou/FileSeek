using Spire.Presentation;
using System;
using System.Collections.Generic;
using System.Drawing;
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
            this.BackColor = ColorTranslator.FromHtml("#eb3b5a");
            result.Text = "";
            extract.Text = "";

            StringBuilder temp = new StringBuilder();
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
                        }
                    }
                    Regex ee = new Regex(searcht.Text);
                    if (ee.IsMatch(temp.ToString()))
                    {
                        result.Text += "File found on file : " + item + "\n";
                        result.Text += "Page :" + i + "\n";
                        extract.Text += temp;
                        extract.Text += item + "page :" + i + "-----------------\n\n";

                    }

                    temp.Clear();
                }
                

            }
            if (result.Text == "")
            {
                result.Text = "not found";
            }
            this.BackColor = ColorTranslator.FromHtml("#273c75");
        }
    }
}