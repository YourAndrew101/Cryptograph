using System.IO;
using System.Text;

namespace FileIOControllers
{
    public static class TxtFileController
    {
        public const string FileExtension = ".txt";

        public static void Save(string path, string text)
        {
            if (File.Exists(path)) File.Delete(path);

            try
            {
                File.AppendAllText(path, text, Encoding.Unicode);
            }
            catch(DirectoryNotFoundException)
            {
                DirectoryInfo dirInfo = new DirectoryInfo(path.Substring(0, path.LastIndexOf('\\')));
                dirInfo.Create();
                File.AppendAllText(path, text, Encoding.Unicode);
            }
        }

        public static string Load(string path)
        {
            if (!File.Exists(path)) throw new FileNotFoundException();

            string str;
            using (StreamReader sr = new StreamReader(path)) str = sr.ReadToEnd();

            return str;
        }
    }
}
