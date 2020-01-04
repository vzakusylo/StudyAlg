using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace solution_null_ref_branch_maybestring
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
        static void ShowIt(MaybeString data)
        {
            MaybeString upper = data.ToMaybeUpper();// there are never be null

            Console.WriteLine(upper);
        }
    }

    public class MaybeString
    {
        internal MaybeString ToMaybeUpper()
        {
            throw new NotImplementedException();
        }
    }

}

namespace solution_null_ref_branch
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