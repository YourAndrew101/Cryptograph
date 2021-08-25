using ConsoleIO;
using System;
using System.Text;
using System.IO;
using FileIOControllers;

namespace Managers
{
    public static class TextManager
    {
        public static bool Load { get; private set; }

        public static string GetNewString(string prompt)
        {
            Console.Clear();
            string str = Entering.EnterString(prompt);
            str = CheckForPath(str);

            return str;
        }
        public static string GetChangedString(string prompt, string string_in)
        {
            StringBuilder sb = new StringBuilder(string_in);

            while (true)
            {
                Console.Clear();
                Console.Write("{0} {1}", prompt, sb.ToString());

                ConsoleKeyInfo cki = Console.ReadKey();

                if (cki.Key == ConsoleKey.Enter) break;
                if (cki.Key == ConsoleKey.Backspace)
                {
                    if (sb.Length == 0) continue;
                    sb.Remove(sb.Length - 1, 1);
                    continue;
                }

                sb.Append(cki.KeyChar);
            }

            string str = sb.ToString();
            str = CheckForPath(str);

            return str;
        }

        private static string CheckForPath(string str)
        {
            if (!File.Exists(str.Trim('\"')))
            {
                Load = false;
                return str;
            }

            Load = true;
            return str;
        }
    }
}
