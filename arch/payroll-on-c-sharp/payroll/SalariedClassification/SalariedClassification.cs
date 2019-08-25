namespace payroll.SalariedClassification
{
    public class SalariedClassification : PaymentClassification
    {
        public double Salary { get; set; }

        public SalariedClassification(double salary)
        {
            Salary = salary;
        }

        public override double CalculatePay(Paycheck paycheck)
        {
            return Salary;
        }
    }
}