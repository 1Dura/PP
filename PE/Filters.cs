using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Markup;
using System.ComponentModel;
using System.Security.Policy;
using System.Windows.Forms;


namespace _2_lab_computer_graphics
{
    public abstract class Filters
    {
        public int Clamp(int value, int min, int max)
        {
            if (value < min)
                return min;
            if (value > max)
                return max;
            return value;
        }
        protected abstract Color calculateNewPixelColor(Bitmap sourceImage, int x, int y);

        public Bitmap processImage(Bitmap sourceImage, BackgroundWorker worker)
        {
            if(sourceImage == null) MessageBox.Show("Сначала загрузите картинку! ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Bitmap resultImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            for (int i = 0; i < sourceImage.Width; i++)
            {
                worker.ReportProgress((int)((float)i / resultImage.Width * 100));
                if (worker.CancellationPending)
                    return null;
                for (int j = 0; j < sourceImage.Height; j++)
                {
                    resultImage.SetPixel(i, j, calculateNewPixelColor(sourceImage, i, j));
                }
            }
            return resultImage;
        }
    }
    class InvertFilter : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            Color resultColor = Color.FromArgb(255 - sourceColor.R,
                255 - sourceColor.G,
                255 - sourceColor.B);
            return resultColor;
        }
    }
    class GrayScaleFilter : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            double Intensity = 0.36 * sourceColor.R + 0.53 * sourceColor.G + 0.11 * sourceColor.B;
            Color resultColor = Color.FromArgb((int)Intensity, (int)Intensity, (int)Intensity);
            return resultColor;
        }
    }
    class SepiaFilter : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            
            Color sourceColor = sourceImage.GetPixel(x, y);
            int k = 10;
            double Intensity = 0.36 * sourceColor.R + 0.53 * sourceColor.G + 0.11 * sourceColor.B;
            Color resultColor = Color.FromArgb(Clamp((int)Intensity + 2 * k, 0, 255),
                Clamp((int)(Intensity + 0.5 * k), 0, 255),
                Clamp((int)Intensity - 1 * k, 0, 255));
            return resultColor;
        }
    }
    class BrightFilter : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            int k = 30;

            Color resultColor = Color.FromArgb(Clamp(sourceColor.R + k, 0, 255),
                Clamp(sourceColor.G + k, 0, 255),
                Clamp(sourceColor.B + k, 0, 255));
            return resultColor;
        }
    }
    class DarkFilter : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            Color sourceColor = sourceImage.GetPixel(x, y);
            int k = -30;

            Color resultColor = Color.FromArgb(Clamp(sourceColor.R + k, 0, 255),
                Clamp(sourceColor.G + k, 0, 255),
                Clamp(sourceColor.B + k, 0, 255));
            return resultColor;
        }
    }


    public class MatrixFilter : Filters
    {
        protected float[,] kernel = null;
        protected MatrixFilter() { }
        public MatrixFilter(float[,] kernel)
        {
            this.kernel = kernel;
        }

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int radiusX = kernel.GetLength(0) / 2;
            int radiusY = kernel.GetLength(1) / 2;
            float resultR = 0;
            float resultG = 0;
            float resultB = 0;
            for (int k = -radiusX; k <= radiusX; k++)
                for (int l = -radiusY; l <= radiusY; l++)
                {
                    int idX = Clamp(x + k, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + l, 0, sourceImage.Height - 1);
                    Color neighborColor = sourceImage.GetPixel(idX, idY);
                    resultR += neighborColor.R * kernel[k + radiusX, l + radiusY];
                    resultG += neighborColor.G * kernel[k + radiusX, l + radiusY];
                    resultB += neighborColor.B * kernel[k + radiusX, l + radiusY];
                }
            return Color.FromArgb(
                Clamp((int)resultR, 0, 255),
                Clamp((int)resultG, 0, 255),
                Clamp((int)resultB, 0, 255));
        }
    }


    class BlurFilter : MatrixFilter
    {
        public BlurFilter()
        {
            int sizeX = 3;
            int sizeY = 3;
            kernel = new float[sizeX, sizeY];
            for (int i = 0; i < sizeX; i++)
                for (int j = 0; j < sizeY; j++)
                    kernel[i, j] = 1.0f / (float)(sizeX * sizeY);
        }
    }


    class GaussianFilter : MatrixFilter
    {
        public void createGaussianKernel(int radius, float sigma)
        {
            int size = 2 * radius + 1;
            kernel = new float[size, size];
            float norm = 0;
            for (int i = -radius; i <= radius; i++)
                for (int j = -radius; j <= radius; j++)
                {
                    kernel[i + radius, j + radius] = (float)(Math.Exp(-(i * i + j * j) / (sigma * sigma)));
                    norm += kernel[i + radius, j + radius];
                }
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    kernel[i, j] /= norm;
                }
        }
        public GaussianFilter()
        {
            createGaussianKernel(3, 2);
        }
    }


    class SobelFilter : Filters
    {
        protected int[,] operatorX = { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
        protected int[,] operatorY = { { -1, -2, -1 }, { 0, 0, 0 }, { 1, 2, 1 } };

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int radiusX = operatorX.GetLength(0) / 2;
            int radiusY = operatorY.GetLength(1) / 2;

            float resultRX = 0;
            float resultGX = 0;
            float resultBX = 0;
            float resultRY = 0;
            float resultGY = 0;
            float resultBY = 0;

            for (int l = -radiusY; l <= radiusY; l++)
                for (int k = -radiusX; k <= radiusX; k++)
                {
                    int idX = Clamp(x + k, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + l, 0, sourceImage.Height - 1);
                    Color neighborColor = sourceImage.GetPixel(idX, idY);

                    resultRX += neighborColor.R * operatorX[k + radiusX, l + radiusY];
                    resultGX += neighborColor.G * operatorX[k + radiusX, l + radiusY];
                    resultBX += neighborColor.B * operatorX[k + radiusX, l + radiusY];

                    resultRY += neighborColor.R * operatorY[k + radiusX, l + radiusY];
                    resultGY += neighborColor.G * operatorY[k + radiusX, l + radiusY];
                    resultBY += neighborColor.B * operatorY[k + radiusX, l + radiusY];
                }
            int resultR = (int)Math.Sqrt(resultRX * resultRX + resultRY * resultRY);
            int resultG = (int)Math.Sqrt(resultGX * resultGX + resultGY * resultGY);
            int resultB = (int)Math.Sqrt(resultBX * resultBX + resultBY * resultBY);

            return Color.FromArgb(Clamp(resultR, 0, 255), Clamp(resultG, 0, 255), Clamp(resultB, 0, 255));
        }
    }


    class RezkostFilter : MatrixFilter
    {
        public RezkostFilter()
        {
            kernel = new float[3, 3];
            kernel[0, 0] = 0; kernel[0, 1] = -1; kernel[0, 2] = 0;
            kernel[1, 0] = -1; kernel[1, 1] = 5; kernel[1, 2] = -1;
            kernel[2, 0] = 0; kernel[2, 1] = -1; kernel[2, 2] = 0;

        }
    }
    class Rezkost2Filter : MatrixFilter
    {
        public Rezkost2Filter()
        {
            kernel = new float[3, 3];
            kernel[0, 0] = -1; kernel[0, 1] = -1; kernel[0, 2] = -1;
            kernel[1, 0] = -1; kernel[1, 1] = 9; kernel[1, 2] = -1;
            kernel[2, 0] = -1; kernel[2, 1] = -1; kernel[2, 2] = -1;

        }
    }


    // тиснение
    class TissFilter : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int[,] kernel = { { 0, 1, 0 }, { 1, 0, -1 }, { 0, -1, 0 } };

            int resultR = 0, resultG = 0, resultB = 0, norm = 0;

            for (int ky = -1; ky <= 1; ky++)
            {
                for (int kx = -1; kx <= 1; kx++)
                {
                    int idX = Clamp(x + kx, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + ky, 0, sourceImage.Height - 1);
                    Color pixel = sourceImage.GetPixel(idX, idY);

                    int kernelValue = kernel[ky + 1, kx + 1];
                    norm += Math.Abs(kernelValue);

                    resultR += pixel.R * kernelValue;
                    resultG += pixel.G * kernelValue;
                    resultB += pixel.B * kernelValue;
                }
            }

            if (norm != 0)
            {
                resultR /= norm;
                resultG /= norm;
                resultB /= norm;
            }

            resultR = Clamp(resultR + 100, 0, 255);
            resultG = Clamp(resultG + 100, 0, 255);
            resultB = Clamp(resultB + 100, 0, 255);

            return Color.FromArgb(resultR, resultG, resultB);
        }
    }
    class TissColorfulFilter : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int[,] kernel = { { 0, 1, 0 }, { 1, 0, -1 }, { 0, -1, 0 } };

            int resultR = 0, resultG = 0, resultB = 0;
            for (int ky = -1; ky <= 1; ky++)
            {
                for (int kx = -1; kx <= 1; kx++)
                {
                    int idX = Clamp(x + kx, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + ky, 0, sourceImage.Height - 1);
                    Color pixel = sourceImage.GetPixel(idX, idY);

                    resultR += pixel.R * kernel[ky + 1, kx + 1];
                    resultG += pixel.G * kernel[ky + 1, kx + 1];
                    resultB += pixel.B * kernel[ky + 1, kx + 1];
                }
            }
            resultR = Clamp(resultR + 100, 0, 255);
            resultG = Clamp(resultG + 100, 0, 255);
            resultB = Clamp(resultB + 100, 0, 255);

            return Color.FromArgb(resultR, resultG, resultB);
        }
    }
    class MedianFilter : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            List<int> rValues = new List<int>();
            List<int> gValues = new List<int>();
            List<int> bValues = new List<int>();
            //int sR=0, sG=0, sB=0, s=0;

            for (int dx = -3; dx <= 3; dx++)
            {
                for (int dy = -3; dy <= 3; dy++)
                {
                    int idX = Clamp(x + dx, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + dy, 0, sourceImage.Height - 1);

                    Color neighborColor = sourceImage.GetPixel(idX, idY);
                    rValues.Add(neighborColor.R);
                    gValues.Add(neighborColor.G);
                    bValues.Add(neighborColor.B);
                    //sR=sR+neighborColor.R;
                    //sG=sG+neighborColor.G;
                    //sB=sB+neighborColor.B;
                    //s++;
                }
            }
            //sR = Clamp(sR / s, 0,255);
            //sG = Clamp(sG / s, 0,255);
            //sB = Clamp(sB / s, 0, 255);
            rValues.Sort();
            gValues.Sort();
            bValues.Sort();

            int rMedian = rValues[rValues.Count / 2];
            int gMedian = gValues[gValues.Count / 2];
            int bMedian = bValues[bValues.Count / 2];

            //return Color.FromArgb(sR, sG, sB);
            return Color.FromArgb(rMedian, gMedian, bMedian);
        }
    }

    class MaxFilter : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int maxR = 0, maxG = 0, maxB = 0;
            for (int kx = -1; kx <= 1; kx++)
                for (int ky = -1; ky <= 1; ky++)
                {
                    int idX = Clamp(x + kx, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + ky, 0, sourceImage.Height - 1);

                    Color neighborColor = sourceImage.GetPixel(idX, idY);
                    int R = neighborColor.R;
                    int G = neighborColor.G;
                    int B = neighborColor.B;
                    if (R > maxR) maxR = R;
                    if (G > maxG) maxG = G;
                    if (B > maxB) maxB = B;
                }
            return Color.FromArgb(maxR, maxG, maxB);
        }
    }

    class MinFilter : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int maxR = 250, maxG = 250, maxB = 250;
            for (int kx = -1; kx <= 1; kx++)
                for (int ky = -1; ky <= 1; ky++)
                {
                    int idX = Clamp(x + kx, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + ky, 0, sourceImage.Height - 1);

                    Color neighborColor = sourceImage.GetPixel(idX, idY);
                    int R = neighborColor.R;
                    int G = neighborColor.G;
                    int B = neighborColor.B;
                    if (R < maxR) maxR = R;
                    if (G < maxG) maxG = G;
                    if (B < maxB) maxB = B;
                }
            return Color.FromArgb(maxR, maxG, maxB);
        }
    }

    class PerenosFilter : Filters 
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            if (x + 50 > sourceImage.Width - 1) { return Color.FromArgb(0, 0, 0); }
            else
            {
                Color pixel = sourceImage.GetPixel(Clamp(x + 50, 0, sourceImage.Width - 1), y);
                return Color.FromArgb(pixel.R, pixel.G, pixel.B);
            }
        }

    }
    class RotateFilter : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int u = 45;
            int resR, resG, resB;
            int xO, yO;
            xO = sourceImage.Width / 2; yO = sourceImage.Height / 2;
            double X = (x - xO) * Math.Cos(u) - (y - yO) * Math.Sin(u) + xO;
            double Y = (x - xO) * Math.Sin(u) + (y - yO) * Math.Cos(u) + yO;
            if (X > sourceImage.Width - 1 || Y > sourceImage.Height - 1 || X<0 || Y<0) { return Color.FromArgb(0, 0, 0); }
            int idX = Clamp((int)X, 0, sourceImage.Width - 1);
            int idY = Clamp((int)Y, 0, sourceImage.Height - 1);
            Color P = sourceImage.GetPixel(idX, idY);
            resR = P.R;
            resG = P.G;
            resB = P.B;
            return Color.FromArgb(resR, resG, resB);

        }
    }
    class VWaveFilter : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int resR, resG, resB;
            double X = x + 20 * Math.Sin((2 * Math.PI * y) / 60);
            //double Y = x + 20 * Math.Sin((2 * Math.PI * x) / 30);
            int idX = Clamp((int)X, 0, sourceImage.Width - 1);
            int idY = Clamp(y, 0, sourceImage.Height - 1);
            Color P = sourceImage.GetPixel(idX, idY);
            resR = P.R;
            resG = P.G;
            resB = P.B;
            return Color.FromArgb(resR, resG, resB);

        }
    }
    class HWaveFilter : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int resR, resG, resB;
            //double X = x + 20 * Math.Sin((2 * Math.PI * y) / 30);
            double Y = y + 20 * Math.Sin((2 * Math.PI * x) / 30);
            //double Y = y + 20 * Math.Sin((2 * Math.PI * x) / 30);
            int idX = Clamp(x, 0, sourceImage.Width - 1);
            int idY = Clamp((int)Y, 0, sourceImage.Height - 1);
            Color P = sourceImage.GetPixel(idX, idY);
            resR = P.R;
            resG = P.G;
            resB = P.B;
            return Color.FromArgb(resR, resG, resB);

        }
    }
    class GlassFilter : Filters
    {
        Random rnd = new Random();
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {

            double r1 = rnd.NextDouble();
            double r2 = rnd.NextDouble();
            double X = x + (r1 - 0.5) * 10;
            double Y = y + (r2 - 0.5) * 10;
            
            int idX = Clamp((int)X, 0, sourceImage.Width - 1);
            int idY = Clamp((int)Y, 0, sourceImage.Height - 1);
            Color P = sourceImage.GetPixel(idX, idY);
           
            return Color.FromArgb(P.R, P.G, P.B);

        }
    }
    class MotionBlurFilter : MatrixFilter
    {
        public MotionBlurFilter(int size=5)
        {
            kernel = new float[size, size];

            // Заполняем диагональ единицами
            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                {
                    if (i == j) kernel[i, j] = 1.0f;
                    else kernel[i, j] = 0.0f;
                    kernel[i, j] /= size;
                }
        }
    }
    class SharraFilter : Filters
    {
        protected int[,] operatorX = { { 3, 0, -3 }, { 10, 0, -10 }, { 3, 0, -3 } };
        protected int[,] operatorY = { { 3, 10, 3 }, { 0, 0, 0 }, { -3, 10, -3 } };

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int radiusX = operatorX.GetLength(0) / 2;
            int radiusY = operatorY.GetLength(1) / 2;

            float resultRX = 0;
            float resultGX = 0;
            float resultBX = 0;
            float resultRY = 0;
            float resultGY = 0;
            float resultBY = 0;

            for (int l = -radiusY; l <= radiusY; l++)
                for (int k = -radiusX; k <= radiusX; k++)
                {
                    int idX = Clamp(x + k, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + l, 0, sourceImage.Height - 1);
                    Color neighborColor = sourceImage.GetPixel(idX, idY);

                    resultRX += neighborColor.R * operatorX[k + radiusX, l + radiusY];
                    resultGX += neighborColor.G * operatorX[k + radiusX, l + radiusY];
                    resultBX += neighborColor.B * operatorX[k + radiusX, l + radiusY];

                    resultRY += neighborColor.R * operatorY[k + radiusX, l + radiusY];
                    resultGY += neighborColor.G * operatorY[k + radiusX, l + radiusY];
                    resultBY += neighborColor.B * operatorY[k + radiusX, l + radiusY];
                }
            int resultR = (int)Math.Sqrt(resultRX * resultRX + resultRY * resultRY);
            int resultG = (int)Math.Sqrt(resultGX * resultGX + resultGY * resultGY);
            int resultB = (int)Math.Sqrt(resultBX * resultBX + resultBY * resultBY);

            return Color.FromArgb(Clamp(resultR, 0, 255), Clamp(resultG, 0, 255), Clamp(resultB, 0, 255));
        }
    }
    class PriutFilter : Filters
    {
        protected int[,] operatorX = { { -1, -1, -1 }, { 0, 0, 0 }, { 1, 1, 1 } };
        protected int[,] operatorY = { { -1, 0, 1 }, { -1, 0, 1 }, { -1, 0, 1 } };

        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int radiusX = operatorX.GetLength(0) / 2;
            int radiusY = operatorY.GetLength(1) / 2;

            float resultRX = 0;
            float resultGX = 0;
            float resultBX = 0;
            float resultRY = 0;
            float resultGY = 0;
            float resultBY = 0;

            for (int l = -radiusY; l <= radiusY; l++)
                for (int k = -radiusX; k <= radiusX; k++)
                {
                    int idX = Clamp(x + k, 0, sourceImage.Width - 1);
                    int idY = Clamp(y + l, 0, sourceImage.Height - 1);
                    Color neighborColor = sourceImage.GetPixel(idX, idY);

                    resultRX += neighborColor.R * operatorX[k + radiusX, l + radiusY];
                    resultGX += neighborColor.G * operatorX[k + radiusX, l + radiusY];
                    resultBX += neighborColor.B * operatorX[k + radiusX, l + radiusY];

                    resultRY += neighborColor.R * operatorY[k + radiusX, l + radiusY];
                    resultGY += neighborColor.G * operatorY[k + radiusX, l + radiusY];
                    resultBY += neighborColor.B * operatorY[k + radiusX, l + radiusY];
                }
            int resultR = (int)Math.Sqrt(resultRX * resultRX + resultRY * resultRY);
            int resultG = (int)Math.Sqrt(resultGX * resultGX + resultGY * resultGY);
            int resultB = (int)Math.Sqrt(resultBX * resultBX + resultBY * resultBY);

            return Color.FromArgb(Clamp(resultR, 0, 255), Clamp(resultG, 0, 255), Clamp(resultB, 0, 255));
        }
    }
    class ChessFilter : Filters
    {
        protected override Color calculateNewPixelColor(Bitmap sourceImage, int x, int y)
        {
            int dx = sourceImage.Width / 8;
            int dy = sourceImage.Height / 8;
            int fx = (x / dx)%2;
            int fy = (y / dy)%2;
            if (fx==0 && fy==1 || fx==1 && fy==0) {
                Color sourceColor = sourceImage.GetPixel(x, y);
                int k = 10;
                double Intensity = 0.36 * sourceColor.R + 0.53 * sourceColor.G + 0.11 * sourceColor.B;
                Color resultColor = Color.FromArgb(Clamp((int)Intensity + 2 * k, 0, 255),
                    Clamp((int)(Intensity + 0.5 * k), 0, 255),
                    Clamp((int)Intensity - 1 * k, 0, 255));
                return resultColor;
            }
            else
            {
                Color sourceColor = sourceImage.GetPixel(x, y);
                Color resultColor = Color.FromArgb(255 - sourceColor.R,
                    255 - sourceColor.G,
                    255 - sourceColor.B);
                return resultColor;
            }
        }
    }
}

