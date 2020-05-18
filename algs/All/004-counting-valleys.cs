using System;
using System.ComponentModel.Design;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace counting_valleys
{
    //https://www.hackerrank.com/challenges/counting-valleys/problem?h_l=interview&playlist_slugs%5B%5D=interview-preparation-kit&playlist_slugs%5B%5D=warmup

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {          
            int result = countingValleys(8, "UDDDUDUU");
            Assert.AreEqual(1, result);

            result = countingValleys(12, "DDUUDDUDUUUD");
            Assert.AreEqual(2, result);

            result = countingValleys(10, "UDUUUDUDDD");
            Assert.AreEqual(0, result);
        }

        // Complete the countingValleys function below.
        static int countingValleys(int n, string s)
        {
            var upLevel = 0;
            var downLevel = 0;
            var isUp = false;
            var isDown = false;
            var steps = s.ToArray();
            char previuseStep = ' ';
            var upMax = 0;
            var downMax = 0;

            for (int i = 0; i < s.Length; i++)
            {
                var currentStep = s[i];
                if (i == 0)
                {
                    previuseStep = s[0];                    
                }
                else
                {
                    previuseStep = s[i - 1];
                }
                if (currentStep == 'U' && previuseStep == 'U')
                {
                    upLevel += 1;
                }
                if (currentStep == 'D' && previuseStep == 'D')
                {
                    downLevel += 1;                  
                }
                if (previuseStep != currentStep)
                {
                    if (upLevel > upMax)
                    {
                        upMax = upLevel;
                    }
                    if (downLevel > downMax)
                    {
                        downMax = downLevel;
                    }
                    upLevel = 0;
                    downLevel = 0;
                }
            }

            return Math.Min(upMax, upMax);
          
        }
    }
}
