using Core.Seascape.BLL.Extensions;
using System;
using System.Collections.Generic;

namespace Core.Seascape.BLL.Models
{
    public class SeascapeModel
    {
        public SeascapeModel(int width, int height, SeascapeOptions options = null)
        {
            ImageWidth = width;
            ImageHeight = height;
            ImageConstant1 = 511;

            wB = wB.Resize(width + 1, height + 1);
            wX = wX.Resize(width, height);
            wY = wY.Resize(width, height);
            col = col.Resize(width, height);
            nZ = nZ.Resize(ImageConstant1 + 1, ImageConstant1 + 1);

            Options = options ?? new SeascapeOptions()
            {
                SunSize = 100.0F,
                CloudCover = 200000.0F,
                Hue = 600.0F,
                K1L = (height / 2) - 1,
                K1R = (height / 2),
                YDivisor = (height / 2),
                KXL = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(((width / 10) * 9.0F) / 10)) * 10),
                KXR = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(((height / 10) * 9.0F) / 10)) * 10),
                K1C = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(((width / 2) + 0.5F) / 5)) * 5),
                K1F = Convert.ToInt32(Math.Floor(Convert.ToDouble(((height / 2) - 0.5F) / 5)) * 5)
            };

            rnd = new Random();
        }

        public readonly float[,] nZ;
        public readonly float[,] wB;
        public readonly float[,] wX;
        public readonly float[,] wY;
        public readonly int[,] col;
        public readonly int[,] cc = new int[129, 9];
        public readonly List<byte> ByteList = new List<byte>();
        public readonly int ImageWidth;
        public readonly int ImageHeight;
        public readonly int ImageConstant1 = 511;
        public readonly Random rnd;

        public int FC { get; private set; }
        public float SX { get; private set; }
        public float SY { get; private set; }

        public SeascapeOptions Options { get; set; }

        public void Set_FC(int value) { FC = value; }
        public void Set_SX(float value) { SX = value; }
        public void Set_SY(float value) { SY = value; }
    }
}
