using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UsefulMethods;

namespace EncryptionMethods
{
    public class AesEncryption : MainEncryption
    {
        private readonly Encoding stringsEncoding = Encoding.UTF8;


        private readonly byte[][] _rCon = new byte[10][]
        {
            new byte[]{ 0x01, 0x00, 0x00, 0x00},
            new byte[]{ 0x02, 0x00, 0x00, 0x00},
            new byte[]{ 0x04, 0x00, 0x00, 0x00},
            new byte[]{ 0x08, 0x00, 0x00, 0x00},
            new byte[]{ 0x10, 0x00, 0x00, 0x00},
            new byte[]{ 0x20, 0x00, 0x00, 0x00},
            new byte[]{ 0x40, 0x00, 0x00, 0x00},
            new byte[]{ 0x80, 0x00, 0x00, 0x00},
            new byte[]{ 0x1b, 0x00, 0x00, 0x00},
            new byte[]{ 0x36, 0x00, 0x00, 0x00},
        };
        private readonly byte[,] _sBox = new byte[,]
        {
            { 0x63, 0x7c, 0x77, 0x7b, 0xf2, 0x6b, 0x6f, 0xc5, 0x30, 0x01, 0x67, 0x2b, 0xfe, 0xd7, 0xab, 0x76 },
            { 0xca, 0x82, 0xc9, 0x7d, 0xfa, 0x59, 0x47, 0xf0, 0xad, 0xd4, 0xa2, 0xaf, 0x9c, 0xa4, 0x72, 0xc0 },
            { 0xb7, 0xfd, 0x93, 0x26, 0x36, 0x3f, 0xf7, 0xcc, 0x34, 0xa5, 0xe5, 0xf1, 0x71, 0xd8, 0x31, 0x15 },
            { 0x04, 0xc7, 0x23, 0xc3, 0x18, 0x96, 0x05, 0x9a, 0x07, 0x12, 0x80, 0xe2, 0xeb, 0x27, 0xb2, 0x75 },
            { 0x09, 0x83, 0x2c, 0x1a, 0x1b, 0x6e, 0x5a, 0xa0, 0x52, 0x3b, 0xd6, 0xb3, 0x29, 0xe3, 0x2f, 0x84 },
            { 0x53, 0xd1, 0x00, 0xed, 0x20, 0xfc, 0xb1, 0x5b, 0x6a, 0xcb, 0xbe, 0x39, 0x4a, 0x4c, 0x58, 0xcf },
            { 0xd0, 0xef, 0xaa, 0xfb, 0x43, 0x4d, 0x33, 0x85, 0x45, 0xf9, 0x02, 0x7f, 0x50, 0x3c, 0x9f, 0xa8 },
            { 0x51, 0xa3, 0x40, 0x8f, 0x92, 0x9d, 0x38, 0xf5, 0xbc, 0xb6, 0xda, 0x21, 0x10, 0xff, 0xf3, 0xd2 },
            { 0xcd, 0x0c, 0x13, 0xec, 0x5f, 0x97, 0x44, 0x17, 0xc4, 0xa7, 0x7e, 0x3d, 0x64, 0x5d, 0x19, 0x73 },
            { 0x60, 0x81, 0x4f, 0xdc, 0x22, 0x2a, 0x90, 0x88, 0x46, 0xee, 0xb8, 0x14, 0xde, 0x5e, 0x0b, 0xdb },
            { 0xe0, 0x32, 0x3a, 0x0a, 0x49, 0x06, 0x24, 0x5c, 0xc2, 0xd3, 0xac, 0x62, 0x91, 0x95, 0xe4, 0x79 },
            { 0xe7, 0xc8, 0x37, 0x6d, 0x8d, 0xd5, 0x4e, 0xa9, 0x6c, 0x56, 0xf4, 0xea, 0x65, 0x7a, 0xae, 0x08 },
            { 0xba, 0x78, 0x25, 0x2e, 0x1c, 0xa6, 0xb4, 0xc6, 0xe8, 0xdd, 0x74, 0x1f, 0x4b, 0xbd, 0x8b, 0x8a },
            { 0x70, 0x3e, 0xb5, 0x66, 0x48, 0x03, 0xf6, 0x0e, 0x61, 0x35, 0x57, 0xb9, 0x86, 0xc1, 0x1d, 0x9e },
            { 0xe1, 0xf8, 0x98, 0x11, 0x69, 0xd9, 0x8e, 0x94, 0x9b, 0x1e, 0x87, 0xe9, 0xce, 0x55, 0x28, 0xdf },
            { 0x8c, 0xa1, 0x89, 0x0d, 0xbf, 0xe6, 0x42, 0x68, 0x41, 0x99, 0x2d, 0x0f, 0xb0, 0x54, 0xbb, 0x16 }
        };


