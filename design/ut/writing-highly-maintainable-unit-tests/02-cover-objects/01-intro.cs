using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace template
{
    [TestClass]
    public class Client
    {
        // Method under test
        // Scenario which is tested
        // Expected behavior/state/result
        [TestMethod]
        public void Maximum_ArrayContainsOneValue_ReturnsThatValue()
        {

        }
    }

    public class FinancialTarget
    {
        public void AddTargetPoints(MyArray toArray, int count)
        {
            for (int i = 0; i < count; i++)
            {
                toArray.Append(3 + 2 * i);
            }
        }
    }
    // how will the test verify that values has been added as expected;
    // 
    //    Check content             Make sure Append() 
    //    of the array              method was invoked
    //    STATE test                INTERACTION test
    
    public class MyArray
    {
        private int[] Data { get; set; }
        public MyArray(IEnumerable<int> values)
        {
            Data = values.ToArray();
        }

        public MyArray()
        {
            Data = new int[0];
        }

        public void Append(int value)
        {
            int[] data = Data;
            Array.Resize(ref data, data.Length + 1);
            data[data.Length - 1] = value;
            Data = data;
        }

        public int Maximum()
        {
            int max = Data[0];
            for (int i = 0; i < Data.Length; i++)
            {
                if (Data[i] > max)
                {
                    max = Data[i];
                }
            }
            return max;
        }
    }
}
