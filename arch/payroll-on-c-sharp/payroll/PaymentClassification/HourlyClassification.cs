using System;
using System.Collections;
using System.Threading.Tasks;

namespace payroll.SalariedClassification
{
    public class HourlyClassification : PaymentClassification
    {
        Hashtable timeCards = new Hashtable();
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
            return (TimeCard) timeCards[dateTime];
        }

        public Task AddTimeCardAsync(TimeCard timeCard)
        {
            timeCards.Add(timeCard.Date, timeCard);
            return Task.CompletedTask;
        }
    }
}