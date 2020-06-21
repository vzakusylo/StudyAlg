using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;

namespace function_language_pattern_matching
{
    [TestClass]
    public class Client
    {
        [TestMethod]
        public void Main()
        {
            // we can much options against pattern  
            Option<string> name = Option<string>.Some("something");

            // Advice: Start from the feature consumer. Let the customer define which calls it would like to make. 
            //         Don't start from the feature provider. It might not understand caller's needs well.

            //name
            //    .When(s => s.Length > 3).Do(s => Console.WriteLine($"{s} long"))
            //    .WhenSome().Do(s => Console.WriteLine($"{s} short"))
            //    .WhenNone().Do(() => Console.WriteLine("missing"))
            //    .Execute();

            //string label =
            //    name
            //        .When(s => s.Length > 3).MapTo(s => s.Substring(0, 3) + ".")
            //        .WhenSome().MapTo(s => s)
            //        .WhenNone().MapTo("<empty>")
            //        .Map();
        }
    }

    public class Option<T> : IOption<T>
    {
        private IEnumerable<T> Content { get; }

        private Option(IEnumerable<T> content)
        {
            Content = content;
        }

        public static Option<T> Some(T value) // static factory method 
        => new Option<T>(new[] { value });

        public static Option<T> None
        => new Option<T>(new T[0]);

        public IEnumerator<T> GetEnumerator() => Content.GetEnumerator();

        //IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public interface IOption<T>
    {
    }
}
