using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace larrysarray
{
    //https://www.hackerrank.com/challenges/larrys-array

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var a = new List<int>() {1, 3, 4, 2};
            var res = larrysArray(a);
            Assert.AreEqual("YES", res);

            a = new List<int>() {1, 2, 3, 5, 4};
            res = larrysArray(a);
            Assert.AreEqual("NO", res);
        }

        public static string larrysArray(List<int> A)
        {
            Dictionary<int, int> hashMap = new Dictionary<int, int>();
            for (int i = 0; i < A.Count; i++)
            {
                hashMap.Add(A[i], i);
            }

            int index = 0;
            while (index < A.Count)
            {
                if (A[index] == index + 1)
                {
                    index++;
                    continue;
                }

                int nextMinPosition = hashMap[index + 1];
                if (nextMinPosition - index >= 2)
                {
                    int temp = A[nextMinPosition - 2];
                    A[nextMinPosition - 2] = A[nextMinPosition - 1];
                    A[nextMinPosition - 1] = A[nextMinPosition];
                    A[nextMinPosition] = temp;
                    hashMap[A[nextMinPosition - 2]] = nextMinPosition - 2;
                    hashMap[A[nextMinPosition - 1]] = nextMinPosition - 1;
                    hashMap[A[nextMinPosition]] = nextMinPosition;
                }
                else if (nextMinPosition - index == 1)
                {

                    if (nextMinPosition == A.Count - 1) return "NO";

                    int temp = A[nextMinPosition - 1];
                    A[nextMinPosition - 1] = A[nextMinPosition];
                    A[nextMinPosition] = A[nextMinPosition + 1];
                    A[nextMinPosition + 1] = temp;
                    hashMap[A[nextMinPosition - 1]] = nextMinPosition - 1;
                    hashMap[A[nextMinPosition]] = nextMinPosition;
                    hashMap[A[nextMinPosition + 1]] = nextMinPosition + 1;
                }
            }
            return index == A.Count ? "YES" : "NO";
        }



    }
}
