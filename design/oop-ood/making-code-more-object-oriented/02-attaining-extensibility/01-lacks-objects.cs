using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace solution_array_with_number_to_object
{
    [TestClass]
    public class SolutionWithSelector
    {
        [TestMethod]
        public void HardToMakeChanges()
        {
            var res = Sum(new Values(new[] { 1, 2, 3 }), new Selector());
        }

        static int Sum(Values values, Selector selector)
        {
            int sum = values.Sum(selector);           

            return sum;
        }
    }

    public class Values
    {
        public Values(int [] values)
        {

        }

        internal int Sum(Selector selector)
        {
            throw new NotImplementedException();
        }
    }

    public class Selector
    {
        internal IEnumerable<int> Pick(int[] values)
        {
            throw new NotImplementedException();
        }
    }
}

namespace solution_with_selector
{
    [TestClass]
    public class SolutionWithSelector
    {
        [TestMethod]
        public void HardToMakeChanges()
        {
            var res = Sum(new[] { 1, 2, 3 }, new Selector());
        }

        static int Sum(int[] values, Selector selector)
        {
            int sum = 0;
            foreach (var item in selector.Pick(values))
                sum += item;

            return sum;
        }
    }

    public class Selector
    {
        internal IEnumerable<int> Pick(int[] values)
        {
            throw new NotImplementedException();
        }
    }
}


    namespace lacks_objects
{
   

    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void HardToMakeChanges()
        {
           var res = Sum(new[] { 1, 2, 3 }, true);
        }

        static int Sum(int[] values, bool oddNumbersOnly)
        {
            int sum = 0;
            for (int i = 0; i < values.Length; i++)
            {
                if (!oddNumbersOnly || values[i] % 2 != 0)
                {
                    sum += values[i];
                }
            }

            return sum;
        }
    }
}
