using System;
using System.Text;

namespace EncryptionMethods
{
    public class VigenerEncryption : MainEncryption
    {
        public string Key { get; private set; }

        public override void Crypto()
        {
            StringBuilder sb = new StringBuilder(StringIn.Length);

            int keyword_index = 0;
            foreach (char c in StringIn)
            {
                int letterString = Array.IndexOf(Alphabet, c);
                int letterIndex = Array.IndexOf(Alphabet, Key[keyword_index]);
                int finalLetter = (letterString + letterIndex) % Alphabet.Length;

                sb.Append(Alphabet[finalLetter]);
            
                if ((keyword_index + 1) == Key.Length) keyword_index = 0;
                keyword_index++;
            }

            StringOut = sb.ToString();
        }
        public override void Decrypto()
        {
            StringBuilder sb = new StringBuilder(StringIn.Length);

            int keyword_index = 0;
            foreach (char c in StringIn)
            {
                int letterString = Array.IndexOf(Alphabet, c);
                int letterIndex = Array.IndexOf(Alphabet, Key[keyword_index]);
                int finalLetter = letterString - letterIndex;

                if (finalLetter < 0) finalLetter += Alphabet.Length;
                sb.Append(Alphabet[finalLetter]);

                if ((keyword_index + 1) == Key.Length) keyword_index = 0;
                keyword_index++;
            }

            StringOut = sb.ToString();
        }

        public VigenerEncryption(string string_in) : base(string_in) { }
        public VigenerEncryption(string string_in, string key) : base(string_in) => Key = key.Trim();
    }
}
