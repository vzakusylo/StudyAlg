using System;

namespace payroll
{
    public interface IPaymentSchedule
    {
        bool IsPayDate(DateTime payDate);

    }
}