using ConsoleIO;
using EncryptionMethods;
using Managers;
using ShortHandMethods;
using System.Drawing;

namespace main
{
    static class Program
    {
        static void Main(string[] args)
        {
            Settings.SetConsoleParams();


            AesEncryption aesEncryption;
            aesEncryption = new AesEncryption("I love lesbians", "1234567891011123");
            aesEncryption.Crypto();

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