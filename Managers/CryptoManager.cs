﻿using ConsoleIO;
using EncryptionMethods;
using FileIOControllers;
using Managers.CommandManagers;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using UsefulMethods;

namespace Managers
{
    public class CryptoManager : CommandManager
    {
        private readonly string _string_in;
        private readonly string _act;

        public string StringOut { get; private set; }

        protected override void IniCommandInfoArray()
        {
            commandInfoArray = new CommandInfo[]{
                new CommandInfo("назад", null),
                new CommandInfo("ROT1;", Rot1Encryption),
                new CommandInfo("ROT13;", Rot13Encryption),
                new CommandInfo("Шифр Цезаря;", CesarEncryptio),
                new CommandInfo("Транспозицiя;", TranspositionEncryption),
                new CommandInfo("Двійковий код;", Numeral2Encryption),
                new CommandInfo("Вісімковий код;", Numeral8Encryption),
                new CommandInfo("Шістнадцядковий код;", Numeral16Encryption),                
                new CommandInfo("Шифр Віженера;", VigenerEncryption),
                new CommandInfo("RSA шифрування;", RsaEncryption),
            };
        }
        protected override void PrepareScreen()
        {
            Console.Clear();
            Console.WriteLine("{0}Ваш текст: {1}\nВи {2} текст\n\nВиберіть метод шифрування:",
                TextManager.Load ? "Текст завантажено з файлу\n" : "", _string_in, _act == "Crypto" ? "шифруєте" : "розшифровуєте");
        }
        protected override void ThingAfterCommand()
        {
            Console.WriteLine("Зашифрований текст: {0}", StringOut);
            if (ReaquestForContinuation())
            {
                while (true)
                {
                    try
                    {
                        string path = Entering.EnterStringNullable("Місце для завантаження файлу(Enter - директорія проекту): ");
                        if (path == null) path = "";

                        string name = Entering.EnterString("Назва файлу: ");
                        TxtFileController.Save(Path.Combine(path, name + TxtFileController.FileExtension), StringOut);
                        break;
                    }
                    catch (UnauthorizedAccessException) { Console.WriteLine("\nВідмовлено в доступу до цієї директорії\n"); }
                    catch (DirectoryNotFoundException) { Console.WriteLine("\nНеможливо знайти та створити введену директорію\n"); }
                }
            }
        }

        private void Rot1Encryption()
        {
            RotEncryption rotEncryption = new RotEncryption(_string_in, RotEncryption.ROT1_SHIFT);

            if (_act == "Crypto") rotEncryption.Crypto();
            else rotEncryption.Decrypto();

            StringOut = rotEncryption.StringOut;
        }
        private void Rot13Encryption()
        {
            RotEncryption rotEncryption = new RotEncryption(_string_in, RotEncryption.ROT13_SHIFT);

            if (_act == "Crypto") rotEncryption.Crypto();
            else rotEncryption.Decrypto();

            StringOut = rotEncryption.StringOut;
        }
        private void CesarEncryptio()
        {
            CesarEncryption cesarEncryption = new CesarEncryption(_string_in);

            if (_act == "Crypto")
            {
                cesarEncryption = new CesarEncryption(_string_in, GetKeysConsole.GetSimpleKeyForCrypto(cesarEncryption.Alphabet));
                cesarEncryption.Crypto();
            }
            else
            {
                cesarEncryption = new CesarEncryption(_string_in, GetKeysConsole.GetSimpleKeyForDecrypto());
                cesarEncryption.Decrypto();
            }

            StringOut = cesarEncryption.StringOut;
        }
        private void TranspositionEncryption()
        {
            TranspositionEncryption transpositionEncryption = new TranspositionEncryption(_string_in);

            if (_act == "Crypto") transpositionEncryption.Crypto();
            else transpositionEncryption.Decrypto();

            StringOut = transpositionEncryption.StringOut;
        }
        private void Numeral2Encryption()
        {
            NumeralEncryption numeral2Encryption = new NumeralEncryption(_string_in, 2);

            try
            {
                if (_act == "Crypto") numeral2Encryption.Crypto();
                else numeral2Encryption.Decrypto();
            }
            catch (Exception) { Console.WriteLine("Невірна система числення!!"); }

            StringOut = numeral2Encryption.StringOut;
        }
        private void Numeral8Encryption()
        {
            NumeralEncryption numeral8Encryption = new NumeralEncryption(_string_in, 8);

            try
            {
                if (_act == "Crypto") numeral8Encryption.Crypto();
                else numeral8Encryption.Decrypto();
            }
            catch (Exception) { Console.WriteLine("Невірна система числення!!"); }

            StringOut = numeral8Encryption.StringOut;
        }
        private void Numeral16Encryption()
        {
            NumeralEncryption numeral16Encryption = new NumeralEncryption(_string_in, 16);

            try
            {
                if (_act == "Crypto") numeral16Encryption.Crypto();
                else numeral16Encryption.Decrypto();
            }
            catch (Exception) { Console.WriteLine("Невірна система числення!!"); }

            StringOut = numeral16Encryption.StringOut;
        }
        private void RsaEncryption()
        {
            RsaEncryption rsaEncryption;

            if (_act == "Crypto")
            {
                var (publicKey, generalKey, privateKey) = GetKeysConsole.GetPairKeysForCrypto();
                rsaEncryption = new RsaEncryption(_string_in, publicKey, generalKey, privateKey);
                rsaEncryption.Crypto();
            }
            else
            {
                var (generalKey, privateKey) = GetKeysConsole.GetPairKeysForDecrypto();
                rsaEncryption = new RsaEncryption(_string_in, generalKey, privateKey);
                rsaEncryption.Decrypto();
            }

            StringOut = rsaEncryption.StringOut;
        }
        private void VigenerEncryption()
        {
            VigenerEncryption vigenerEncryption = new VigenerEncryption(_string_in);

            if (_act == "Crypto")
            {
                vigenerEncryption = new VigenerEncryption(_string_in, GetKeysConsole.GetSimpleKeyForCrypto(vigenerEncryption.Alphabet));
                vigenerEncryption.Crypto();
            }
            else
            {
                vigenerEncryption = new VigenerEncryption(_string_in, GetKeysConsole.GetSimpleKeyForDecrypto());
                vigenerEncryption.Decrypto();
            }

            StringOut = vigenerEncryption.StringOut;
        }

        public CryptoManager(string string_in, [CallerMemberName] string name = "")
        {
            _string_in = string_in;
            _act = name;
        }
    }
}
