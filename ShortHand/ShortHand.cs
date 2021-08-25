using EncryptionMethods;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Text;

namespace ShortHandMethods
{
    public class ShortHand : MainEncryption
    {
        readonly Bitmap _inputImage;

        static readonly List<int> bitMasks = new List<int>(new int[] { BitVector32.CreateMask() });
        private static void CreateBitMasks()
        {
            for (int i = 1; i < 32; i++) bitMasks.Add(BitVector32.CreateMask(bitMasks[i - 1]));
        }

        private int currentXPos = 0;
        private int CurrentXPos
        {
            get => currentXPos;
            set
            {
                currentXPos = value % _inputImage.Width;
                CurrentYPos += value / _inputImage.Width;
            }
        }

        private int CurrentYPos { get; set; }

        public Bitmap OutputImage { get; set; }
        private int _lengthStringOut;

        public override void Crypto()
        {
            CurrentXPos = 0;
            CurrentYPos = 0;

            SetPixelSettings();

            foreach (char letter in StringIn)
            {
                BitVector32 numberBitVector = new BitVector32(Array.IndexOf(Alphabet, letter));
                BitVector32[] colorBitVectors = GetColorBitVectors();

                colorBitVectors[0][bitMasks[2]] = numberBitVector[bitMasks[7]];
                colorBitVectors[0][bitMasks[1]] = numberBitVector[bitMasks[6]];
                colorBitVectors[0][bitMasks[0]] = numberBitVector[bitMasks[5]];

                colorBitVectors[1][bitMasks[1]] = numberBitVector[bitMasks[4]];
                colorBitVectors[1][bitMasks[0]] = numberBitVector[bitMasks[3]];

                colorBitVectors[2][bitMasks[2]] = numberBitVector[bitMasks[2]];
                colorBitVectors[2][bitMasks[1]] = numberBitVector[bitMasks[1]];
                colorBitVectors[2][bitMasks[0]] = numberBitVector[bitMasks[0]];

                SetColorBitVectors(colorBitVectors);

                CurrentXPos++;
            }

            OutputImage = _inputImage;
        }

        private void SetPixelSettings()
        {
            SetPixelAvailabilityText();
            SetPixelLanguage();
            SetPixelTextLength();
        }
        private void SetPixelAvailabilityText()
        {
            for (CurrentXPos = 0; CurrentXPos < 4; CurrentXPos++)
            {
                BitVector32[] colorBitVectors = GetColorBitVectors();
                for (int i = 0; i < colorBitVectors.Length; i++)
                {
                    colorBitVectors[i][bitMasks[1]] = false;
                    colorBitVectors[i][bitMasks[0]] = false;
                }

                SetColorBitVectors(colorBitVectors);
            }
        }
        private void SetPixelLanguage()
        {
            BitVector32[] colorBitVectors = GetColorBitVectors();

            if (Language == Languages.Eng) colorBitVectors[0][bitMasks[0]] = false;
            else colorBitVectors[0][bitMasks[0]] = true;

            SetColorBitVectors(colorBitVectors);
        }
        private void SetPixelTextLength()
        {
            BitVector32 stringLengthBitVector = new BitVector32(StringIn.Length);

            int lengthVectorPos = 31;

            BitVector32[] colorBitVectors = GetColorBitVectors();
            colorBitVectors[2][bitMasks[1]] = stringLengthBitVector[bitMasks[lengthVectorPos--]];
            colorBitVectors[2][bitMasks[0]] = stringLengthBitVector[bitMasks[lengthVectorPos]];
            SetColorBitVectors(colorBitVectors);
            CurrentXPos++;

            for (int i = 0; i < 5; i++)
            {
                colorBitVectors = GetColorBitVectors();

                for (int j = 0; j < 3; j++)
                {
                    colorBitVectors[j][bitMasks[1]] = stringLengthBitVector[bitMasks[--lengthVectorPos]];
                    colorBitVectors[j][bitMasks[0]] = stringLengthBitVector[bitMasks[--lengthVectorPos]];
                }

                SetColorBitVectors(colorBitVectors);
                CurrentXPos++;
            }
        }


