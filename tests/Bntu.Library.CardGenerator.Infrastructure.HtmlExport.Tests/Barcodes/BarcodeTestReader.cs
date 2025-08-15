using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing.Common;
using ZXing;
using SixLabors.ImageSharp;

namespace Bntu.Library.CardGenerator.Infrastructure.HtmlExport.Tests.Barcodes
{
    public class BarcodeTestReader
    {
        public string Read(string fileName)
        {
            using var image = Image.Load<Rgba32>(fileName);
            var luminanceSource = new ImageSharpLuminanceSource(image);
            var binaryBitmap = new BinaryBitmap(new HybridBinarizer(luminanceSource));

            var reader = new BarcodeReaderGeneric();
            var result = reader.Decode(luminanceSource);
            return result.Text;
        }
    }
}
