using Core.Seascape.BLL.Models;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace Core.Seascape.BLL.Services
{
    public class SeascapeService : ISeascapeService
    {

        #region " Public Methods "

        /// <summary>
        /// Generates a random seascape image and returns the base64 data for the full size and lower half crop images.
        /// </summary>
        /// <param name="width">Specify an image width.</param>
        /// <param name="height">Specify an image height.</param>
        /// <returns>A full size and a lower half crop image in base64 string format.</returns>
        public SeascapeImageData GenerateBase64Data(int width, int height)
        {
            return GenerateBase64Data(new SeascapeModel(width, height));
        }

        /// <summary>
        /// Generates a random seascape image and returns the base64 data for the full size and lower half crop images.
        /// </summary>
        /// <param name="model">A <see cref="SeascapeModel"/> containing image parameters.</param>
        /// <param name="retryOnError">If <see langword="true"/> a second pass to generate an image with default parameters is made if an error is caught during the first pass.</param>
        /// <returns>A full size and a lower half crop image in base64 string format.</returns>
        public SeascapeImageData GenerateBase64Data(SeascapeModel model, bool retryOnError = true)
        {
            try
            {
                Gen(model);
                Sky(model);
                FCC(model);
                Water(model);
                Air(model);

                using var bmp = new Bitmap(model.ImageWidth, model.ImageHeight, PixelFormat.Format32bppRgb);
                var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                var bmpData = bmp.LockBits(rect, ImageLockMode.ReadWrite, bmp.PixelFormat);
                var ptr = bmpData.Scan0;
                var ba = model.ByteList.ToArray();
                Marshal.Copy(ba, 0, ptr, ba.Length - 4);
                bmp.UnlockBits(bmpData);
                model.ByteList.Clear();

                var stream = new MemoryStream();
                bmp.Save(stream, ImageFormat.Png);
                var imageBytes = stream.ToArray();
                var imageData = Convert.ToBase64String(stream.ToArray());

                var cropRect = new Rectangle(0, (model.ImageHeight / 2), model.ImageWidth, (model.ImageHeight / 2));
                using var bmpCrop = new Bitmap(cropRect.Width, cropRect.Height);
                using var g = Graphics.FromImage(bmpCrop);
                g.DrawImage(bmp, new Rectangle(0, 0, bmpCrop.Width, bmpCrop.Height), cropRect, GraphicsUnit.Pixel);

                stream = new MemoryStream();
                bmp.Save(stream, ImageFormat.Png);
                imageBytes = stream.ToArray();
                var cropData = Convert.ToBase64String(stream.ToArray());

                return new SeascapeImageData
                {
                    FullSizeData = imageData,
                    CropData = cropData
                };
            }
            catch
            {
                if (retryOnError)
                {
                    model.Options = new SeascapeOptions();
                    return GenerateBase64Data(model, false);
                }
            }

            return null;
        }

        #endregion " Public Methods "

        #region " Private Methods "

        private void Gen(SeascapeModel model)
        {
            int d = 128;

            do
            {
                d /= 2;

                for (int y = 0; y < model.ImageConstant1; y += d + d)
                {
                    for (int x = 0; x < model.ImageConstant1; x += d + d)
                    {
                        model.nZ[(x + d) & model.ImageConstant1, y] = (model.nZ[x, y] + model.nZ[(x + d + d) & model.ImageConstant1, y]) * 0.5F + Convert.ToSingle(d * (model.rnd.NextDouble() - 0.5));
                        model.nZ[x, (y + d) & model.ImageConstant1] = (model.nZ[x, y] + model.nZ[x, (y + d + d) & model.ImageConstant1]) * 0.5F + Convert.ToSingle(d * (model.rnd.NextDouble() - 0.5));
                        model.nZ[(x + d) & model.ImageConstant1, (y + d) & model.ImageConstant1] = (model.nZ[x, y] + model.nZ[(x + d + d) & model.ImageConstant1, (y + d + d) & model.ImageConstant1] + model.nZ[x, (y + d + d) & model.ImageConstant1] + model.nZ[(x + d + d) & model.ImageConstant1, y]) * 0.25F + Convert.ToSingle(d * (model.rnd.NextDouble() - 0.5));
                    }
                }
            } while (d > 1);
        }

        private void Sky(SeascapeModel model)
        {
            int c1;
            int c2;
            float k;
            float s;
            float sx1;
            float sy1;

            model.Set_SX(50.0F + Convert.ToSingle(model.rnd.NextDouble()) * (model.ImageWidth - model.Options.SunSize - 50));
            model.Set_SY(50.0F + Convert.ToSingle(model.rnd.NextDouble()) * ((model.ImageHeight / 2) - model.Options.SunSize - 50));

            for (int y = 0; y <= model.Options.K1R; y++)
            {
                sy1 = Convert.ToSingle(model.Options.CloudCover / (double)Convert.ToSingle(model.Options.K1C - y));
                for (int x = 0; x <= model.ImageWidth - 1; x++)
                {
                    sx1 = (Convert.ToSingle(x) - (model.ImageConstant1 + 0.5F)) * sy1 * 0.0005F;
                    k = BN(model, sx1, sy1) - BN(model, sx1 * 0.14F + sy1 * 0.21F, sy1 * 0.14F - sx1 * 0.21F);
                    if (k < -8.0F)
                    {
                        k = 0.0F;
                    }
                    else
                    {
                        k = (k + 8.0F) * 0.02F;
                        if (k > 1.0F)
                        {
                            k = 1.0F;
                        }
                    }
                    model.Set_FC(model.Options.TroposphereColour + Convert.ToInt32((model.SY + model.Options.Hue) * 0.2F));
                    c1 = Lerp(model.FC + 25, model.Options.StratosphereColour, Convert.ToSingle(y / (double)model.Options.K1R));
                    c2 = Lerp(model.Options.TroposphereCloudColour, model.Options.StratosphereCloudColour, Convert.ToSingle(y / (double)model.Options.YDivisor));
                    s = Convert.ToSingle(model.Options.SunSize / (double)Convert.ToSingle(Math.Sqrt((x - model.SX) * (x - model.SX) + (y - model.SY) * (y - model.SY))));
                    if (s > 1.0F)
                    {
                        s = 1.0F;
                    }
                    c1 = Lerp(model.Options.SunColour, c1, s);
                    model.col[x, y] = Lerp(c2, c1, k);
                }
            }
        }

        private void FCC(SeascapeModel model)
        {
            int r;
            int g;
            int b;

            for (int x = 0; x < 128; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    r = 0;
                    g = 0;
                    b = 0;
                    for (int yy = 0; yy < 48; yy++)
                    {
                        for (int xx = 0; xx < 8; xx++)
                        {
                            r += model.col[xx + x * 8, yy + y * 48] & 255;
                            g += model.col[xx + x * 8, yy + y * 48] & 0xFF00;
                            b += model.col[xx + x * 8, yy + y * 48] & 0xFF0000 / 256;
                        }
                    }
                    model.cc[x, y] = r / model.Options.K1R + ((g / model.Options.K1R) & 0xFF00) + ((b / model.Options.K1R) & 0xFF00) * 256;
                }
                model.cc[x, 8] = model.cc[x, 7];
            }
        }

        private void Water(SeascapeModel model)
        {
            int x1;
            int y1;
            int c;
            float k;
            float kx;
            float sx1;
            float sy1;
            float sx2;
            float sy2;

            for (int y = model.ImageHeight; y >= (model.ImageHeight / 2); y += -1)
            {
                k = Convert.ToSingle((y - ((model.ImageHeight / 2) - 1))) * 0.5F;
                kx = Convert.ToSingle((model.Options.KXL - y) / (double)model.Options.KXR);
                if (Math.Abs(kx) > 100)
                    kx = 100 * Math.Sign(kx);
                for (int x = model.ImageWidth; x >= 0; x += -1)
                {
                    sy1 = Convert.ToSingle(64000 / (double)(y - model.Options.K1F));
                    sx1 = (Convert.ToSingle(x) - (model.ImageConstant1 + 0.5F)) * sy1 * 0.002F;
                    sy2 = sy1 * 0.34F - sx1 * 0.71F;
                    sx2 = sx1 * 0.34F + sy1 * 0.71F;
                    sy1 = sy2 * 0.34F - sx2 * 0.21F;
                    sx1 = sx2 * 0.34F + sy2 * 0.21F;
                    model.wB[x, y] = BN(model, sx1, sy1) - BN(model, sx2, sy2);
                    if (x < model.ImageWidth & y < model.ImageHeight)
                    {
                        model.wX[x, y] = (model.wB[x + 1, y] - model.wB[x, y]) * k * kx;
                        model.wY[x, y] = (model.wB[x, y + 1] - model.wB[x, y]) * k;
                        x1 = Convert.ToInt32(x + model.wX[x, y]);
                        if (x1 < 0)
                        {
                            x1 = -x1;
                        }
                        else if (x1 > model.ImageWidth - 1)
                        {
                            x1 = ((model.ImageWidth * 2) - 1) - x1;
                        }
                        y1 = Convert.ToInt32(model.ImageHeight - y + model.wY[x, y]);
                        if (y1 < 0)
                        {
                            y1 = 0;
                        }
                        else if (y1 > model.Options.K1R - 1)
                        {
                            y1 = model.Options.K1R - 1;
                        }
                        c = Lerp(BC(model, Convert.ToSingle(x1 / (double)8), Convert.ToSingle(y1 / (double)model.Options.WaterRipple)), model.Options.WaterColour, kx);
                        model.col[x, y] = c;
                    }
                }
            }
        }

        private void Air(SeascapeModel model)
        {
            int c;
            float k1;
            float k2;
            float s;
            float k1LmyDk1R;

            for (int y = 0; y <= model.ImageHeight - 1; y++)
            {
                if (Math.Abs(1 - Math.Abs(model.Options.K1L - y) / (double)model.Options.K1R) > 2.5)
                {
                    k1LmyDk1R = Convert.ToSingle(2.5 * Math.Sign(1 - Math.Abs(model.Options.K1L - y) / (double)model.Options.K1R));
                }
                else
                {
                    k1LmyDk1R = Convert.ToSingle(1 - Math.Abs(model.Options.K1L - y) / (double)model.Options.K1R);
                }
                k1 = Convert.ToSingle(Math.Pow(k1LmyDk1R, 5));
                for (int x = 0; x <= model.ImageWidth - 1; x++)
                {
                    if (y == model.SY)
                    {
                        k2 = 0.25F;
                    }
                    else
                    {
                        k2 = Convert.ToSingle(Math.Atan((x - model.SX) / (double)(y - model.SY)) / 6.283186 + 0.25);
                    }
                    if ((y - model.SY) < 0)
                    {
                        k2 += 0.5F;
                    }
                    k2 = BN(model, k2 * 512.0F, 0.0F) * 0.03F;
                    k2 = 0.2F - k2 * k2;
                    if (k2 < 0.0F)
                    {
                        k2 = 0.0F;
                    }
                    s = Convert.ToSingle(50.0F / (double)Convert.ToSingle(Math.Sqrt((x - model.SX) * (x - model.SX) + (y - model.SY) * (y - model.SY))));
                    if (s > 1.0F)
                    {
                        s = 1.0F;
                    }
                    c = Lerp(model.Options.SunRaysColour, model.FC, k2 * (1 - s));
                    model.ByteList.AddRange(BitConverter.GetBytes(Lerp(c, model.col[x, y], k1)));
                }
            }
        }

        private int BC(SeascapeModel model, float x, float y)
        {
            int ix = Convert.ToInt32(x);
            int iy = Convert.ToInt32(y);
            float SX1 = x - ix;
            float SY1 = y - iy;
            int c0 = model.cc[ix & 127, iy % 9];
            int c1 = model.cc[(ix + 1) & 127, iy % 9];
            int c2 = model.cc[ix & 127, (iy + 1) % 9];
            int c3 = model.cc[(ix + 1) & 127, (iy + 1) % 9];

            return Convert.ToInt32((c0 & 255) * (1.0F - SX1) * (1.0F - SY1) + (c1 & 255) * SX1 * (1.0F - SY1) + (c2 & 255) * (1.0F - SX1) * SY1 + (c3 & 255) * SX1 * SY1)
                + (Convert.ToInt32((c0 & 0xFF00) * (1.0F - SX1) * (1.0F - SY1) + (c1 & 0xFF00) * SX1 * (1.0F - SY1) + (c2 & 0xFF00) * (1.0F - SX1) * SY1 + (c3 & 0xFF00) * SX1 * SY1) & 0xFF00)
                + (Convert.ToInt32((c0 & 0xFF0000) * (1.0F - SX1) * (1.0F - SY1) + (c1 & 0xFF0000) * SX1 * (1.0F - SY1) + (c2 & 0xFF0000) * (1.0F - SX1) * SY1 + (c3 & 0xFF0000) * SX1 * SY1) & 0xFF0000);
        }

        private float BN(SeascapeModel model, float x, float y)
        {
            int ix = Convert.ToInt32(Math.Floor(x));
            int iy = Convert.ToInt32(Math.Floor(y));
            float SX1 = x - ix;
            float SY1 = y - iy;

            return model.nZ[ix & model.ImageConstant1, iy & model.ImageConstant1] * (1.0F - SX1) * (1.0F - SY1)
                + model.nZ[(ix + 1) & model.ImageConstant1, iy & model.ImageConstant1] * SX1 * (1.0F - SY1)
                + model.nZ[ix & model.ImageConstant1, (iy + 1) & model.ImageConstant1] * (1.0F - SX1) * SY1
                + model.nZ[(ix + 1) & model.ImageConstant1, (iy + 1) & model.ImageConstant1] * SX1 * SY1;
        }

        private int Lerp(int c1, int c2, float k)
        {
            return Convert.ToInt32((c1 & 255) * k + (c2 & 255) * (1.0F - k))
                | (Convert.ToInt32((c1 & 0xFF00) * k + (c2 & 0xFF00) * (1.0F - k)) & 0xFF00)
                | (Convert.ToInt32((c1 & 0xFF0000) * k + (c2 & 0xFF0000) * (1.0F - k)) & 0xFF0000);
        }

        #endregion " Private Methods "

    }
}
