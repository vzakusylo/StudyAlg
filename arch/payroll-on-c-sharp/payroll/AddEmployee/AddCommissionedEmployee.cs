using System;
using payroll.SalariedClassification;

namespace payroll.AddEmployee
{
    public class AddCommissionedEmployee : AddEmployeeTransaction
    {
        public int BaseRate { get; }
        public double CommissionRate { get; }

        public AddCommissionedEmployee(int empId, string name, string home, int baseRate, double commissionRate) : base(empId, name, home)
        {
            BaseRate = baseRate;
            CommissionRate = commissionRate;
        }

        protected override PaymentClassification MakeClassification()
        {
            return new CommissionedClassification(BaseRate, CommissionRate);
        }

        protected override IPaymentSchedule MakeSchedule()
        {
            return new MonthlySchedule();
        }
    }
}