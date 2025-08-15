using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Infrastructure.HtmlExport.Tests
{
    [TestClass]
    public class ExportFoldersContextTests
    {
        [TestMethod]
        public void InitializeTest()
        {
            string root = "/root";
            string subfoler = "folder";
            string group = "3123";
            string photo = "/root/photos";
            var context = new ExportFoldersContext(root, subfoler, group, photo);
            Assert.AreEqual(root, context.RootFolder);
            Assert.AreEqual(subfoler, context.Subfolder);
            Assert.AreEqual(group, context.GroupFolder);
            Assert.AreEqual(photo, context.PhotoFolder);
        }
    }
}
