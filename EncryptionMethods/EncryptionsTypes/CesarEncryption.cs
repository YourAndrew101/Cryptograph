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
            int key = KeyMethods.DecryptoKey(Key, Alphabet); 

            RotEncryption rotEncryption = new RotEncryption(StringIn, key);
            rotEncryption.Crypto();
            StringOut = rotEncryption.StringOut;
        }
        public override void Decrypto()
        {
            int key = KeyMethods.DecryptoKey(Key, Alphabet);
            if (key > Alphabet.Length) key %= Alphabet.Length;

            RotEncryption rotEncryption = new RotEncryption(StringIn, key);
            rotEncryption.Decrypto();
            StringOut = rotEncryption.StringOut;
        }
        
        public CesarEncryption(string string_in) : base(string_in) {}
        public CesarEncryption(string string_in, string key) : base(string_in) => Key = key.Trim();
    }
}
