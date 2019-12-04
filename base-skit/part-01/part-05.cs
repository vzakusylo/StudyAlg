using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;

namespace Page173
{
    [TestClass]
    public class Page173
    {
        [TestMethod]
        public void Main()
        {
            printReverse("Hello world");
            printRoot(2);
            printMean(new double[] { 1.5, 2.5, 3, 4.5 });
        }

        Action<string> printReverse = delegate (string text) // Encapsulate method that has a single parameter and does not return value.
        {
            char[] chars = text.ToCharArray();
            Array.Reverse(chars);
            Console.WriteLine(new string(chars));
        };

        Action<int> printRoot = delegate (int number)
        {
            Console.WriteLine(Math.Sqrt(number));
        };

        Action<IList<double>> printMean = delegate (IList<double> numbers)
        {
            double total = 0;
            foreach (double value in numbers)
            {
                total += value;
            }
            Console.WriteLine(total / numbers.Count);
        };

    }
}


namespace Page172
{
    [TestClass]
    public class Page172
    {
        delegate void SampleDelegate(string x);

        [TestMethod]
        public void Main()
        {
            Derivded x = new Derivded();
            SampleDelegate factory = new SampleDelegate(x.CandidateAction);
            factory("test");
        }        

        public class Snippet
        {
            public void CandidateAction(string x)
            {
                Console.WriteLine(x);
            }
        }

        public class Derivded : Snippet
        {
            public void CandidateAction(object o)
            {
                Console.WriteLine(o);
            }
        }
    }
}


namespace Page170
{
    [TestClass]
    public class Page170
    {
        [TestMethod]
        public void Main()
        {
            StreamFactory factory = GenerateSimpleData;

            //using (MemoryStream stream = factory())  // Error CS0266  Cannot implicitly convert type 'System.IO.Stream' to 'System.IO.MemoryStream'.An explicit conversion exists (are you missing a cast?)  
            using (Stream stream = factory())
            {
                int data;
                while ((data = stream.ReadByte()) != -1)
                {
                    Console.WriteLine(data);
                }
            }
        }

        delegate Stream StreamFactory(); // declare delegate with type Stream
        static MemoryStream GenerateSimpleData()
        {
            byte[] buffer = new byte[16];
            for(int i=0; i<buffer.Length; i++)
            {
                buffer[i] = (byte)i;
            }
            return new MemoryStream(buffer);
        }
    }
}