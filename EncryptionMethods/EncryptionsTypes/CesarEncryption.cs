using System;
using System.Text;
using UsefulMethods;

namespace EncryptionMethods
{
    public class CesarEncryption : MainEncryption
    {
        public string Key { get; private set; }
        public override void Crypto()
        {    
            int key = KeyMethods.Decrypto_Key(Key, Alphabet); 

            RotEncryption rotEncryption = new RotEncryption(StringIn, key);
            rotEncryption.Crypto();
            StringOut = rotEncryption.StringOut;
        }
        public override void Decrypto()
        {
            StringBuilder sb = new StringBuilder(StringIn.Length);

            int key = KeyMethods.Decrypto_Key(Key, Alphabet);
            if (key > Alphabet.Length) key %= Alphabet.Length;

            foreach (char item in StringIn)
            {   int letterIndex = Array.IndexOf(Alphabet, item);
                if ((letterIndex - key) < 0) letterIndex = Alphabet.Length + letterIndex;

                sb.Append(Alphabet[letterIndex - key]);
            }

            StringOut = sb.ToString();
        }
        
        public CesarEncryption(string string_in) : base(string_in) {}
        public CesarEncryption(string string_in, string key) : base(string_in) => Key = key.Trim();
    }
}
