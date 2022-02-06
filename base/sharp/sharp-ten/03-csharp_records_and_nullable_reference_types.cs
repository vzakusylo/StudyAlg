using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace csharp_records_and_nullable_reference_types
{
    [TestClass]
    public class UnitTest1
    {
        // https://event-driven.io/en/notes_about_csharp_records_and_nullable_reference_types/
        [TestMethod]
        public async  Task TestMethod()
        {
            var anna = Guid.NewGuid();
            var john = Guid.NewGuid();

            var amount = 100;

            var moneyTransfer = new MoneyTransfer(amount, anna, john, "Money laudry", "Do not tell anyone");

            // I can create the derived object using with keyword. It will create a clone of the object with some properties getting new values.
            var wrongMoneyTransfer = moneyTransfer with {Amount = -100};


        }

        public record MoneyTransfer(decimal Amount, Guid FromAccountId, Guid ToAccountId, string Title,
            string? Comment = null)
        {
            public static MoneyTransfer Create(decimal Amount, Guid FromAccountId, Guid ToAccountId, string Title,
                string? Comment = null)
            {
                if (Amount <= 0) throw new ArgumentOutOfRangeException();

                if (FromAccountId == default) throw new ArgumentOutOfRangeException();
                if(ToAccountId == default)throw new ArgumentOutOfRangeException();

                if (Title.Trim().Length == 0) throw new ArgumentOutOfRangeException();

                return new(Amount, FromAccountId, ToAccountId, Title, Comment);
            }
        }
    }

}
