using System.IO;
using System.Text;

namespace Algorithms.Core
{
    public static class FileStreamExtensions
    {
        // adapted from https://stackoverflow.com/a/50819828/4764487
        public static string ReadLastLine(this FileStream fileStream)
        {
            if (fileStream.Length == 0)
            {
                return null;
            }

            fileStream.Position = fileStream.Length - 1;

            while (fileStream.Position > 0)
            {
                fileStream.Position--;
                if (fileStream.ReadByte() == '\n')
                {
                    break;
                }
                fileStream.Position--;
            }

            var lastLineBytes = new BinaryReader(fileStream).ReadBytes((int)(fileStream.Length - fileStream.Position));
            return Encoding.UTF8.GetString(lastLineBytes);
        }
    }
}
