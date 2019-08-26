using payroll;
using payroll.Classification;

namespace Payroll.Classification
{
    public class CommissionedClassification : PaymentClassification
    {
        public double BaseRate { get; }
        public double CommissionedRate { get; }

        public CommissionedClassification(double baseRate, double commissionedRate)
        {
            BaseRate = baseRate;
            CommissionedRate = commissionedRate;
        }

        public override double CalculatePay(Paycheck paycheck)
        {
            throw new System.NotImplementedException();
        }
    }
}