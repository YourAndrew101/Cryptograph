using System;
using System.Text;

namespace EncryptionMethods
{
    public class RotEncryption : MainEncryption
    {
        readonly private int _shift;

        public static int ROT1_SHIFT { get; } = 2;
        public static int ROT13_SHIFT { get; } = 26;

        public override void Crypto()
        {
            StringBuilder sb = new StringBuilder(StringIn.Length);

            foreach (char item in StringIn)
            {
                int letterIndex = Array.IndexOf(Alphabet, item);
                sb.Append(Alphabet[(letterIndex + _shift) % Alphabet.Length]);
            }

            StringOut = sb.ToString();
        }
        public override void Decrypto()
        {
            StringBuilder sb = new StringBuilder(StringIn.Length);  

            foreach (char item in StringIn)
            {
                int letterIndex = Array.IndexOf(Alphabet, item);
                if ((letterIndex - _shift) < 0) letterIndex = Alphabet.Length + letterIndex;

                sb.Append(Alphabet[letterIndex - _shift]);
            }

            StringOut = sb.ToString();
        }

        public RotEncryption(string string_in, int shift) : base(string_in)
        {
            _shift = shift;
        }

    }
}
