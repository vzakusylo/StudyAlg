using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace camelcase
{
    //https://www.hackerrank.com/challenges/camelcase/problem?utm_campaign=challenge-recommendation&utm_medium=email&utm_source=7-day-campaign&h_r=next-challenge&h_v=zen

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var result = camelcase("saveChangesInTheEditor");
            Assert.AreEqual(5, result);

            result = camelcase("save");
            Assert.AreEqual(1, result);
        }

        // Complete the camelcase function below.
        static int camelcase(string s)
        {
            var upperCharCount = 0;            
            foreach (var item in s.ToArray())
            {
                if (char.IsUpper(item))
                {  
                    upperCharCount++;
                }
                else if (!char.IsUpper(item) && upperCharCount == 0)
                {
                    upperCharCount = 1;
                }
            }
            return upperCharCount;
        }

    }
}
