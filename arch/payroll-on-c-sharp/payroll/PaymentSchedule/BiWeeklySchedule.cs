using System;
using payroll.PaySchedule;

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