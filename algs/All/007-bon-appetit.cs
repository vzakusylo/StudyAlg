using System.Collections.Generic;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace bon_appetit
{
    //https://www.hackerrank.com/challenges/bon-appetit/problem?utm_campaign=challenge-recommendation&utm_medium=email&utm_source=24-hour-campaign

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var result = bonAppetit(new List<int>(){3,10,2,9}, 1, 12);
            Assert.AreEqual(5, result.Item1);

            result = bonAppetit(new List<int>() { 3, 10, 2, 9 }, 1, 7);
            Assert.AreEqual("Bon Appetit", result.Item2);
        }

        // Complete the bonAppetit function below.
        static (int, string) bonAppetit(List<int> bill, int k, int b)
        {
            var bothSum = 0;
            for (int i = 0; i <  bill.Count; i++)
            {
                if (i == k)
                {
                    continue;
                }

                bothSum += bill[i];
            }

            var singleBill = bothSum / 2;
            var anna = b - singleBill;

            if (anna < b && anna > 0)
            {
                return (anna, string.Empty);
            }
            else
            {
                return (0, "Bon Appetit");
            }
        }

    }
}
