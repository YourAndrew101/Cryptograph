using System;
using System.IO;
using System.Text;

namespace ConsoleIO
{
    public static class Settings
    {
        public static void SetConsoleParams()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;

            int bufSize = short.MaxValue;
            Stream inStream = Console.OpenStandardInput(bufSize);
            Console.SetIn(new StreamReader(inStream, Console.InputEncoding, false, bufSize));
        }
    }
}
