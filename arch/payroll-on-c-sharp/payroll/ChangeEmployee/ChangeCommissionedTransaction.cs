using payroll.AddEmployee;
using payroll.SalariedClassification;

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

        protected override PaymentClassification Classification => new CommissionedClassification(BaseRate,CommissionRate);

        protected override PaymentSchedule Schedule => new MonthlySchedule();
    }
}