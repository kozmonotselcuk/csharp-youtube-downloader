using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MediaToolkit;
using MediaToolkit.Model;
using VideoLibrary;

namespace Youtube
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count > 0 || (textBox1.Text.IndexOf("youtube") > -1 && textBox1.Text.IndexOf(".com") > -1))
            {
                downloadFile();
            } else
            {
                textBox1.Focus();
            }
        }

        private void downloadFile()
        {
            YouTube youtube = YouTube.Default;
            Video vid = youtube.GetVideo(textBox1.Text);
            System.IO.File.WriteAllBytes(Application.StartupPath + "\\" + vid.FullName, vid.GetBytes());
            var inputfile = new MediaFile { Filename = Application.StartupPath + "\\" + vid.FullName };
            var outputfile = new MediaFile { Filename = $"{Application.StartupPath + "\\" + vid.FullName}.mp3" };
            button1.Enabled = false;
            if (true)
            {
                using (var engine = new Engine())
                {
                    engine.GetMetadata(inputfile);
                    if (checkBox1.Checked)
                    {
                        engine.Convert(inputfile, outputfile);
                    }
                }

                MessageBox.Show("Download Completed.");
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = true;
                MessageBox.Show("Oops! Houston we have a problem...");
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                this.Size = new Size(575, 495);
                listBox1.Visible = true;
                button2.Visible = true;
            } else
            {
                this.Size = new Size(575,135);
                listBox1.Visible = false;
                button2.Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.IndexOf("youtube") > -1 || textBox1.Text.IndexOf(".com") > -1)
            {
                listBox1.Items.Add(textBox1.Text.ToString());
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Remove(listBox1.SelectedItem);
        }
    }
}
