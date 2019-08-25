using payroll.AddEmployee;
using payroll.SalariedClassification;

namespace payroll.ChangeEmployee
{
    public class ChangeSalariedTransaction : ChangeClassificationTransaction
    {
        public ChangeSalariedTransaction(int empId) : base(empId)
        {
        }

        protected override PaymentClassification Classification { get; }
        protected override PaymentSchedule Schedule { get; }
    }
}