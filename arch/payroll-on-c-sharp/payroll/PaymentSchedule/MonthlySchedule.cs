using System;
using Payroll.PaymentSchedule;

namespace payroll.PaymentSchedule
{
    public class MonthlySchedule : IPaymentSchedule
    {
        public bool IsPayDate(DateTime payDate)
        {
            return IsLastDayOfMonth(payDate);
        }

        public DateTime GetPayPeriodStartDate(DateTime payDate)
        {
            int days = 0;
            while (payDate.AddDays(days-1).Month == payDate.Month)
            {
                days--;
            }

            return payDate.AddDays(days);
        }

        public override string ToString()
        {
            return "monthly";
        }

        private bool IsLastDayOfMonth(DateTime date)
        {
            int m1 = date.Month;
            int m2 = date.AddDays(1).Month;
            return (m1 != m2);
        }
    }
}