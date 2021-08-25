using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsefulMethods
{
    public static class MathMethods
    {
        public static bool IsPrime(int number)
        {
            for (int i = 2; i < Math.Sqrt(number) + 1; i++) if (number % i == 0) return false;

            return true;
        }
        public static int PrimeNumberGenerate(int min, int max, Random rand)
        {
            int simple_number;

            while (true)
            {
                simple_number = rand.Next(min, max);
                if (simple_number % 2 == 0) simple_number++;

                if (IsPrime(simple_number)) break;
            }

            return simple_number;
        }
    }
}
