using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace lock_statement
{
    //https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/lock-statement
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public async Task Main()
        {
            var account = new Account(1000);
            var tasks = new Task[100];
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Run(() => AccountTest.Update(account));
            }
            await Task.WhenAll(tasks);
            Console.WriteLine($"Account balance {account.GetBalance()}");
            Console.WriteLine($"The same acoount balance {(await AccountTest.Process()).GetBalance()}");
        }
    }

    public static class AccountTest
    {
        public async static Task<Account> Process()
        {
            var account = new Account(1000);
            var tasks = new Task[100];
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Run(() => AccountTest.Update(account));
            }
            await Task.WhenAll(tasks);

            return account;
        }

        public static void Update(Account account)
        {
            decimal[] amounts = { 0, 2, -3, 6, -2, -1, 8, -5, 11, -6 };
            foreach (var amount in amounts)
            {
                if (amount >= 0)
                {
                    account.Credit(amount);
                }
                else
                {
                    account.Debit(Math.Abs(amount));
                }
            }
        }
    }


    public class Account
    {
        private readonly object balanceLock = new object();
        private decimal balance;

        public Account(decimal initialBalance) => balance = initialBalance;

        public decimal Debit(decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "The debit amount can't be negative.");
            }
            decimal appliedAmmount = 0;
            lock (balanceLock)
            {
                if (balance >= amount)
                {
                    balance -= amount;
                    appliedAmmount = amount;
                }
                return appliedAmmount;
            }
        }

        public void Credit(decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount));
            }

            lock (balanceLock)
            {
                balance += amount;
            }
        }

        public decimal GetBalance()
        {
            lock (balanceLock)
            {
                return balance;
            }
        }
    }
}
