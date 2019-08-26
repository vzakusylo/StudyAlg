using System;
using Payroll.Classification;
using payroll.SalariedClassification;

namespace payroll.AddEmployee
{
    public class AddHourlyEmployee : AddEmployeeTransaction
    {
        public double HourlyRate { get; }

        public AddHourlyEmployee(int empId, string name, string address, double hourlyRate) : base(empId, name, address)
        {
            HourlyRate = hourlyRate;
        }

        protected override Classification.PaymentClassification MakeClassification()
        {
            return new HourlyClassification(HourlyRate);
        }

        protected override IPaymentSchedule MakeSchedule()
        {
            return new BiWeeklySchedule();
        }
    }
}