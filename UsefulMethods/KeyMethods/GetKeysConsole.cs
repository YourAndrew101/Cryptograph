using ConsoleIO;
using System;

namespace UsefulMethods
{
    public static class GetKeysConsole
    {
        public static string GetSimpleKeyForCrypto(char[] alphabet)
        {
            byte getKeyWay = (byte)Entering.EnterInt("\n1. Згенерувати ключ випадково.\n2. Ввести ключ самостiйно.\n", 1, 2,
                "\nНЕВІРНИЙ ПАРАМЕТР\nВиберіть одну з поданих операцій:\n");
            Console.WriteLine();

            switch (getKeyWay)
            {
                case 1:
                    int keyLength = Entering.EnterInt("Довжина ключа: ", 1, int.MaxValue, "\nНЕВІРНИЙ ПАРАМЕТР\nПотрібно вказати цілу додатню довжину ключа\n");
                    string key = KeyMethods.KeyGenerate(alphabet, keyLength);

                    Console.WriteLine("Bаш ключ: {0}", key);
                    return key;
                case 2:
                    return Entering.EnterString("Введiть ваш ключ: ");
                default:
                    return "";
            }

        }
        public static string GetSimpleKeyForDecrypto()
        {
            string keyString = Entering.EnterString("\nВведiть ваш ключ: ");
            Console.WriteLine();

            return keyString;
        }

        public static (ulong, ulong, ulong) GetPairKeysForCrypto()
        {
            const byte ACTS = 2;
            ulong generalKey = 0, publicKey = 0, privateKey = 0;

            byte act = (byte)Entering.EnterInt("\n1. Згенерувати нову пару ключів та зашифрувати дані;\n" +
                "2. Зашифрувати дані з уже відомим публічним ключем;\n", 1, ACTS, "НЕВІРНИЙ ПАРАМЕТР\nВиберіть одну з поданих операцій:");

            if (act == 1)
            {
                KeyMethods.CalculateKeys(out generalKey, out publicKey, out privateKey);

                Console.WriteLine("\nЗагальний ключ: {0}\nПублічний ключ: {1}\nПриватний ключ: {2}",
                    KeyMethods.KeyNumberToString(generalKey), KeyMethods.KeyNumberToString(publicKey), KeyMethods.KeyNumberToString(privateKey));
            }
            if (act == 2)
            {
                while (true)
                {
                    try
                    {
                        generalKey = KeyMethods.KeyStringToNumber(Entering.EnterString("Введіть загальний ключ: "));
                        break;
                    }
                    catch (Exception) { Console.WriteLine("\nНевірний формат ключа\nКлюч повинен бути додатнім числом\n"); }
                }
                while (true)
                {
                    try
                    {
                        publicKey = KeyMethods.KeyStringToNumber(Entering.EnterString("Введіть публічний ключ: "));
                        break;
                    }
                    catch (Exception) { Console.WriteLine("\nНевірний формат ключа\nКлюч повинен бути додатнім числом\n"); }
                }
            }

            return (publicKey, generalKey, privateKey);
        }
        public static (ulong, ulong) GetPairKeysForDecrypto()
        {
            ulong generalKey, privateKey;
            while (true)
            {
                try
                {
                    generalKey = KeyMethods.KeyStringToNumber(Entering.EnterString("Загальний ключ: "));
                    break;
                }
                catch (Exception) { Console.WriteLine("\nНевірний формат ключа\nКлюч повинен бути додатнім числом\n"); }
            }
            while (true)
            {
                try
                {
                    privateKey = KeyMethods.KeyStringToNumber(Entering.EnterString("Приватний ключ: "));
                    break;
                }
                catch (Exception) { Console.WriteLine("\nНевірний формат ключа\nКлюч повинен бути додатнім числом\n"); }
            }

            return (generalKey, privateKey);
        }
    }
}
