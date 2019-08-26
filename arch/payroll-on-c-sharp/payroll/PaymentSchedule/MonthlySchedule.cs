using System;
using payroll.PaySchedule;

namespace payroll
{
    public class MonthlySchedule : IPaymentSchedule
    {
        public bool IsPayDate(DateTime payDate)
        {
            throw new NotImplementedException();
        }
    }
}