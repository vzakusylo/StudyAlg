using System;

namespace Payroll.PaymentSchedule
{
    public class WeeklySchedule : IPaymentSchedule
    {
        public bool IsPayDate(DateTime payDate)
        {
            return payDate.DayOfWeek == DayOfWeek.Friday;
        }
    }
}