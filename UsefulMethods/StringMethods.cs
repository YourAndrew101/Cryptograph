using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsefulMethods
{
    public static class StringMethods
    {
        public static string[] SplitStringForSegmentsLength(this string str, int segmentLength)
        {          
            string[] strings = new string[str.Length / segmentLength];
            if (str.Length < segmentLength) return new string[] { str };

            for (int i = 0; i < strings.Length; i++)
            {
                StringBuilder sb = new StringBuilder();
                for (int k = 0; k < segmentLength; k++) sb.Insert(k, str[i * segmentLength + k]);

                strings[i] = sb.ToString();
            }

            return strings;
        }

        public static string[] SplitStringForSegmentsCount(this string str, int segmentsCount)
        {
            if (str.Length < segmentsCount) throw new ArgumentException("Бажана кількість сегментів перевищує довжину рядка");

            char[][] strInCharArray = ArrayMethods.SplitArrayForSegment(str.ToCharArray(), segmentsCount);
            strInCharArray = strInCharArray.DistributeItems2dArray(segmentsCount);
            string[] ouputStringArray = new string[segmentsCount];

            for (int i = 0; i < strInCharArray.GetLength(0); i++) ouputStringArray[i] = new string(strInCharArray[i]);

            return ouputStringArray;           
        }
    }
}
