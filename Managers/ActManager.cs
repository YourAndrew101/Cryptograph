using FileIOControllers;
using Managers.CommandManagers;
using ShortHandMethods;
using System;
using System.Drawing;

namespace Managers
{
    public class ActManager : CommandManager
    {
        private readonly string _stringIn;
        public string StringIn { get => _stringIn; }
        public string StringOut { get; private set; }

        protected override void PrepareScreen()
        {
            Console.Clear();
            Console.WriteLine("{0}Ваш текст: {1}\n\nЯку операцію слід виконати:", TextManager.Load ? "Текст завантажено з файлу\n" : "",
                TextManager.Load ? TxtFileController.Load(_stringIn.Trim('\"')) : _stringIn);
        }
        protected override void IniCommandInfoArray()
        {
            commandInfoArray = new CommandInfo[]{
                new CommandInfo("назад", null),
                new CommandInfo("Зашифрувати;", Crypto),
                new CommandInfo("Розшифрувати;", Decrypto),
                new CommandInfo("Стінографія", ShortHandCrypto)
            };
        }

        private void ShortHandCrypto()
        {
            object actObject;
            if (TextManager.Load) actObject = new Bitmap(_stringIn);
            else actObject = _stringIn;
            ShortHandManager shortHandManager = new ShortHandManager(actObject);

            _doThingAfterCommand = shortHandManager.Run();
        }

        void Crypto()
        {
            CryptoManager cryptoManager = new CryptoManager(TextManager.Load ? TxtFileController.Load(_stringIn.Trim('\"')) : _stringIn);
            _doThingAfterCommand = cryptoManager.Run();
        }
        void Decrypto()
        {
            CryptoManager cryptoManager = new CryptoManager(TextManager.Load ? TxtFileController.Load(_stringIn.Trim('\"')) : _stringIn);
            _doThingAfterCommand = cryptoManager.Run();
        }

        public ActManager(string string_in)
        {
            _stringIn = string_in;
        }
    }
}
