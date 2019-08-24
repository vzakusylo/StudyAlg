using System;
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

        protected override PaymentClassification MakeClassification()
        {
            return new HourlyClassification(HourlyRate);
        }

        protected override PaymentSchedule MakeSchedule()
        {
            throw new NotImplementedException();
        }
    }
}