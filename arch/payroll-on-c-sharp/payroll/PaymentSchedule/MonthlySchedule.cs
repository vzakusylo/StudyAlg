using System;

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