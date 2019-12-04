using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Page306
{
    [TestClass]
    public class Page306
    {
        [TestMethod]
        public void Main() //Необходимо только преобразовать код в листинге 6.9
        {
            var collection = Enumerable.Range(0, 10);
            var res = collection.Where(x => x % 2 != 0);
            foreach (int item in res)
            {
                Console.WriteLine(item.ToString());
            }
        }

    }

    public static class Ext {
        public static IEnumerable<T> Where<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            if (source == null || predicate == null)
            {
                throw new ArgumentException();
            }

            return WhereImpl(source, predicate);
        }

        private static IEnumerable<T> WhereImpl<T>(IEnumerable<T> source, Func<T, bool> predicate){
            foreach(T item in source)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }
    }
}
