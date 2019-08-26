using System.Threading.Tasks;
using payroll.SalariedClassification;

namespace payroll.ChangeEmployee
{
    public abstract class ChangeClassificationTransaction : ChangeEmployeeTransaction
    {
        protected ChangeClassificationTransaction(int empId) : base(empId)
        {
        }

        protected override Task ChangeAsync(Employee e)
        {
            e.Classification = Classification;
            e.Schedule = Schedule;
            return Task.CompletedTask;
        }

        protected abstract PaymentClassification Classification { get; }
        protected abstract IPaymentSchedule Schedule { get; }
    }
}