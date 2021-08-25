using System.Text;

namespace EncryptionMethods
{
    public class TranspositionEncryption : MainEncryption
    {
        public override string StringIn { protected get; set; }

        public override void Crypto()
        {
            StringBuilder sb = new StringBuilder(StringIn + " ");

            for (int i = 0; i < StringIn.Length - 1; i += 2)
            {
                sb[i] = StringIn[i + 1];
                sb[i + 1] = StringIn[i];
            }

            StringOut = sb.ToString();
        }
        public override void Decrypto()
        {
            StringBuilder sb = new StringBuilder(StringIn);           

            for (int i = 0; i < StringIn.Length - 1; i += 2)
            {
                sb[i + 1] = StringIn[i];
                sb[i] = StringIn[i + 1];
            }

            StringOut = sb.ToString();
        }
        
        public TranspositionEncryption(string string_in) : base(string_in) {}
    }
}
