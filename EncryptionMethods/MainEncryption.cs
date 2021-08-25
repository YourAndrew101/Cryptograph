using System;

namespace EncryptionMethods
{
    public abstract class MainEncryption
    {
        protected static readonly char[][] alphabets = new char[][]
        {
            new char[] { '.', 'A', 'a', 'B', 'b', 'C', 'c', 'D', 'd', 'E', 'e', 'F', 'f', 'G', 'g', 'H', 'h', 'I', 'i', 'J', 'j', 'K', 'k', 'L', 'l', 'M', 'm', 'N', 'n', 'O', 'o', 'P', 'p', 'Q', 'q', 'R', 'r', 'S', 's', 'T', 't', 'U', 'u', 'V', 'v', 'W', 'w', 'X', 'x', 'Y', 'y', 'Z', 'z', '/', ',', ' ', '?', '!', '@', '"', '[', ']', '{', '}', '(', ')', '#', '№', '$', '%', '^', ';', ':', '&', '*', '+', '-', '_', '|', '~', '«', '»', '▪', '▴', '▸', '►', '▾', '◂', '•', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '\n' },
            new char[] { '.', 'А', 'а', 'Б', 'б', 'В', 'в', 'Г', 'г', 'Ґ', 'ґ', 'Д', 'д', 'Е', 'е', 'Ё', 'ё', 'Є', 'є', 'Ж', 'ж', 'З', 'з', 'И', 'и', 'І', 'і', 'Ї', 'ї', 'Й', 'й', 'К', 'к', 'Л', 'л', 'М', 'м', 'Н', 'н', 'О', 'о', 'П', 'п', 'Р', 'р', 'С', 'с', 'Т', 'т', 'У', 'у', 'Ф', 'ф', 'Х', 'х', 'Ц', 'ц', 'Ч', 'ч', 'Ш', 'ш', 'Щ', 'щ', 'Ъ', 'ъ', 'Ы', 'ы', 'Ь', 'ь', 'Э', 'э', 'Ю', 'ю', 'Я', 'я', '/', ',', ' ', '?', '!', '@', '"', '[', ']', '{', '}', '(', ')', '#', '$', '%', ';', ':', '&', '*', '+', '-', '_', '|', '~', '«', '»', '▪', '▴', '▸', '►', '▾', '◂', '•', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '\n' },
        };

        private string _stringIn;
        public virtual string StringIn { protected get => _stringIn; set => _stringIn = value.Trim(); }
        public string StringOut { get; protected set; }

        public enum Languages
        {
            Eng,
            Rus
        }
        public Languages Language { get; protected set; }

        public char[] Alphabet { get; protected set; }

        protected void GetLanguage()
        {
            for (int i = 0; i < StringIn.Length; i++)
            {
                if (char.IsLetter(StringIn[i]) && Array.IndexOf(alphabets[(int)Languages.Eng], StringIn[i]) != -1)
                {
                    Language = Languages.Eng;
                    return;
                }
                if (char.IsLetter(StringIn[i]) && Array.IndexOf(alphabets[(int)Languages.Rus], StringIn[i]) != -1)
                {
                    Language = Languages.Rus;
                    return;
                }
            }
        }
        protected void GetAlphabet() => Alphabet = alphabets[(int)Language];

        public abstract void Crypto();
        public abstract void Decrypto();

        protected MainEncryption(string string_in)
        {
            StringIn = string_in;

            GetLanguage();
            GetAlphabet();
        }
        protected MainEncryption() { }
    }
}
