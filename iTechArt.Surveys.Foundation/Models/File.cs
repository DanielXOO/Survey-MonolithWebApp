using System.IO;

using FileInfo = iTechArt.Surveys.DomainModel.FileInfo;

namespace iTechArt.Surveys.Foundation.Models
{
    public class File
    {
        public Stream Data { get; set; }

        public FileInfo Info { get; set; }
    }
}