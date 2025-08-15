using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;

namespace Bntu.Library.CardGenerator.Infrastructure.HtmlExport.Tests.Barcodes
{
    internal class ImageSharpLuminanceSource : LuminanceSource
    {
        private readonly byte[] luminances;

        public ImageSharpLuminanceSource(Image<Rgba32> image)
            : base(image.Width, image.Height)
        {
            luminances = new byte[image.Width * image.Height];

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Rgba32 pixel = image[x, y];
                    luminances[y * image.Width + x] = (byte)((pixel.R * 299 + pixel.G * 587 + pixel.B * 114) / 1000);
                }
            }
        }

        public override byte[] Matrix => luminances;

        public override byte[] getRow(int y, byte[] row)
        {
            if (row == null || row.Length < Width)
                row = new byte[Width];

            Array.Copy(luminances, y * Width, row, 0, Width);
            return row;
        }

        public override bool CropSupported => false;
        public override bool RotateSupported => false;

        public override LuminanceSource crop(int left, int top, int width, int height)
        {
            throw new NotSupportedException();
        }

        public override LuminanceSource rotateCounterClockwise()
        {
            throw new NotSupportedException();
        }
    }
}
