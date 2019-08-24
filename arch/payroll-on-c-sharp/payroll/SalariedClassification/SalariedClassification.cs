namespace payroll.SalariedClassification
{
    public class SalariedClassification : PaymentClassification
    {
        public double Salary { get; set; }
        public override double CalculatePay(Paycheck paycheck)
        {
            throw new System.NotImplementedException();
        }
    }
}