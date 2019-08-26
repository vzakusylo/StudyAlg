using System;

namespace payroll
{
    public class BiWeeklySchedule : IPaymentSchedule
    {
        public bool IsPayDate(DateTime payDate)
        {
            throw new NotImplementedException();
        }
    }
}