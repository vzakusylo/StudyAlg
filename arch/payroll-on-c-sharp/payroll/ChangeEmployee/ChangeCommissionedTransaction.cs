using Payroll.Classification;
using payroll.PaymentSchedule;

namespace payroll.ChangeEmployee
{
    public class ChangeCommissionedTransaction : ChangeClassificationTransaction
    {
        public double BaseRate { get; }
        public double CommissionRate { get; }
        
        public ChangeCommissionedTransaction(int empId, double baseRate, double commissionRate) : base(empId)
        {
            
            BaseRate = baseRate;
            CommissionRate = commissionRate;
        }

        protected override Classification.PaymentClassification Classification => new CommissionedClassification(BaseRate,CommissionRate);

        protected override IPaymentSchedule Schedule => new MonthlySchedule();
    }
}