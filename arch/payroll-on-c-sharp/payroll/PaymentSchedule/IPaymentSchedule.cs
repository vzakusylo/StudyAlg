using System;

namespace Payroll.PaymentSchedule
{
    public interface IPaymentSchedule
    {
        bool IsPayDate(DateTime payDate);

        DateTime GetPayPeriodStartDate(DateTime payDate);
    }
}