using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using ZXing.Common;

namespace Bntu.Library.CardGenerator.Infrastructure.HtmlExport
{
    public class BarcodeGenerator
    {
        private BarcodeWriterPixelData writer;

        public BarcodeGenerator()
        {
            writer = new BarcodeWriterPixelData
            {
                Format = BarcodeFormat.CODE_128,
                Options = new EncodingOptions
                {
                    Width = 300,
                    Height = 100,
                    Margin = 10
                }
            };
        }

        public void Generate(string value, string filePath)
        {
            var pixelData = writer.Write(value);

            using (var image = new Image<Rgba32>(pixelData.Width, pixelData.Height))
            {
                for (int y = 0; y < pixelData.Height; y++)
                {
                    for (int x = 0; x < pixelData.Width; x++)
                    {
                        int i = (y * pixelData.Width + x) * 4;
                        byte r = pixelData.Pixels[i + 0];
                        byte g = pixelData.Pixels[i + 1];
                        byte b = pixelData.Pixels[i + 2];
                        byte a = pixelData.Pixels[i + 3];

                        image[x, y] = new Rgba32(r, g, b, a);
                    }
                }

                image.Save(filePath, new PngEncoder());
            }
        }
    }
}
