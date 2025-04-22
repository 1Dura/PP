using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.ES11;
using OpenTK.Graphics.OpenGL;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace tomograph2
{
    public partial class Form1 : Form
    {
        private Bin bin = new Bin();
        private View view = new View();
        private bool loaded = false;
        private int currentLayer = 0;
        private bool needReload = false;
        private bool textureMode = false;
        string quadMode = "quad";
        private int FrameCount = 0;
        private DateTime NextFPSUpdate = DateTime.Now.AddSeconds(1);

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Application.Idle += Application_Idle;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string str = dialog.FileName;
                bin.readBIN(str);
                view.SetUpView(glControl1.Width, glControl1.Height);
                loaded = true;
                trackBar1.Maximum = Bin.Z - 1;
                glControl1.Invalidate();
            }
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            if (loaded)
            {
                if (textureMode)
                {
                    if (needReload)
                    {
                        view.generateTextureImage(currentLayer);
                        view.Load2DTexture();
                        needReload = false;
                    }
                    view.DrawTexture();
                }
                else
                {
                    if (needReload)
                    {
                        if (quadMode=="stripquad")
                        {
                            view.DrawStripQuads(currentLayer);
                        }
                        else if(quadMode=="quad")
                        {
                            view.DrawQuads(currentLayer);
                        }
                        else if (quadMode == "striptriangle")
                        {
                            view.DrawStripTriangle(currentLayer);
                        }
                    }
                }
                glControl1.SwapBuffers();
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            currentLayer = trackBar1.Value;
            needReload = true;
            glControl1.Invalidate();
        }
        void Application_Idle(object sender, EventArgs e)
        {
            while (glControl1.IsIdle)
            {
                displayFPS();
                glControl1.Invalidate();
            }
        }
        void displayFPS()
        {
            if (DateTime.Now >= NextFPSUpdate)
            {
                this.Text = String.Format("CT Visualizer (fps={0})", FrameCount);
                NextFPSUpdate = DateTime.Now.AddSeconds(1);
                FrameCount = 0;
            }
            FrameCount++;
        }
        private void textureToolStripMenuItem_Click(object sender, EventArgs e) //texture
        {
            textureMode = true;
            needReload = true;
            glControl1.Invalidate();
            label5.Text = "texture";
        }

        private void quadsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textureMode = false;
            needReload = true;
            glControl1.Invalidate();
        }

        private void WidthTrackBar_Scroll(object sender, EventArgs e)
        {
            view.SetWidthTF(WidthTrackBar.Value);
            needReload = true;
            glControl1.Invalidate();
        }

        private void MinTrackBar_Scroll(object sender, EventArgs e)
        {
            view.SetMinTF(MinTrackBar.Value);
            needReload = true;
            glControl1.Invalidate();
        }

        private void defaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            quadMode = "quad";
            needReload = true;
            glControl1.Invalidate();
            label5.Text = quadMode;
        }

        private void quadStripToolStripMenuItem_Click(object sender, EventArgs e)
        {
            quadMode = "stripquad";
            needReload = true;
            glControl1.Invalidate();
            label5.Text = quadMode;
        }

        private void triangleModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            quadMode = "striptriangle";
            needReload = true;
            glControl1.Invalidate();
            label5.Text = quadMode;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            
        }
    }
}
