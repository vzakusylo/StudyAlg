using System;
using System.Threading.Tasks;

namespace payroll.SalariedClassification
{
    public class HourlyClassification : PaymentClassification
    {
        public double HourlyRate { get; }

        public HourlyClassification(double hourlyRate)
        {
            HourlyRate = hourlyRate;
        }

        public override double CalculatePay(Paycheck paycheck)
        {
            throw new System.NotImplementedException();
        }

        public TimeCard GetTimeCard(DateTime dateTime)
        {
            throw new System.NotImplementedException();
        }

        public Task AddTimeCardAsync(TimeCard timeCard)
        {
            throw new NotImplementedException();
        }
    }
}