        public override void Decrypto()
        {
            CurrentXPos = 0;
            CurrentYPos = 0;

            StringBuilder sb = new StringBuilder(_lengthStringOut);

            GetPixelSettings();

            for (int i = 0; i < _lengthStringOut; i++)
            {
                BitVector32 numberBitVector = new BitVector32();
                BitVector32[] colorBitVectors = GetColorBitVectors();

                numberBitVector[bitMasks[7]] = colorBitVectors[0][bitMasks[2]];
                numberBitVector[bitMasks[6]] = colorBitVectors[0][bitMasks[1]];
                numberBitVector[bitMasks[5]] = colorBitVectors[0][bitMasks[0]];

                numberBitVector[bitMasks[4]] = colorBitVectors[1][bitMasks[1]];
                numberBitVector[bitMasks[3]] = colorBitVectors[1][bitMasks[0]];

                numberBitVector[bitMasks[2]] = colorBitVectors[2][bitMasks[2]];
                numberBitVector[bitMasks[1]] = colorBitVectors[2][bitMasks[1]];
                numberBitVector[bitMasks[0]] = colorBitVectors[2][bitMasks[0]];

                sb.Append(Alphabet[numberBitVector.Data == 255 ? 0 : numberBitVector.Data]);

                CurrentXPos++;
            }

            StringOut = sb.ToString();
        }

        private void GetPixelSettings()
        {
            if (!GetPixeltAvailabilityText()) throw new BadImageFormatException("В данному зображенні не зашифровано текст");
            GetPixelLanguage();
            GetPixelTextLength();
        }
        private bool GetPixeltAvailabilityText()
        {
            for (CurrentXPos = 0; CurrentXPos < 4; CurrentXPos++)
            {
                BitVector32[] colorBitVectors = GetColorBitVectors();
                for (int i = 0; i < colorBitVectors.Length; i++) if (colorBitVectors[i][bitMasks[1]] || colorBitVectors[i][bitMasks[0]]) return false;
            }

            return true;
        }
        private void GetPixelLanguage()
        {
            BitVector32[] colorBitVectors = GetColorBitVectors();

            if (!colorBitVectors[0][bitMasks[0]]) Language = Languages.Eng;
            else Language = Languages.Rus;
            Alphabet = alphabets[(int)Language];
        }
        private void GetPixelTextLength()
        {
            BitVector32 stringLengthBitVector = new BitVector32();
            int lengthVectorPos = 31;

            BitVector32[] colorBitVectors = GetColorBitVectors();
            stringLengthBitVector[bitMasks[lengthVectorPos--]] = colorBitVectors[2][bitMasks[1]];
            stringLengthBitVector[bitMasks[lengthVectorPos]] = colorBitVectors[2][bitMasks[0]];
            CurrentXPos++;

            for (int i = 0; i < 5; i++)
            {
                colorBitVectors = GetColorBitVectors();

                for (int j = 0; j < 3; j++)
                {
                    stringLengthBitVector[bitMasks[--lengthVectorPos]] = colorBitVectors[j][bitMasks[1]];
                    stringLengthBitVector[bitMasks[--lengthVectorPos]] = colorBitVectors[j][bitMasks[0]];
                }

                CurrentXPos++;
            }

            _lengthStringOut = stringLengthBitVector.Data;
        }


        private BitVector32[] GetColorBitVectors()
        {
            Color color = _inputImage.GetPixel(CurrentXPos, CurrentYPos);
            return new BitVector32[] { new BitVector32(color.R), new BitVector32(color.G), new BitVector32(color.B) };
        }
        private void SetColorBitVectors(BitVector32[] colorBitVectors) => _inputImage.SetPixel(CurrentXPos, CurrentYPos, Color.FromArgb(colorBitVectors[0].Data, colorBitVectors[1].Data, colorBitVectors[2].Data));


        public ShortHand(string stringIn, string path) : base(stringIn)
        {
            _inputImage = new Bitmap(path);

            CreateBitMasks();
        }
        public ShortHand(string stringIn, Bitmap image) : base(stringIn)
        {
            _inputImage = image;

            CreateBitMasks();
        }
        public ShortHand(Bitmap image)
        {
            _inputImage = image;

            CreateBitMasks();
        }
        public ShortHand(string path)
        {
            _inputImage = new Bitmap(path);

            CreateBitMasks();
        }
    }
}