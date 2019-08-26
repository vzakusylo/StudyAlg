using System;

namespace payroll
{
    public class Paycheck
    {
        public Paycheck(DateTime payDate)
        {
            PayDate = payDate;
        }

        public DateTime PayDate { get; set; }
        public double GrossPay { get; set; }
        public double Deduction { get; set; }
        public double NetPay { get; set; }

        public string GetField(string disposition)
        {
            throw new NotImplementedException();
        }
    }
}