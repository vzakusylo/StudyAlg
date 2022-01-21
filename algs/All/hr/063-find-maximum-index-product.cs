using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace findmaximumindexproduct
{
    // m 15
    //https://www.hackerrank.com/challenges/find-maximum-index-product/problem?h_r=internal-search&isFullScreen=true

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var arr = new List<int> {5, 4, 3, 4, 5};
            var res = solve(arr);

            Assert.AreEqual(8, res);
        }

        public static long solve(List<int> arr)
        {
            int[] left = new int[arr.Count];
            int[] right = new int[arr.Count];
            Stack<int> leftStack = new Stack<int>();
            Stack<int> rightStack = new Stack<int>();

            left[0] = 0;
            leftStack.Push(0);
            for (int i = 1; i < arr.Count; i++)
            {
                while (leftStack.Count != 0 && arr[i] >= arr[leftStack.Peek()])
                {
                    leftStack.Pop();
                }
                if (leftStack.Count == 0)
                {
                    left[i] = 0;
                }
                else
                {
                    left[i] = leftStack.Peek() + 1;
                }

                leftStack.Push(i);
            }

            right[right.Count() - 1] = 0;
            rightStack.Push(right.Count() - 1);
            for (int i = arr.Count - 2; i >= 0; i--)
            {
                while (rightStack.Count != 0 && arr[i] >= arr[rightStack.Peek()])
                {
                    rightStack.Pop();
                }
                if (rightStack.Count == 0)
                {
                    right[i] = 0;
                }
                else
                {
                    right[i] = rightStack.Peek() + 1;
                }
                rightStack.Push(i);
            }

            long maxValue = 0;
            for (int i = 0; i < left.Count(); i++)
            {
                maxValue = Math.Max(maxValue, (long)left[i] * right[i]);
            }
            return maxValue;
        }


    }
}
