using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace two_characters
{
    //https://www.hackerrank.com/challenges/two-characters/problem?utm_campaign=challenge-recommendation&utm_medium=email&utm_source=7-day-campaign

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var result = alternate("beabeefeab");
            Assert.AreEqual(5, result);

            result = alternate("asvkugfiugsalddlasguifgukvsa");
            Assert.AreEqual(0, result);

            result = alternate("asdcbsdcagfsdbgdfanfghbsfdab");
            Assert.AreEqual(8, result);


        }
        // Complete the alternate function below.
        static int alternate(string s)
        {
            var chars = s.ToArray().Distinct().ToArray();
            var uniquePairs = new string[chars.Length * chars.Length];

            int uniquePairsIndex = 0;
            for (int i = 0; i < chars.Length; i++)
            {
                for (int j = 0; j < chars.Length; j++)
                {
                    uniquePairs[uniquePairsIndex++] = new string(new char[] { chars[i], chars[j] });
                }
            }

            List<string> res = new List<string>();
            for (int i = 0; i < uniquePairs.Length; i++)
            {
                var modifiedString = string.Copy(s);
                var pair = uniquePairs[i].ToArray();
                for (int j = 0; j < 2; j++)
                {
                    modifiedString = modifiedString.Replace(new string(new char[] { pair[j] }), string.Empty);
                    if (IsDistinct(modifiedString) && !res.Contains(modifiedString))
                        res.Add(modifiedString);
                }
            }

            var maxLength = 0;
            foreach (var item in res.OrderByDescending(x => x.Length))
            {
                if (item.Length > maxLength && item.ToArray().Distinct().ToArray().Length == 2)
                {
                    maxLength = item.Length;
                }
            }

            return maxLength;


            //if (IsDistinct(s))
            //    return s.Length;


            //for (int i = 0; i < s.Length; i++)
            //{
            //    var modifiedString = string.Copy(s);
            //    for (int j = 0; j < s.Length; j++)
            //    {
            //        var index = (j + i) % s.Length;
            //        modifiedString = modifiedString.Replace(s[index].ToString(), string.Empty);
            //        if (IsDistinct(modifiedString) && !res.Contains(modifiedString))
            //            res.Add(modifiedString);
            //    }
            //}

            //var maxLength = 0;
            //foreach (var item in res.OrderByDescending(x=> x.Length))
            //{
            //    if (item.Length > maxLength && item.ToArray().Distinct().ToArray().Length == 2)
            //    {
            //        maxLength = item.Length;
            //    }
            //}

            //return maxLength;
        }

        private static bool IsDistinctOneByOne(string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;
           
            var uniqueChars = s.ToArray().Distinct().ToArray();
            var charCounts = new Dictionary<char, int>();
            //if (uniqueChars.Length > 2)
            //{
            //    return false;
            //}
            foreach (var uc in uniqueChars)
            {
                var count = s.ToArray().Where(x => x == uc).Count();
                charCounts.Add(uc, count);
                //var duplicates = s.Contains(new string(new[] { uc, uc }));
                //if (duplicates)
                //    return false;
            }
            foreach (var item in charCounts)
            {

            }
           
            return true;
        }

        private static bool IsDistinct(string s)
        {
            if (string.IsNullOrEmpty(s))
                return false;
            var uniqueChars = s.ToArray().Distinct().ToArray();
            foreach (var uc in uniqueChars)
            {
                if (s.Contains(new string(new[] { uc, uc })))
                    return false;
            }
            return true;
        }

       

    }
}
