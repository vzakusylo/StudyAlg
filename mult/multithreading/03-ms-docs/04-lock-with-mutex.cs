using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace lock_with_mutex_03
{
    //https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/lock-statement
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public async Task Main()
        {
            IAccount account = new AccountUsingLock(1000);
            var tasks = new Task[100];
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Run(() => AccountTest<IAccount>.Update(account));
            }
            await Task.WhenAll(tasks);
            Console.WriteLine($"Account balance using lock {account.GetBalance()}");

            account = new AccountUsingMutex(1000);
            tasks = new Task[100];
            for (int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = Task.Run(() => AccountTest<IAccount>.Update(account));
            }
            await Task.WhenAll(tasks);

            Console.WriteLine($"Account balance using mutex {account.GetBalance()}");
        }        

        public static class AccountTest<T> where T : IAccount
        {
            public static void Update(T account)
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

        public class AccountUsingMutex : IAccount
        {
            private Mutex mutex = new Mutex(); 
            private decimal balance;

            public AccountUsingMutex(decimal initialBalance) => balance = initialBalance;

            public decimal Debit(decimal amount)
            {
                if (amount < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(amount), "The debit amount can't be negative.");
                }
                decimal appliedAmmount = 0;
                mutex.WaitOne();
                if (balance >= amount)
                {
                    balance -= amount;
                    appliedAmmount = amount;
                }
                mutex.ReleaseMutex();            
                   
                return appliedAmmount;
            }

            public void Credit(decimal amount)
            {
                if (amount < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(amount));
                }
                mutex.WaitOne();
                balance += amount;
                mutex.ReleaseMutex();
            }

            public decimal GetBalance()
            {
                mutex.WaitOne();
                decimal actualBalance = balance;
                mutex.ReleaseMutex();
                return actualBalance;
            }
        }
              
        public class AccountUsingLock : IAccount
        {
            private readonly object balanceLock = new object();
            private decimal balance;

            public AccountUsingLock(decimal initialBalance) => balance = initialBalance;

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

        public interface IAccount
        {
            void Credit(decimal amount);
            decimal Debit(decimal amount);
            decimal GetBalance();
        }
    }
}
