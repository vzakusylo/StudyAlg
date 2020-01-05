using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

namespace Leveraging_Special_Case_Objects_To_Remove_Null_Checks_1
{
    [TestClass]
    public class Client
    {
        [TestMethod]
        public void Main()
        {
            DateTime sellingDate = new DateTime(2019, 12, 9);
            TimeSpan moneyBackSpan = TimeSpan.FromDays(30);
            TimeSpan warrantySpan = TimeSpan.FromDays(365);

            IWarranty moneyBack = new TimeLimitedWarranty(sellingDate, moneyBackSpan);
            IWarranty warranty = new TimeLimitedWarranty(sellingDate, warrantySpan);
           
            //SoldArticle goods = new SoldArticle(moneyBack, warranty);
            //  Offer money back
            //  Offer repair

            SoldArticle goods = new SoldArticle(VoidWarranty.Instance, warranty);
            // Offer repair

            ClaimWarrenty(goods);
        }

        // warning: beware of bool method arguments
        // Their appearance is the indicator of leaving the OO design principles
        private static void ClaimWarrenty(SoldArticle article)  // bool flags
        {
            DateTime now = DateTime.Now;

            if (article.MoneyBackGuarantee.IsValidOn(now))
            {
                Console.WriteLine("Offer money back");
            }
            if (article.ExpressWarranty.IsValidOn(now))
            {
                Console.WriteLine("Offer repair");
            }
        }
    }

    public class SoldArticle
    {
        public IWarranty MoneyBackGuarantee { get; set; }
        public IWarranty ExpressWarranty { get; set; }

        public SoldArticle(IWarranty moneyBack, IWarranty express)
        {
            MoneyBackGuarantee = moneyBack;
            ExpressWarranty = express;
        }
    }

    public interface IWarranty
    {
        bool IsValidOn(DateTime date);
    }

    public class TimeLimitedWarranty : IWarranty
    {
        private DateTime DateIssued { get; set; }
        private TimeSpan Duration { get; set; }

        public TimeLimitedWarranty(DateTime dateIssued, TimeSpan duration)
        {
            DateIssued = dateIssued.Date;
            Duration = TimeSpan.FromDays(duration.Days);
        }

        public bool IsValidOn(DateTime date) =>
            date.Date >= DateIssued &&
            date.Date < DateIssued + Duration;
    }

   
    public class VoidWarranty : IWarranty
    {
        [ThreadStatic] //Indicates that the value of a static field is unique for each thread.
        private static VoidWarranty instance;
        private VoidWarranty() { }
        public static VoidWarranty Instance
        {
            get
            {
                if(instance == null) {
                    instance = new VoidWarranty();
                }
                return instance;
            }
        }
        public bool IsValidOn(DateTime date) => false;
    }
}

namespace Leveraging_Special_Case_Objects_To_Remove_Null_Checks
{
    [TestClass]
    public class Client
    {
        [TestMethod]
        public void Main()
        {
        }

        // warning: beware of bool method arguments
        // Their appearance is the indicator of leaving the OO design principles
        private static void ClaimWarrenty(SoldArticle article, bool isInGoodCondition, bool isBrocken)  // bool flags
        {
            DateTime now = DateTime.Now;

            if (isInGoodCondition && !isBrocken &&
                article.MoneyBackGuarantee != null && article.MoneyBackGuarantee.IsValidOn(now))
            {
                Console.WriteLine("Offer money back");
            }
            if (isBrocken && article.ExpressWarranty != null && article.ExpressWarranty.IsValidOn(now))
            {
                Console.WriteLine("Offer repair");
            }
        }
    }

    public class SoldArticle
    {
        public Warranty MoneyBackGuarantee { get; set; }
        public Warranty ExpressWarranty { get; set; }

        public SoldArticle(Warranty moneyBack, Warranty express)
        {
            MoneyBackGuarantee = moneyBack;
            ExpressWarranty = express;
        }
    }

    public class Warranty
    {
        private DateTime DateIssued { get; set; }
        private TimeSpan Duration { get; set; }

        public Warranty(DateTime dateIssued, TimeSpan duration)
        {
            DateIssued = dateIssued.Date;
            Duration = TimeSpan.FromDays(duration.Days);
        }

        public bool IsValidOn(DateTime date) =>
            date.Date >= DateIssued &&
            date.Date < DateIssued + Duration;
    }
}