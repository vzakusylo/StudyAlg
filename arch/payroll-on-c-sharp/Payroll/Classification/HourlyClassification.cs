using System;
using System.Collections;
using System.Threading.Tasks;
using payroll;
using payroll.Classification;
using payroll.SalariedClassification;

namespace Payroll.Classification
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
            double totalPay = 0.0;
            foreach (TimeCard timeCard in timeCards.Values)
            {
                if (IsInPayPeriod(timeCard, paycheck.PayDate))
                {
                    totalPay += CalculatePayForTimeCard(timeCard);
                }
            }

            return totalPay;
        }

        private double CalculatePayForTimeCard(TimeCard timeCard)
        {
            double overtimeHours = Math.Max(0.0, timeCard.Hours - 8);
            double normalHours = timeCard.Hours - overtimeHours;
            return HourlyRate * normalHours + HourlyRate * 1.5 * overtimeHours;
        }

        private bool IsInPayPeriod(TimeCard timeCard, DateTime payPeriod)
        {
            DateTime payPeriodEndDate = payPeriod;
            DateTime payPeriodStartDate = payPeriod.AddDays(-5);
            return timeCard.Date <= payPeriodEndDate && timeCard.Date >= payPeriodStartDate;
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