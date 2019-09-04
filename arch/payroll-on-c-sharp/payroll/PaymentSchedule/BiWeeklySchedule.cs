using System;

namespace Payroll.PaymentSchedule
{
    public class BiWeeklySchedule : IPaymentSchedule
    {
        public bool IsPayDate(DateTime payDate)
        {
            return payDate.DayOfWeek == DayOfWeek.Friday && payDate.Day % 2 == 0;
        }

        public DateTime GetPayPeriodStartDate(DateTime payDate)
        {
            return payDate.AddDays(-13);
        }

        public override string ToString()
        {
            return "bi-weekly";
        }
    }
}