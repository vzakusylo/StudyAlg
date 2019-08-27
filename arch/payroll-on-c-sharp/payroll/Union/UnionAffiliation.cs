using System;
using System.Collections;

namespace payroll.Union
{
    public class UnionAffiliation : IAffiliation
    {
        private readonly Hashtable _serviceCharges = new Hashtable();

        public UnionAffiliation() { }

        public UnionAffiliation(int memberId, double dues)
        {
            MemberId = memberId;
            Dues = dues;
        }

        public double Dues { get; }
        public int MemberId { get; }

        public ServiceCharge GetServiceCharge(DateTime dateTime)
        {
            return (ServiceCharge)_serviceCharges[dateTime];
        }

        public void AddServiceCharge(ServiceCharge serviceCharge)
        {
            _serviceCharges.Add(serviceCharge.Time, serviceCharge);
        }

        public double CalculateDeductions(Paycheck pc)
        {
            double totalDues = 0;
            int fridays = NumberOfFridaysInPayPeriod(pc.PayPeriodStartDate, pc.PayPeriodEndDate);
            totalDues = Dues * fridays;
            return totalDues;
        }

        private int NumberOfFridaysInPayPeriod(DateTime pcPayPeriodStartDate, DateTime payPeriodEndDate)
        {
            int fridays = 0;
            for (DateTime day = pcPayPeriodStartDate; day <= payPeriodEndDate; day.AddDays(1))
            {
                if (day.DayOfWeek == DayOfWeek.Friday)
                {
                    fridays++;
                }
            }
            return fridays;
        }
    }
}