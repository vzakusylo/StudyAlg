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

        private bool IsLastDayOfMonth(DateTime date)
        {
            int m1 = date.Month;
            int m2 = date.AddDays(1).Month;
            return (m1 != m2);
        }
    }
}