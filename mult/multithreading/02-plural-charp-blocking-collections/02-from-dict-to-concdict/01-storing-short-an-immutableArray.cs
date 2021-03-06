using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace storing_shirts_an_immutableArray
{
    // https://app.pluralsight.com/course-player?clipId=28b7cdda-74c4-4ba7-9905-976725ef1800
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public async Task Run()
        {
            StockController ctr = new StockController(TShirtProvider.AllShirts);
            TimeSpan workDay = new TimeSpan(0, 0, 0, 0, 500);

            new SalesPerson("Kim").Work(workDay, ctr);

            ctr.DisplayStock();
        }
    }

    public class SalesPerson
    {
        public string Name { get; }

        public SalesPerson(string name)
        {
            this.Name = name;
        }

        public void Work(TimeSpan workDay, StockController ctr)
        {
            DateTime start = DateTime.Now;
            while (DateTime.Now - start < workDay)
            {
                var result = ServeCustomer(ctr);

                if (result.Status != null)                
                    Console.WriteLine($"{Name}: {result.Status}");
                if (!result.ShirtInStock)
                    break;
            }
        }

        public (bool ShirtInStock, string Status) ServeCustomer(StockController ctr)
        {
            TShirt shirt = ctr.SelectRandomShirt();
            if (shirt == null)            
                return (false, "All shirts sold");

            Thread.Sleep(Rnd.NextInt(30));

            // customer chooses to buy with only 20% probability
            if (Rnd.TrueWithProb(0.2))
            {
                ctr.Sell(shirt.Code);
                return (true, $"Sold {shirt.Name}");
            }

            return (true, null);
            
        }
    }

    public class StockController
    {
        private Dictionary<string, TShirt> _stock;

        public StockController(IEnumerable<TShirt> allShirts)
        {
            this._stock = allShirts.ToDictionary(x=>x.Code);
        }

        public void Sell(string code)
        {
            _stock.Remove(code);
        }

        public TShirt SelectRandomShirt()
        {
            var keys = _stock.Keys.ToList();
            if (keys.Count == 0)
            {
                return null;
            }
            Thread.Sleep(Rnd.NextInt(10));
            string selectedCode = keys[Rnd.NextInt(keys.Count)];
            return _stock[selectedCode];
        }

        public void DisplayStock()
        {
            Console.WriteLine($"\r\n{_stock.Count} items left in stock:");
            foreach (var shirt in _stock.Values)            
                Console.WriteLine(shirt);
        }
    }

    public static class TShirtProvider
    {
        public static ImmutableArray<TShirt> AllShirts { get; } = ImmutableArray.Create(
            new TShirt("igeek", "IGeek", 500),
            new TShirt("bigdata", "Big Data", 500),
            new TShirt("ilovenode", "I Love Node", 500),
            new TShirt("kcdc", "kcdc ", 500),
            new TShirt("docker", "Docker", 500),
            new TShirt("qconf", "QCon", 500),
            new TShirt("ps", "Pluralsight", 60000),
            new TShirt("pslive", "Pluralsight Live", 500)
        );
    }

    public class TShirt
    {
        public string Code { get; }
        public string Name { get; }
        public int PricePence { get; }

        public TShirt(string code, string name, int pricePence)
        {
            Code = code;
            Name = name;
            PricePence = pricePence;
        }

        public override string ToString()
            => $"{Name} ({DisplayPrice(PricePence)})";

        private string DisplayPrice(int pricePence)
        => $"{pricePence / 100}.{pricePence % 100:00}";
    }

    public class Rnd
    {
        private static Random _generator = new Random();
        public static int NextInt(int max) => _generator.Next(max);
        public static bool TrueWithProb(double probOfTrue)
            => _generator.NextDouble() < probOfTrue;
    }
}
