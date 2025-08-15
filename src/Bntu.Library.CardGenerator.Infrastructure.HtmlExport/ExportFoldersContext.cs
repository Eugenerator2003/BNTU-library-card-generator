using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bntu.Library.CardGenerator.Infrastructure.HtmlExport
{
    public class ExportFoldersContext
    {
        public string RootFolder { get; }
        public string? Subfolder { get; }
        public string GroupFolder { get; }

        public string PhotoFolder { get; }

        public ExportFoldersContext(string rootFolder, string? subfolder, string groupFolder, string photoFolder)
        {
            RootFolder = rootFolder;
            Subfolder = subfolder;
            GroupFolder = groupFolder;
            PhotoFolder = photoFolder;
        }
    }
}
