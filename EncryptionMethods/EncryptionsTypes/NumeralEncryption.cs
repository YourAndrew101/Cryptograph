using System;
using System.Text;
using UsefulMethods;

namespace EncryptionMethods
{
    public class NumeralEncryption : MainEncryption
    {
        const string BINENG = "11111110";
        const string OCTENG = "776";
        const string HEXENG = "fd";

        const string BINRUS = "11111111";
        const string OCTRUS = "777";
        const string HEXRUS = "ff";

        readonly private int _numeral;
        private string _languageCode = "";

        private string GetCodeLanguage()
        {
            if (_numeral == 2) return StringIn.Substring(0, BINENG.Length);
            if (_numeral == 8) return StringIn.Substring(0, OCTENG.Length);
            else return StringIn.Substring(0, HEXENG.Length);
        }
        private void SetCodeLanguage(ref StringBuilder sb)
        {
            if (Language == Languages.Eng)
            {
                switch (_numeral)
                {
                    case 2:
                        _languageCode = BINENG;
                        break;
                    case 8:
                        _languageCode = OCTENG;
                        break;
                    case 16:
                        _languageCode = HEXENG;
                        break;
                    default:
                        break;
                }
            }
            if (Language == Languages.Rus)
            {
                switch (_numeral)
                {
                    case 2:
                        _languageCode = BINRUS;
                        break;
                    case 8:
                        _languageCode = OCTRUS;
                        break;
                    case 16:
                        _languageCode = HEXRUS;
                        break;
                    default:
                        break;
                }
            }

            sb.Append(_languageCode);
        }

        private void GetAlphabetLanguageForCode()
        {
            if(_languageCode == BINENG || _languageCode == OCTENG || _languageCode == HEXENG)
            {
                Alphabet = en_alphabet;
                Language = Languages.Eng;
            }
            if (_languageCode == BINRUS || _languageCode == OCTRUS || _languageCode == HEXRUS)
            {
                Alphabet = ru_alphabet;
                Language = Languages.Rus;
            }
        }
        

        public override void Crypto()
        {
            StringBuilder sb = new StringBuilder(StringIn.Length * _languageCode.Length);
            SetCodeLanguage(ref sb);

            foreach (char item in StringIn)
            {
                string numeralLetter = Convert.ToString(Array.IndexOf(Alphabet, item), _numeral);
                numeralLetter = numeralLetter.PadLeft(_languageCode.Length, '0');

                sb.Append(numeralLetter);
            }

            StringOut = sb.ToString();
        }
        public override void Decrypto()
        {
            _languageCode = GetCodeLanguage();
            GetAlphabetLanguageForCode();
            StringIn = StringIn.Remove(0, _languageCode.Length);

            foreach (char item in StringIn) Convert.ToInt32(item.ToString(), _numeral);

            StringBuilder sb = new StringBuilder(StringIn.Length / _languageCode.Length);

            string[] numeralChars = StringIn.SplitStringForSegmentsLength(_languageCode.Length);
            foreach (string item in numeralChars) sb.Append(Alphabet[Convert.ToInt32(item, _numeral)]);

            StringOut = sb.ToString();
        }

        public NumeralEncryption(string string_in, int numeral) : base(string_in) => _numeral = numeral;
    }
}
