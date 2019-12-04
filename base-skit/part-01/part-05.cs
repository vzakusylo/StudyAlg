using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace Page170
{

    [TestClass]
    public class Page170
    {
        [TestMethod]
        public void Main()
        {
            StreamFactory factory = GenerateSimpleData;

            using (var stream = factory())
            {
                int data;
                while((data = stream.ReadByte()) != -1)
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