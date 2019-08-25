using payroll.SalariedClassification;

namespace payroll.AddEmployee
{
    public abstract class ChangeClassificationTransaction : ChangeEmployeeTransaction
    {
        protected ChangeClassificationTransaction(int empId) : base(empId)
        {
        }

        protected override void Change(Employee e)
        {
            e.Classification = Classification;
            e.Schedule = Schedule;
        }

        protected abstract PaymentClassification Classification { get; }
        protected abstract PaymentSchedule Schedule { get; }
    }
}