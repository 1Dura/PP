using System;
using System.Drawing;
using System.Text;

public static class ImageToAsciiConverter
{
    // Расширенный набор символов (более детализированный)
    private static readonly char[] asciiChars = { ' ', '.', '-', ':', '+', '*', '?', '%', '$', '#', '@' };

    public static string ConvertToAscii(Bitmap image, int gridHeight = 60, int gridWidth = 140)
    {
        int cellWidth = image.Width / gridWidth;
        int cellHeight = image.Height / gridHeight;

        StringBuilder sb = new StringBuilder();

        for (int y = 0; y < gridHeight; y++)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                int brightnessSum = 0;
                int pixelCount = 0;

                for (int yy = y * cellHeight; yy < (y + 1) * cellHeight && yy < image.Height; yy++)
                {
                    for (int xx = x * cellWidth; xx < (x + 1) * cellWidth && xx < image.Width; xx++)
                    {
                        Color pixel = image.GetPixel(xx, yy);
                        int brightness = (pixel.R + pixel.G + pixel.B) / 3;

                        // Увеличение контраста
                        brightness = Math.Max(0, Math.Min(255, (int)((brightness - 128) * 1.2 + 128)));

                        brightnessSum += brightness;
                        pixelCount++;
                    }
                }

                int averageBrightness = brightnessSum / pixelCount;
                int charIndex = averageBrightness * (asciiChars.Length - 1) / 255;
                sb.Append(asciiChars[charIndex]);
            }
            sb.AppendLine();
        }

        return sb.ToString();
    }
}
