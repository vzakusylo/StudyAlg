using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace storing_shirts_an_immutableArray_concur_tryremove
{
    // https://app.pluralsight.com/course-player?clipId=03e12ec9-d9c3-4264-a93e-c4cdb79a1a4a


    //Sahil: Sold Docker
    //Chuck: Sold kcdc
    //Sahil: Sold IGeek
    //Kim: Sold QCon
    //Kim: Sold Pluralsight
    //Chuck: Can't show shirt to customer - already sold
    //Chuck: Sold Big Data
    //Kim: Sold I Love Node
    //Chuck: Sold Pluralsight Live
    //Chuck: All shirts sold
    //Kim: Can't sell Pluralsight Live: Already sold
    //Kim: All shirts sold
    //Sahil: Can't show shirt to customer - already sold
    //Sahil: All shirts sold

    //0 items left in stock:

    // 

    [TestClass]
    public class Solution
    {
        [TestMethod]
        public async Task Run()
        {
            StockController ctr = new StockController(TShirtProvider.AllShirts);
            TimeSpan workDay = new TimeSpan(0, 0, 0, 0, 500);

            Task task1 = Task.Run(() => new SalesPerson("Kim").Work(workDay, ctr));
            Task task2 = Task.Run(() => new SalesPerson("Sahil").Work(workDay, ctr));
            Task task3 = Task.Run(() => new SalesPerson("Chuck").Work(workDay, ctr));

            Task.WaitAll(task1, task2, task3);

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
            var result = ctr.SelectRandomShirt();
            TShirt shirt = result.Shirt;

            if (result.Result == SelectResult.NoStockLeft)
                return (false, "All shirts sold");
            else if (result.Result == SelectResult.ChosenShirtSold)
                return (true, "Can't show shirt to customer - already sold");

            Thread.Sleep(Rnd.NextInt(30));

            // customer chooses to buy with only 20% probability
            if (Rnd.TrueWithProb(0.2))
            {
                bool sold = ctr.Sell(shirt.Code);
                if (sold)
                    return (true, $"Sold {shirt.Name}");
                else
                    return (true, $"Can't sell {shirt.Name}: Already sold");
            }

            return (true, null);
        }
    }

    public enum SelectResult { Success, NoStockLeft, ChosenShirtSold}

    public class StockController
    {
        private ConcurrentDictionary<string, TShirt> _stock;

        public StockController(IEnumerable<TShirt> shirts)
        {
            this._stock = new ConcurrentDictionary<string, TShirt>
                (shirts.ToDictionary(x => x.Code));
        }

        public bool Sell(string code)
        {
            var result = _stock.TryRemove(code, out TShirt shirtRemoved);
            return result;

        }

        public (SelectResult Result, TShirt Shirt) SelectRandomShirt()
        {
            var keys = _stock.Keys.ToList();
            if (keys.Count == 0)
            {
                return (SelectResult.NoStockLeft, null);
            }
            Thread.Sleep(Rnd.NextInt(10));
            string selectedCode = keys[Rnd.NextInt(keys.Count)];
            var found = _stock.TryGetValue(selectedCode, out TShirt shirt);
            if (found)            
                return (SelectResult.Success, shirt);
            else            
                return (SelectResult.ChosenShirtSold, null);
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
