using System;
using System.Numerics;
using System.Text;
using System.Threading;

namespace UsefulMethods
{
    public static class KeyMethods
    {
        private static Random rand = new Random();

        public static int DecryptoKey(string keyString, char[] alphabet)
        {
            int key = 0;

            for (int i = 0; i < keyString.Length; i++)
            {
                int letterIndex = Array.IndexOf(alphabet, keyString[i]);
                key += letterIndex;
            }

            return key == alphabet.Length ? key : ++key;
        }
        public static string KeyGenerate(char[] alphabet, int keyLength)
        {
            StringBuilder sb = new StringBuilder(keyLength);

            for (int i = 0; i < keyLength; i++) sb.Append(alphabet[rand.Next(0, alphabet.Length)].ToString());

            return sb.ToString();
        }

        public static void CalculateKeys(out ulong generalKey, out ulong publicKey, out ulong privateKey)
        {
            int keyLength = 3;
            int min = (int)Math.Pow(10, keyLength - 1);
            int max = (int)Math.Pow(10, keyLength);

            int number1 = MathMethods.PrimeNumberGenerate(min, max, rand);
            min /= 10; max /= 10;
            int number2 = MathMethods.PrimeNumberGenerate(min, max, rand);

            ulong eilerNumber = (ulong)((number1 - 1) * (number2 - 1));

            generalKey = GetGeneralKey(number1, number2);

            publicKey = GetPublicKey(min, max, rand);
            privateKey = GetPrivateKey(publicKey, eilerNumber);

        }

        private static ulong GetGeneralKey(int primeNumber1, int prineNumber2) => (ulong)primeNumber1 * (ulong)prineNumber2;
        private static ulong GetPrivateKey(ulong publicKey, ulong eilerNumber)
        {
            ulong privateKey, k = 1;

            while (true)
            {
                k += eilerNumber;

                if (k % publicKey == 0)
                {
                    privateKey = (k / publicKey);
                    return privateKey;
                }
            }
        }
        private static ulong GetPublicKey(int min, int max, Random rand)
        {
            min *= 100; max *= 100;
            return (ulong)MathMethods.PrimeNumberGenerate(min, max, rand);
        }

        
        public static string KeyNumberToString(ulong key)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char digitChar in key.ToString())
            {
                int digitInt = int.Parse(digitChar.ToString());
                int firstNumber = rand.Next(digitInt + 1, 100);
                string secondNumber = (firstNumber - digitInt).ToString().PadLeft(2, '0');

                sb.Append(firstNumber.ToString().PadLeft(2, '0') + secondNumber);
            }

            return sb.ToString();
        }
        public static ulong KeyStringToNumber(string keyString)
        {
            keyString = BigInteger.Parse(keyString).ToString();
            StringBuilder sb = new StringBuilder();
            ulong keyNumber;

            for (int i = 0; i < keyString.Length; i += 4)
            {
                int firstNumber = int.Parse(keyString[i].ToString() + keyString[i + 1].ToString());
                int secondNumber = int.Parse(keyString[i + 2].ToString() + keyString[i + 3].ToString());

                sb.Append((firstNumber - secondNumber).ToString());
            }

            keyNumber = ulong.Parse(sb.ToString());
            if (keyNumber < 0) throw new ArithmeticException();

            return ulong.Parse(sb.ToString());
        }
    }
}