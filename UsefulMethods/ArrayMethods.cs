using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsefulMethods
{
    public static class ArrayMethods
    {
        public static T[][] SplitArrayForSegment<T>(this T[] inputArray, int countParts)
        {
            int counter, temp;

            if (inputArray.Length % countParts == 0) counter = inputArray.Length / countParts;
            else counter = inputArray.Length / countParts + 1;

            temp = counter;

            var b = inputArray.GroupBy(_ => counter++ / temp).Select(v => v.ToArray());
            return DistributeItems2dArray(b.ToArray(), countParts);
        }

        public static T[][] DistributeItems2dArray<T>(this T[][] inputArray, int wantedArrayLength)
        {
            T[][] outputArray = new T[wantedArrayLength][];
            T[] OneDInputArray = inputArray.TwoDArrayIn1dArray();

            if (OneDInputArray.Length < wantedArrayLength) throw new Exception("Бажана довжина массиву перевищує загальну кількість елементів");

            int baseElements = OneDInputArray.Length / wantedArrayLength;
            int extraElements = OneDInputArray.Length % wantedArrayLength;
            int current1dIndex = 0;
            int current2dIndex = 0;

            List<T> tempList = new List<T>();
            foreach (var item in OneDInputArray)
            {
                if (current2dIndex < baseElements)
                {
                    tempList.Add(item);
                    outputArray[current1dIndex] = tempList.ToArray();
                    current2dIndex++;

                    if (extraElements == 0 && current2dIndex == baseElements)
                    {
                        tempList.Clear();

                        current1dIndex++;
                        current2dIndex = 0;
                    }
                }
                else
                {
                    if (extraElements > 0)
                    {
                        tempList.Add(item);
                        outputArray[current1dIndex] = tempList.ToArray();
                        tempList.Clear();

                        extraElements--;
                        current1dIndex++;
                        current2dIndex = 0;
                    }
                    else
                    {
                        tempList.Clear();
                        current1dIndex++;
                        current2dIndex = 0;
                    }
                }
            }

            return outputArray;
        }

        public static T[] TwoDArrayIn1dArray<T>(this T[][] inputArray)
        {
            List<T> list = new List<T>();

            foreach (var item in inputArray) list.AddRange(item);
            return list.ToArray();
        }
    }
}
