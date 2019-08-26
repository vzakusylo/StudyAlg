using Payroll.Classification;
using Payroll.PaymentSchedule;

namespace payroll.ChangeEmployee
{
    public class ChangeHourlyTransaction : ChangeClassificationTransaction
    {
        public double HourlyRate { get; }
        
        public ChangeHourlyTransaction(int empId, double hourlyRate) : base(empId)
        {
            HourlyRate = hourlyRate;
        }

        protected override Classification.PaymentClassification Classification => new HourlyClassification(HourlyRate);

        protected override IPaymentSchedule Schedule => new WeeklySchedule();
    }
}