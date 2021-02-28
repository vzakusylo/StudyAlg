using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace birthday_cake_candles
{
    //https://www.hackerrank.com/challenges/birthday-cake-candles/problem

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {          
            int result = birthdayCakeCandlesArray(new[] { 3, 2, 1, 3 });
            Assert.AreEqual(2, result);

            result = birthdayCakeCandlesArray(new[] { 18, 90, 90, 13, 90, 75, 90, 8, 90, 43 });
            Assert.AreEqual(5, result);

            result = birthdayCakeCandlesDict(new[] { 3, 2, 1, 3 });
            Assert.AreEqual(2, result);

            result = birthdayCakeCandlesDict(new[] { 18, 90, 90, 13, 90, 75, 90, 8, 90, 43 });
            Assert.AreEqual(5, result);
        }

        // Complete the birthdayCakeCandles function below.
        static int birthdayCakeCandlesDict(int[] ar)
        {
            var freq = new Dictionary<int, int>();
            foreach (var item in ar)
            {
                if (freq.ContainsKey(item))
                {
                    freq[item] += 1;
                }
                else
                {
                    freq[item] = 1;
                }
            }

            return freq[freq.Keys.Max()];
        }

            // Complete the birthdayCakeCandles function below.
            static int birthdayCakeCandlesArray(int[] ar)
        {           
            int maxCandles = 0;
            int maxValue = 0;
            for (int i = 0; i < ar.Length; i++)
            {
                if(ar[i] > maxValue)
                {
                    maxValue = ar[i];
                }
            }

            int[] friquencies = new int[maxValue+1];
            for (int i = 0; i < ar.Length; i++)
            {
                friquencies[ar[i]] += 1;
            }          

            for (int i = 0; i < friquencies.Length; i++)
            {
                if (friquencies[i] > maxCandles)
                {
                    maxCandles = friquencies[i];
                }
            }
            return maxCandles;
        }
    }
}
