using Microsoft.VisualStudio.TestTools.UnitTesting;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using ZXing;
using ZXing.Common;

namespace Bntu.Library.CardGenerator.Infrastructure.HtmlExport.Tests
{
    [TestClass]
    public class BarcodeGeneratorTests
    {
        [TestCleanup]
        public void Cleanup()
        {
            if (File.Exists("file.png"))
            {
                File.Delete("file.png");
            }
        }

        [TestMethod]
        public void GenerateTest()
        {
            string value = "13232131541551";
            string fileName = "file.png";

            var generator = new BarcodeGenerator();
            generator.Generate(value, fileName);

            using var image = Image.Load<Rgba32>(fileName);
            var luminanceSource = new ImageSharpLuminanceSource(image);
            var binaryBitmap = new BinaryBitmap(new HybridBinarizer(luminanceSource));

            var reader = new BarcodeReaderGeneric();
            var result = reader.Decode(luminanceSource);

            Assert.IsNotNull(result, "Штрихкод не распознан.");
            Assert.AreEqual(value, result.Text);
        }
    }
}
