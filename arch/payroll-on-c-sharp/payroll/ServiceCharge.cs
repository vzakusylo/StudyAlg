using System;

namespace payroll
{
    public class ServiceCharge
    {
        public DateTime Time { get; }

        public double Amount { get; set; }

        public ServiceCharge(DateTime time, double amount)
        {
            Time = time;
            Amount = amount;
        }
    }
}