using Bntu.Library.CardGenerator.Infrastructure.HtmlExport.Tests.Barcodes;
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

            string text = new BarcodeTestReader().Read(fileName);

            Assert.AreEqual(value, text);
        }
    }
}
