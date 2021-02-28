using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace bigger_is_greater
{
    // https://www.hackerrank.com/challenges/bigger-is-greater

    [TestClass]
    public class Program
    {
        [TestMethod]
        public void Main()
        {
            var result = biggerIsGreater("ab");
            Assert.AreEqual("ba", result);

            result = biggerIsGreater("lmno");
            Assert.AreEqual("lmon", result);

            result = biggerIsGreater("dcba");
            Assert.AreEqual("no answer", result);

            result = biggerIsGreater("dcbb");
            Assert.AreEqual("no answer", result);

            result = biggerIsGreater("abdc");
            Assert.AreEqual("acbd", result);

            result = biggerIsGreater("abcd");
            Assert.AreEqual("abdc", result);

            result = biggerIsGreater("fedcbabcd");
            Assert.AreEqual("fedcbabdc", result);
        }

        public static void RotateRight(IList sequence, int count)
        {
            object tmp = sequence[count - 1];
            sequence.RemoveAt(count - 1);
            sequence.Insert(0, tmp);
        }

        public static void RotateLeft(IList sequence, int count)
        {
            object tmp = sequence[count + 1];
            sequence.RemoveAt(count + 1);
            sequence.Insert(0, tmp);
        }

        public static IEnumerable<IList> Permutate(List<int> sequence, int count)
        {
            if (count == 1) yield return sequence;
            else
            {
                for (int i = 0; i < count; i++)
                {
                    foreach (var perm in Permutate(sequence, count - 1))
                        yield return perm;
                    RotateLeft(sequence, count-1);
                }
            }
        }

        static string biggerIsGreater(string w)
        {
            List<int> seq = w.ToArray().Select(x => (int)x).ToList();
            var variations = Permutate(seq, seq.Count);

            bool isNext = false;           
            foreach (var permu in variations)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var i in permu)
                    sb.Append(Char.ConvertFromUtf32((int)i));

                if (sb.ToString() == w)  
                {
                    isNext = true;
                    continue;
                }

                if (isNext)
                {
                    return sb.ToString();
                }
            }

            return "no answer";
        }
    }
}
