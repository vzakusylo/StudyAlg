using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ctci_array_left_rotation
{
    // https://www.hackerrank.com/challenges/ctci-array-left-rotation/

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
           var a = new List<int>(){1,2,3,4,5};
           var res = RotLeft(a, 4);
           Assert.AreEqual(4, res);

           CollectionAssert.AreEqual(new List<int>(){ 5, 1, 2, 3, 4 }, res);
        }

        public static List<int> RotLeft(List<int> a, int d)
        {
            var size = a.Count;
            var rotatedArr = new int[size];
            
            var i = 0;
            var rotatedIndex = d;
            while (rotatedIndex < size)
            {
                rotatedArr[i] = a[rotatedIndex];
                i++;
                rotatedIndex++;
            }

            rotatedIndex = 0;
            while (rotatedIndex < d)
            {
                rotatedArr[i] = a[rotatedIndex];
                i++;
                rotatedIndex++;
            }

            return rotatedArr.ToList();
        }

    }
}
