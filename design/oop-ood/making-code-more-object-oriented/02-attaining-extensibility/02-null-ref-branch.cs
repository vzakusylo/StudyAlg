using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace solution_null_ref_brunc
{
    [TestClass]
    public class EntryPoint
    {
        [TestMethod]
        public void Main()
        {

        }
    }

    public class Program
    {
        static void ShowIt(string data)
        {
            string upper;
            if (data == null) // branching around null ref (
            {
                upper = null;
            }
            else
            {
                upper = data.ToLower();
            }

            Console.WriteLine(upper);
        }
    }

}