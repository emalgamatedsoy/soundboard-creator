using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace soundboard_creator
{
    public partial class SoundboardCreator : Form
    {
        static String directory = "";
        public SoundboardCreator()
        {
            InitializeComponent();
        }

        private void browseBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog openFolderDialog = new FolderBrowserDialog();
            if (openFolderDialog.ShowDialog() == DialogResult.OK)
            {
                labelFileSelect.Text = "Folder Selected";
                directory = openFolderDialog.SelectedPath;
            }
        }

        private void createBtn_Click(object sender, EventArgs e)
        {
            if (!directory.Equals(""))
            {
                string[] files = System.IO.Directory.GetFiles(directory);
                string soundboardhtml = "<!doctype html>";
                soundboardhtml += "<html><head><title>Soundboard</title></head>\n";
                soundboardhtml += " <body>\n";
                soundboardhtml += "<script>\n";
                soundboardhtml += "var music = new Audio(); \n";
                soundboardhtml += "function playMusic(file) { music.pause(); music = new Audio(file); music.play(); }\n";
                soundboardhtml += "function play(id) {playMusic(document.getElementById(id).src); }\n";
                soundboardhtml += "</script>";
                int count = 0;
                foreach (string file in files)
                {
                    if(file.Contains(".mp3") || file.Contains(".ogg") || file.Contains(".wav"))
                    {
                        count++;
                        string id = "audio" + count;
                        string filename = Path.GetFileNameWithoutExtension(file);
                        filename = filename.Replace(@"\\" , @"\");
                        soundboardhtml += "<input type =" + '"' + "button" + '"' + " value=" + '"' + filename + '"' + "onclick=" + '"' + "play('" + id + "')" + '"' + ">\n";
                        soundboardhtml += "<audio id=" + '"' + id + '"' + " src="+ '"' + file + '"' + "></audio>\n";
                    }
                }
                soundboardhtml += "</body></html>";
                using (StreamWriter writer = new StreamWriter(directory + "\\soundboard.html"))
                {
                    writer.Write(soundboardhtml);
                }
                MessageBox.Show("Complete");
            }
        }
    }
}
