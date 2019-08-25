using payroll.AddEmployee;
using payroll.SalariedClassification;

namespace payroll.ChangeEmployee
{
    public class ChangeCommissionedTransaction : ChangeClassificationTransaction
    {
        public ChangeCommissionedTransaction(int empId) : base(empId)
        {
        }

        protected override PaymentClassification Classification { get; }
        protected override PaymentSchedule Schedule { get; }
    }
}