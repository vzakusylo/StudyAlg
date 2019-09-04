using System;
using System.Collections;

namespace payroll
{
    public class Paycheck
    {
        private Hashtable _fields = new Hashtable();
        public Paycheck(DateTime startDate, DateTime payDate)
        {
            PayPeriodEndDate = payDate;
            PayPeriodStartDate = startDate;
        }
        
        public double GrossPay { get; set; }
        public double Deduction { get; set; }
        public double NetPay { get; set; }
        public DateTime PayPeriodEndDate { get; set; }
        public DateTime PayPeriodStartDate { get; set; }

        public string GetField(string fieldName)
        {
            return _fields[fieldName] as string;
        }

        public void SetField(string disposition, string hold)
        {
            _fields[disposition] = hold;
        }
    }
}