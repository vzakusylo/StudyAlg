using System;

namespace payroll.SalariedClassification
{
    public class TimeCard
    {
        public double Hours { get; set; }
        public DateTime Date { get; set; }

        public TimeCard(DateTime date, double hours)
        {
            Hours = hours;
            Date = date;
        }
    }
}