using Payroll.PaymentSchedule;

namespace payroll.ChangeEmployee
{
    public class ChangeSalariedTransaction : ChangeClassificationTransaction
    {
        public double Salary { get; }

        public ChangeSalariedTransaction(int empId, double salary) : base(empId)
        {
            Salary = salary;
        }

        protected override Classification.PaymentClassification Classification => new Payroll.Classification.SalariedClassification(Salary);
        protected override IPaymentSchedule Schedule => new BiWeeklySchedule();
    }
}