        private string _stringIn;
        public override string StringIn
        {
            protected get => _stringIn;
            set
            {
                int padLength = value.Length % 16 == 0 ? 0 : ((value.Length / 16) + 1) * 16;
                _stringIn = value.PadRight(padLength, '\0');
            }
        }
        private readonly List<byte[][]> _stringInBytes = new List<byte[][]>();


        private byte[] _keyHashByte;
        private readonly byte[][] _cipherKey = new byte[4][];
        private readonly byte[][][] _roundsKeys = new byte[11][][];

        private void GetCipherKey()
        {
            for (int i = 0; i < _keyHashByte.Length; i++)
            {
                if (i % 4 == 0) _cipherKey[i / 4] = new byte[4];

                _cipherKey[i / 4][i % 4] = _keyHashByte[i];
            }
        }
        private void GetRoundsKeys()
        {
            _roundsKeys[0] = _cipherKey;

            for (int i = 1; i < _roundsKeys.Length; i++)
            {
                _roundsKeys[i] = new byte[4][];

                for (int j = 0; j < _roundsKeys[i].Length; j++)
                {
                    byte[] wi = new byte[_roundsKeys[i].Length];
                    if (j == 0)
                    {
                        Array.Copy(_roundsKeys[i - 1][3], wi, _roundsKeys[i - 1][3].Length);

                        RotWord(ref wi);
                        SubBytesWord(ref wi);

                        wi = XorTwoWords(wi, _rCon[i - 1]);
                        wi = XorTwoWords(wi, _roundsKeys[i - 1][j]);
                    }
                    else
                    {
                        Array.Copy(_roundsKeys[i][j - 1], wi, _roundsKeys[i][j - 1].Length);
                        wi = XorTwoWords(wi, _roundsKeys[i - 1][j]);
                    }

                    _roundsKeys[i][j] = wi;
                }
            }
        }


        private void RotWord(ref byte[] word)
        {
            byte first = word[0];

            for (int i = 1; i < word.Length; i++) word[i - 1] = word[i];
            word[word.Length - 1] = first;
        }
        private void SubBytesWord(ref byte[] word)
        {
            for (int i = 0; i < word.Length; i++) word[i] = _sBox[(word[i] & 0xf0) >> 4, word[i] & 0xf];
        }
        private byte[] XorTwoWords(byte[] word1, byte[] word2)
        {
            byte[] outWord = new byte[4];
            for (int i = 0; i < word1.Length; i++) outWord[i] = (byte)(word1[i] ^ word2[i]);

            return outWord;
        }

        private byte[] GetColFromMatrix(byte[][] inputMatrix, int numberCol)
        {
            byte[] outCol = new byte[inputMatrix.GetLength(0)];

            for (int i = 0; i < inputMatrix.GetLength(0); i++) outCol[i] = inputMatrix[i][numberCol];

            return outCol;
        }
        private void SetColToMatrix(ref byte[][] inputMatrix, byte[] inputCol, int numberCol)
        {
            for (int i = 0; i < inputMatrix.GetLength(0); i++) inputMatrix[i][numberCol] = inputCol[i];
        }


        public override void Crypto()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < _stringInBytes.Count; i++)
            {
                AddRoundKey(_stringInBytes[i], _roundsKeys[0]);
                for (int j = 1; j < _roundsKeys.Length - 1; j++)
                {
                    SubBytes(_stringInBytes[i]);
                    ShiftRows(_stringInBytes[i]);
                    _stringInBytes[i] = MixColumns(_stringInBytes[i]);
                    AddRoundKey(_stringInBytes[i], _roundsKeys[j]);
                }

                SubBytes(_stringInBytes[i]);
                ShiftRows(_stringInBytes[i]);
                AddRoundKey(_stringInBytes[i], _roundsKeys[10]);

                Console.WriteLine("Bytes");
                PrintMatrix(_stringInBytes[i]);

                byte[] _stringOutBytes = GetByteOutputText(_stringInBytes[i]);
                sb.Append(Convert.ToBase64String(_stringOutBytes));
            }

