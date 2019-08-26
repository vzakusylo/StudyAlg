using System;

namespace payroll
{
    public class BiWeeklySchedule : IPaymentSchedule
    {
        public bool IsPayDate(DateTime payDate)
        {
            return true;
        }
    }
}