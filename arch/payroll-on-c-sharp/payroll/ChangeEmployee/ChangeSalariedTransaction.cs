using payroll.AddEmployee;
using payroll.SalariedClassification;

namespace payroll.ChangeEmployee
{
    public class ChangeSalariedTransaction : ChangeClassificationTransaction
    {
        public double Salary { get; }

        public ChangeSalariedTransaction(int empId, double salary) : base(empId)
        {
            Salary = salary;
        }

        protected override PaymentClassification Classification => new SalariedClassification.SalariedClassification(Salary);
        protected override PaymentSchedule Schedule => new BiWeeklySchedule();
    }
}