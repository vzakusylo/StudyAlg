using System;
using payroll;

namespace Payroll.PaymentSchedule
{
    public class BiWeeklySchedule : IPaymentSchedule
    {
        public bool IsPayDate(DateTime payDate)
        {
            throw new NotImplementedException();
        }
    }
}