            StringOut = sb.ToString();
            Console.WriteLine(StringOut);
        }
        private void PrintMatrix(byte[][] matrix)
        {
            for(int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Console.Write("{0:x}", matrix[i][j].ToString("x").PadLeft(2, '0'));
                }
                Console.Write("  ");
            }
            Console.WriteLine();
        }

        private void AddRoundKey(byte[][] stringInMatrix, byte[][] keyMatrix)
        {
            for (int i = 0; i < stringInMatrix.Length; i++) stringInMatrix[i] = XorTwoWords(stringInMatrix[i], keyMatrix[i]);
        }
        private void SubBytes(byte[][] stringInMatrix)
        {
            for (int i = 0; i < stringInMatrix.Length; i++) SubBytesWord(ref stringInMatrix[i]);
        }
        private void ShiftRows(byte[][] stringInMatrix)
        {
            for (int i = 1; i < stringInMatrix.Length; i++)
            {
                byte[] col = GetColFromMatrix(stringInMatrix, i);
                for (int j = 0; j < i; j++) RotWord(ref col);
                SetColToMatrix(ref stringInMatrix, col, i);
            }
        }
        private byte[][] MixColumns(byte[][] stringInMatrix)
        {
            byte[][] tempArray = new byte[4][];

            for (int i = 0; i < stringInMatrix.GetLength(0); i++)
            {
                tempArray[i] = new byte[4];
                tempArray[i][0] = (byte)((GMul(0x02, stringInMatrix[i][0])) ^ (GMul(0x03, stringInMatrix[i][1])) ^ stringInMatrix[i][2] ^ stringInMatrix[i][3]);
                tempArray[i][1] = (byte)(stringInMatrix[i][0] ^ (GMul(0x02, stringInMatrix[i][1])) ^ (GMul(0x03, stringInMatrix[i][2])) ^ stringInMatrix[i][3]);
                tempArray[i][2] = (byte)(stringInMatrix[i][0] ^ stringInMatrix[i][1] ^ (GMul(0x02, stringInMatrix[i][2])) ^ (GMul(0x03, stringInMatrix[i][3])));
                tempArray[i][3] = (byte)((GMul(0x03, stringInMatrix[i][0])) ^ stringInMatrix[i][1] ^ stringInMatrix[i][2] ^ (GMul(0x02, stringInMatrix[i][3])));
            }

            return tempArray;

            byte GMul(byte a, byte b)
            {
                byte p = 0;

                for (int counter = 0; counter < 8; counter++)
                {
                    if ((b & 1) != 0) p ^= a;

                    bool hi_bit_set = (a & 0x80) != 0;

                    a <<= 1;
                    if (hi_bit_set) a ^= 0x1B;
                    b >>= 1;
                }

                return p;
            }
        }


        public override void Decrypto()
        {

        }


        public AesEncryption(string stringIn, string key)
        {
            StringIn = stringIn;

            GetByteKey(key);
            GetByteInputText(StringIn);
        }

        private void GetByteKey(string key)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = stringsEncoding.GetBytes(key);
                _keyHashByte = md5.ComputeHash(inputBytes);

                GetCipherKey();
                GetRoundsKeys();
            }
        }
        private void GetByteInputText(string text)
        {
            string[] strings = text.SplitStringForSegmentsLength(16);

            foreach (string str in strings)
            {
                byte[] textBytes = stringsEncoding.GetBytes(str);
                byte[][] textFinalBytes = new byte[4][];

                for (int i = 0; i < textBytes.Length; i++)
                {
                    if (i % 4 == 0) textFinalBytes[i / 4] = new byte[4];

                    textFinalBytes[i / 4][i % 4] = textBytes[i];
                }

                _stringInBytes.Add(textFinalBytes);
            }
        }
        private byte[] GetByteOutputText(byte[][] inputBytes)
        {
            byte[] outputBytes = new byte[16];
            for (int i = 0; i < 4; i++) for (int j = 0; j < 4; j++) outputBytes[i * inputBytes.GetLength(0) + j] = inputBytes[i][j];

            return outputBytes;
        }
    }
}
