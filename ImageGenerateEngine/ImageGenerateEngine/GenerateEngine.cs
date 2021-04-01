using System;
using System.Drawing;
using System.IO;

namespace ImageGenerateEngine
{
	public class GenerateEngine
	{
		public static void GeneratePNG(string imagePath)
		{
			try
			{
				for (int i = 0; i < 360; i = i + Config.Default.RotationAngleIncrement)
				{
					Bitmap prototypeImg = new Bitmap(imagePath);
					prototypeImg.SetResolution(prototypeImg.HorizontalResolution, prototypeImg.VerticalResolution);
					using (Graphics g = Graphics.FromImage(prototypeImg))
					{
						// Set the rotation point to the center in the matrix
						g.TranslateTransform(prototypeImg.Width / 2, prototypeImg.Height / 2);

						// Rotate
						g.RotateTransform(i);

						// Restore rotation point in the matrix
						g.TranslateTransform(-prototypeImg.Width / 2, -prototypeImg.Height / 2);

						// Draw the image on the bitmap
						g.DrawImage(prototypeImg, new Point(0, 0));

						g.Dispose();

					}

					//write image
					if (i < 2)
					{
						prototypeImg.Save(imagePath.Replace(Config.Default.Root, Config.Default.GenerateRoot).Replace(".png", "") + "\\" + i + "_degree", System.Drawing.Imaging.ImageFormat.Png);
					}
					else
					{
						prototypeImg.Save(imagePath.Replace(Config.Default.Root, Config.Default.GenerateRoot).Replace(".png", "") + "\\" + i + "_degrees");
					}
					prototypeImg.Dispose();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}

		public static void CreateDirectory(string path)
		{
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
		}
	}
}
