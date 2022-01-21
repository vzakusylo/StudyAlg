using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ctciransomnote
{
    // https://www.hackerrank.com/challenges/ctci-ransom-note/problem?h_r=internal-search&isFullScreen=true

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var m = "apgo clm w lxkvg mwz elo bg elo lxkvg elo apgo apgo w elo bg";
            var n = "elo lxkvg bg mwz clm w";

            var res = checkMagazine(m.Split(" ").ToList(), n.Split(" ").ToList());

            Assert.AreEqual("Yes", res);

        }

        public static string checkMagazine(List<string> magazine, List<string> note)
        {
            var magHashMap = new Dictionary<string, int>();
            var noteHashMap = new Dictionary<string, int>();

            for (int i = 0; i < magazine.Count; i++)
            {
                if (magHashMap.ContainsKey(magazine[i]))
                {
                    magHashMap[magazine[i]] += 1;
                }
                else
                {
                    magHashMap.Add(magazine[i], 1);
                }
            }

            for (int i = 0; i < note.Count; i++)
            {
                if (noteHashMap.ContainsKey(note[i]))
                {
                    noteHashMap[note[i]] += 1;
                }
                else
                {
                    noteHashMap.Add(note[i], 1);
                }
            }

            foreach (var word in noteHashMap.Keys)
            {
                if (!magHashMap.ContainsKey(word))
                {
                    Console.WriteLine("No");
                    return "No";
                }

                if (magHashMap[word] < noteHashMap[word])
                {
                    Console.WriteLine("No");
                    return "No";
                }
            }
            Console.WriteLine("Yes");
            return "Yes";
        }
    }
}
