using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace thepowersum
{
    // https://www.hackerrank.com/challenges/the-power-sum/problem?h_r=internal-search&isFullScreen=true

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
           
        }

        public static int powerSum(int X, int N)
        {
            int upperLimit = 1;
            bool additionalOneResult = false;
            while (upperLimit < X)
            {
                if (Math.Pow(upperLimit, N) >= X)
                {
                    if (Math.Pow(upperLimit, N) == X)
                    {
                        additionalOneResult = true;
                    }
                    break;
                }
                upperLimit++;
            }

            List<List<int>> result = new List<List<int>>();
            List<int> tempList = new List<int>();
            dfs(upperLimit, 1, 0, X, N, tempList, result);

            return additionalOneResult ? 1 + result.Count() : result.Count();

        }

        static void dfs(int upperLimit, int currentIndex, int currentSum, int X, int N, List<int> tempList, List<List<int>> result)
        {
            if (currentIndex > upperLimit) return;

            if (currentSum > X) return;

            if (currentSum == X)
            {
                List<int> copiedList = new List<int>();
                foreach (var element in tempList)
                {
                    copiedList.Add(element);
                }

                copiedList.Sort();
                if (!result.Contains(copiedList))
                {
                    result.Add(copiedList);
                }
                return;
            }

            for (int i = currentSum; i < upperLimit; i++)
            {
                currentSum += (int)Math.Pow(i, N);
                tempList.Add(i);
                dfs(upperLimit, i + 1, currentSum, X, N, tempList, result);
                int y = tempList[tempList.Count - 1];
                currentSum -= (int)Math.Pow(y, N);
                tempList.Remove(tempList.Count - 1);

            }
        }

    }
}
