using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Page277
{
    [TestClass]
    public class Page277
    {
        [TestMethod]
        public void Main()
        {
            Expression<Func<string, string, bool>> expression = (x, y) => x.StartsWith(y);
            Func<string,string,bool> compiled = expression.Compile();
            Console.WriteLine(compiled("First", "Second"));
            Console.WriteLine(compiled("First", "Fir"));

            MethodInfo method = typeof(string).GetMethod("StartsWith", new[] { typeof(string) });
            var target = Expression.Parameter(typeof(string), "x");
            var methodArg = Expression.Parameter(typeof(string), "y");
            Expression[] methodArgs = new[] { methodArg };

            Expression call = Expression.Call(target, method, methodArg);

            var lambdaParameters = new[] { target, methodArg };
            var lambda = Expression.Lambda<Func<string, string, bool>>(call, lambdaParameters);
            var compiled2 = lambda.Compile();
            Console.WriteLine(compiled2("First", "Second"));
            Console.WriteLine(compiled2("First", "Fir"));
        }
    }
}

namespace Page276
{
    [TestClass]
    public class Page276
    {
        [TestMethod]
        public void Main()
        {
            Expression<Func<int>> return5 = () => 5;
            Func<int> compiled = return5.Compile();
            Console.WriteLine(compiled);
        }
    }
}

namespace Page273
{
    [TestClass]
    public class Page273
    {
        [TestMethod]
        public void Main()
        {
            Expression firstArg = Expression.Constant(2);
            Expression secondArg = Expression.Constant(3);
            Expression add = Expression.Add(firstArg, secondArg);
            Console.WriteLine(add);

            Func<int> compliled = Expression.Lambda<Func<int>>(add).Compile();
            Console.WriteLine(compliled);

            Console.WriteLine(compliled());
        }
    }
}