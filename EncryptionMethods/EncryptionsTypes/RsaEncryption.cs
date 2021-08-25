using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UsefulMethods;

namespace EncryptionMethods
{
    public class RsaEncryption : MainEncryption
    {
        private const int _segmentLength = 4;

        public override string StringIn { protected get; set; }

        private ulong PublicKey { get; set; }
        private ulong GeneralKey { get; set; }
        private ulong PrivateKey { get; set; }

        private void SwapKeys()
        {
            if (PublicKey > GeneralKey)
            {
                ulong temp = PublicKey;
                PublicKey = GeneralKey;
                GeneralKey = temp;
            }
            if (PrivateKey > GeneralKey)
            {
                ulong temp = PrivateKey;
                PrivateKey = GeneralKey;
                GeneralKey = temp;
            }
        }

        public override void Crypto()
        {
            int segmentsCount = Environment.ProcessorCount < StringIn.Length ? Environment.ProcessorCount : StringIn.Length;
            string[] stringsIn = StringIn.SplitStringForSegmentsCount(segmentsCount);
            string[] stringsOut = new string[stringsIn.Length];

            Task[] tasks = new Task[stringsIn.Length];

            for (int i = 0; i < stringsIn.Length; i++)
            {
                int currentIndex = i;
                tasks[i] = Task.Factory.StartNew(() => ProcessCrypto(stringsIn[currentIndex], out stringsOut[currentIndex]));
            }
            Task.WaitAll(tasks);

            StringBuilder sb = new StringBuilder();
            foreach (string str in stringsOut) sb.Append(str);
            StringOut = sb.ToString();
        }
        private void ProcessCrypto(string stringIn, out string stringOut)
        {
            StringBuilder sb = new StringBuilder(StringIn.Length / 2);

            foreach (char item in stringIn)
            {
                ulong newLetter = 1;

                int letterIndex = Array.IndexOf(Alphabet, item);
                for (ulong j = 0; j < PublicKey; j++) newLetter = ((ulong)letterIndex * newLetter) % GeneralKey;

                sb.Append(NumberLetterToString(newLetter.ToString()));
            }

            stringOut = sb.ToString();
        }
        private string NumberLetterToString(string letterNumber)
        {
            StringBuilder stringBuilder = new StringBuilder(letterNumber.Length);

            letterNumber = letterNumber.PadLeft(8, '0');
            for (int i = 0; i < letterNumber.Length; i += 2)
            {
                string letter = letterNumber[i].ToString();
                letter += letterNumber[i + 1].ToString();

                stringBuilder.Append(Alphabet[int.Parse(letter)]);
            }

            return stringBuilder.ToString();
        }

        public override void Decrypto()
        {
            StringIn = StringIn.PadRight(4);

            int segmentsCount = Environment.ProcessorCount < (StringIn.Length / _segmentLength) ? Environment.ProcessorCount : (StringIn.Length / _segmentLength);
            string[][] stringsIn = ArrayMethods.SplitArrayForSegment<string>(StringIn.SplitStringForSegmentsLength(_segmentLength), segmentsCount);
            stringsIn = stringsIn.DistributeItems2dArray<string>(segmentsCount);

            string[] stringsOut = new string[segmentsCount];
            Task[] tasks = new Task[stringsOut.Length];

            for (int i = 0; i < stringsIn.Length; i++)
            {
                int currentIndex = i;
                tasks[i] = Task.Run(() => ProcessDecrypto(stringsIn[currentIndex], out stringsOut[currentIndex]));
            }
            Task.WaitAll(tasks);

            StringBuilder sb = new StringBuilder();
            foreach (string str in stringsOut) sb.Append(str);
            StringOut = sb.ToString();
        }
        private void ProcessDecrypto(string[] stringsIn, out string stringOut)
        {
            StringBuilder sb = new StringBuilder(StringIn.Length / 2);

            foreach (string item in stringsIn)
            {
                ulong newLetter = 1;

                for (ulong j = 0; j < PrivateKey; j++)
                {
                    newLetter *= StringLetterToNumber(item);
                    newLetter %= GeneralKey;
                }

                newLetter %= (ulong)Alphabet.Length;
                sb.Append(Alphabet[newLetter]);
            }

            stringOut = sb.ToString();
        }
        private ulong StringLetterToNumber(string letterString)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (char letter in letterString)
            {
                int indexLetter = Array.IndexOf(Alphabet, letter);
                stringBuilder.Append(indexLetter.ToString().PadLeft(2, '0'));
            }

            return ulong.Parse(stringBuilder.ToString());
        }

        public RsaEncryption() : base() { }
        public RsaEncryption(string string_in) : base(string_in) { }
        public RsaEncryption(string string_in, ulong generalKey, ulong privateKey) : base(string_in)
        {
            GeneralKey = generalKey;
            PrivateKey = privateKey;
            SwapKeys();
        }
        public RsaEncryption(string string_in, ulong publicKey, ulong generalKey, ulong privateKey) : base(string_in)
        {
            PublicKey = publicKey;
            GeneralKey = generalKey;
            PrivateKey = privateKey;

            SwapKeys();
        }
    }
}