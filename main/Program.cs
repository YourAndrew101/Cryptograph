using ConsoleIO;
using Managers;

namespace main
{
    static class Program
    {
        static void Main(string[] args)
        {
            Settings.SetConsoleParams();

            while (true)
            {
                string string_in = TextManager.GetNewString("Ваш текст або шлях до файлу: ");
                ActManager actManager = new ActManager(string_in);
                while (!actManager.Run())
                {
                    string_in = TextManager.GetChangedString("Ваш текст або шлях до файлу: ", actManager.StringIn);
                    actManager = new ActManager(string_in);
                    actManager.Run();
                }
            }
        }
    }
}