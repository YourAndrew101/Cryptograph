using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

namespace ConsoleIO
{
    public static class Entering
    {
        public static int EnterInt(string prompt, string message = "\nНевірно вказане значення цілого числа\n")
        {
            int value;
            while (true)
            {
                try
                {
                    Console.Write(prompt);
                    value = Convert.ToInt32(Console.ReadLine());
                    return value;
                }
                catch (Exception)
                {
                    Console.WriteLine(message);
                }
            }
        }
        public static int EnterInt(string prompt, int min, int max = int.MaxValue, string message = "\nПотрібно вказати значення цілого число в межах від {0} до {1}\n")
        {
            while (true)
            {
                int value = EnterInt(prompt);

                if (value > max || value < min) Console.WriteLine(message, min, max);
                else return value;
            }
        }


        public static ulong EnterUlong(string prompt, string message = "\nНевірно вказане значення цілого числа\n")
        {
            ulong value;
            while (true)
            {
                try
                {
                    Console.Write(prompt);
                    value = Convert.ToUInt64(Console.ReadLine());
                    return value;
                }
                catch (Exception)
                {
                    Console.WriteLine(message);
                }
            }
        }
        public static ulong EnterUlong(string prompt, ulong min, ulong max = ulong.MaxValue, string message = "\nПотрібно вказати значення цілого число в межах від {0} до {1}\n")
        {
            while (true)
            {
                ulong value = EnterUlong(prompt);

                if (value > max || value < min) Console.WriteLine(message, min, max);
                else return value;
            }
        }

        public static string EnterStringNullable(string prompt)
        {
            string str;
            Console.Write(prompt);
            str = Console.ReadLine();

            if (str == "") return null;

            str = str.Trim();

            return str;
        }
        public static string EnterStringNullable(string prompt, int minLength, int maxLength = int.MaxValue,
            string message = "\nПотрібно ввести стрічку довжиною від {0} до {1} символів\n")
        {
            while (true)
            {
                string str = EnterStringNullable(prompt);
                if (str == null) return null;

                if (str.Length >= minLength && str.Length <= maxLength) return str;

                Console.WriteLine(message, minLength, maxLength);
            }
        }

        public static string EnterString(string prompt, string message = "\nРядок не може бути пустим\n")
        {
            while (true)
            {
                Console.Write(prompt);
                string str = Console.ReadLine();

                if (str == null || str == "")
                {
                    Console.WriteLine(message);
                    continue;
                }

                return str;
            }
        }
    }
}