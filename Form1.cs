using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace _2_lab_computer_graphics
{

    public partial class Form1 : Form
    {
        private Stack<Bitmap> history = new Stack<Bitmap>(); // История изменений
        private Bitmap currentImage; // Текущее изображение

        Bitmap image;
        private void LoadImage(Bitmap newImage)
        {
            currentImage = (Bitmap)newImage.Clone();
            pictureBox1.Image = currentImage;
            pictureBox1.Refresh();
        }

        private void ApplyFilter(Filters filter)
        {
            
        }


        public Form1()
        {

            InitializeComponent();
            this.KeyPreview = true; // Разрешаем юзать клаву
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);

            this.AllowDrop = true; // разрешаем дропать 
            pictureBox1.AllowDrop = true; // ещё и бокс привлекаем

            pictureBox1.DragEnter += new DragEventHandler(PictureBox_DragEnter);
            pictureBox1.DragDrop += new DragEventHandler(PictureBox_DragDrop);
        }

        // вставка картинки из буфера
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V) // Проверяем сочетание кнопочек
            {
                PasteImageFromClipboard(); 
            }
            if(e.Control && e.KeyCode == Keys.Z)
            {
                ImageBack();
            }
        }
        private void ImageBack()
        {
            if (history.Count > 0)
            {
                image = history.Pop();
                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }
            else
            {
                pictureBox1.Image = null;
                pictureBox1.Refresh();
                Dpicture.Visible = true;

            }
        }
        private void PasteImageFromClipboard()
        {
            if (Clipboard.ContainsImage()) // проверка на пустоту буфера
            {
                image = new Bitmap(Clipboard.GetImage());
                pictureBox1.Image = image;
                pictureBox1.Refresh();
                Dpicture.Visible = false;
            }
            else
            {
                MessageBox.Show("В буфере обмена нет изображения!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // загрузка картинки из проводника
        private void PictureBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }
        private void PictureBox_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length == 1)
            {
                try
                {
                    image = new Bitmap(Image.FromFile(files[0]));
                    pictureBox1.Image = image;
                    pictureBox1.Refresh();
                    Dpicture.Visible = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при загрузке изображения: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Вы попытались загрузить более одного изображения! ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Console.WriteLine("🔵 Приложение запущено.");

        }
        // загрузить картинку
        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files | *.png; *.jpg; *.bmp | All files (*.*) | *.*";
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                image = new Bitmap(dialog.FileName);
                pictureBox1.Image = image;
                pictureBox1.Refresh();
                Dpicture.Visible= false;
            }
        }
        // сохранение картинки
        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            saveFileDialog1.Title = "Save an Image File";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                
                System.IO.FileStream fs =
                    (System.IO.FileStream)saveFileDialog1.OpenFile();

                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        this.pictureBox1.Image.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;

                    case 2:
                        this.pictureBox1.Image.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Bmp);
                        break;

                    case 3:
                        this.pictureBox1.Image.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                }

                fs.Close();
            }
        }
        // Загрузка картинки через центр экрана
        private void Dpicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files | *.png; *.jpg; *.bmp | All files (*.*) | *.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                image = new Bitmap(dialog.FileName);
                pictureBox1.Image = image;
                pictureBox1.Refresh();
                Dpicture.Visible = false;
            }
        }


        //
        //  BACKGROUNDWORKER    BACKGROUNDWORKER    BACKGROUNDWORKER    BACKGROUNDWORKER    BACKGROUNDWORKER     
        //

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Bitmap newImage = ((Filters)e.Argument).processImage(image, backgroundWorker1);
            if (backgroundWorker1.CancellationPending != true)
            {
                history.Push(image);
                image = newImage;
            }
            else { nextFilter = null; nextFilter2 = null; return; }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }
        private Filters nextFilter = null; // Очередь фильтров
        private Filters nextFilter2 = null;
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                currentImage = image; // Обновляем currentImage
                pictureBox1.Image = currentImage;
                pictureBox1.Refresh();
            }

            if (nextFilter != null)
            {
                Filters filterToRun = nextFilter;
                nextFilter = null;
                backgroundWorker1.RunWorkerAsync(filterToRun);
            }
            else if (nextFilter2 != null)
            {
                Filters filterToRun = nextFilter2;
                nextFilter2 = null;
                backgroundWorker1.RunWorkerAsync(filterToRun);
            }
            else
            {
                progressBar1.Value = 0;
            }
        }
        private void Stop_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
            //progressBar1.Value = 0;
        }
        private void отменаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImageBack();
            
        }








        //
        //  FILTERS     FILTERS     FILTERS     FILTERS     FILTERS     FILTERS
        //

        private void размытиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter=new BlurFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void размытиеПоГауссуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new GaussianFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void черноБелыйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new GrayScaleFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void сепияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new SepiaFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void яркостьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new BrightFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void собельToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new SobelFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void резкостьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new RezkostFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void яркостьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Filters filter = new DarkFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void тиснениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new TissFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void тиснениецветнойToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new TissColorfulFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void медианныйToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new MedianFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void максимумToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new MaxFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void переносToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new PerenosFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void поворотToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new RotateFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void волныВертикальныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new VWaveFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void волныГоризонтальныеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new HWaveFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void стеклоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new GlassFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void размытиеВДвиженииToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new MotionBlurFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void резкостьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Filters filter = new Rezkost2Filter();
            backgroundWorker1.RunWorkerAsync(filter);
        }
        private void инверсияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new InvertFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void светящиесяКраяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters medianFilter = new MedianFilter();
            Filters maxFilter = new MaxFilter();
            Filters SobelFilter = new SobelFilter();

            if (backgroundWorker1.IsBusy)
            {
                MessageBox.Show("Подождите, пока текущий фильтр завершится.");
                return;
            }

            nextFilter = maxFilter; // Запоминаем следующий фильтр
            backgroundWorker1.RunWorkerAsync(medianFilter);
            nextFilter2 = SobelFilter;
        }

        private void щарраToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new SharraFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void прюиттаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new PriutFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void минимумToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new MinFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }

        private void шахматыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Filters filter = new ChessFilter();
            backgroundWorker1.RunWorkerAsync(filter);
        }
    }
}
