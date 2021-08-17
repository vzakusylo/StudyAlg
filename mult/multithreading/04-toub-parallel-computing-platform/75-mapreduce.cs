using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace mapreduce
{
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public async Task Run()
        {
            char[] delimiters = Enumerable.Range(0, 256)
                .Select(i => (char)i).Where(c => Char.IsWhiteSpace(c) || Char.IsPunctuation(c))
                .ToArray(); 

            var files = Directory.EnumerateFiles(@"C:\temp", "*.xml").AsParallel();
            var count = files.MapReduce(
                path => File.ReadAllLines(path).SelectMany(line => line.Split(delimiters)),
                word => word,
                group => new[] { new KeyValuePair<string, int>(group.Key, group.Count()) });

            Console.WriteLine(count.Count());
        }
    }

    public static class Ext
    {
        public static IEnumerable<TResult> MapReduce<TSource, TMapped, TKey, TResult>(
            this IEnumerable<TSource> source, 
            Func<TSource, IEnumerable<TMapped>> map,
            Func<TMapped, TKey> keySelector, 
            Func<IGrouping<TKey, TMapped>,IEnumerable<TResult>> reduce)
        {
            return source.SelectMany(map)
                .GroupBy(keySelector)
                .SelectMany(reduce);
        }
    }
}
