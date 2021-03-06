using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Page32;

namespace Page60
{

    [TestClass]
    public class Page60
    {
        delegate void StringProcessor(string input);

        [TestMethod]
        public void Main()
        {
            StringProcessor proc1, proc2;
            proc1 = new StringProcessor(StaticMethods.PrintString);
            InstanceMethods im = new InstanceMethods();
            proc2 = new StringProcessor(im.PrintString);

            proc1("hello from static method");
            proc2("Hello from instance method");

            proc1.Invoke("hello from static method via invoke");
            proc2.Invoke("hello from instance method via invoke");
        }

        public void PrintString(string x)
        {
            Console.WriteLine(x);
        }

        public void PrintInteger(int x)
        {
            Console.WriteLine(x);
        }

        public void PrintTwoStrings(string x, string y)
        {
            Console.WriteLine($"{x} {y}");
        }

        public int GetStringLength(string x)
        {
            Console.WriteLine(x);
            return 1;
        }

        void PrintObject(object x)
        {
            Console.WriteLine(x);
        }

    }

    public static class StaticMethods
    {
        public static void PrintString(string x)
        {
            Console.WriteLine(x);
        }
    }

    public class InstanceMethods
    {
        public void PrintString(string x)
        {
            Console.WriteLine(x);
        }
    }
}

namespace Page54
{
    [TestClass]
    public class Page54
    {
        [TestMethod]
        public void Main()
        {
            Console.WriteLine(Reverse("dlrow olleH"));
        }

        static string Reverse(string input)
        {
            char[] chars = input.ToCharArray();
            Array.Reverse(chars);
            var span = new ReadOnlySpan<char>(chars);
            return new string(span);
        }
    }
}

namespace Page42
{

    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void Main()
        {
            List<Product> products = Product.GetSampleProducts();
            List<Supplier> suppliers = Supplier.GetSampleSuppliers();
            var filtered = from p in products
                join s in suppliers on p.SupplierId equals s.SupplierId
                where p.Price > 10
                orderby s.Name, p.Name
                select new {SupplierName = s.Name, ProductName = p.Name};

            foreach (var v in filtered)
            {
                Console.WriteLine("Supplier={0}; Product={1}", v.SupplierName, v.ProductName);
            }
        }
    }

    public class Supplier
    {
        public string Name { get; set; }
        public int SupplierId { get; set; }

        public static List<Supplier> GetSampleSuppliers()
        {
            return new  List<Supplier>()
            {
                new Supplier(){Name="Supplier1", SupplierId = 1},
                new Supplier(){Name="Supplier2", SupplierId = 2}
            };
        }
    }
}


namespace Page32
{
    // C#1
    public class Product
    {
        private string name;
        public string Name
        {
            get { return name; }
        }

        private decimal price;
        public decimal Price
        {
            get { return price; }
        }

        public static List<Product> GetSampleProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    name = "product1",
                    price = 12,
                    SupplierId = 1
                },
                new Product
                {
                    name = "product2",
                    price = 23,
                    SupplierId = 2
                }

            };
        }

        public int SupplierId { get; set; }
    }

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}

