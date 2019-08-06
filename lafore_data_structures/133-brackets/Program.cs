using System;
using System.Threading.Tasks.Dataflow;

namespace _133_brackets
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            while (true)
            {
                Console.WriteLine("Enter string containing delimiters:");
                var input = Console.ReadLine();
                BracketChecker theChecker = new BracketChecker(input);
                theChecker.Check();
            }
        }
    }
}
