namespace payroll.Union
{
    public class NoAffiliation : IAffiliation {
        public void AddServiceCharge(ServiceCharge serviceCharge)
        {
            throw new System.NotImplementedException();
        }

        public double CalculateDeductions(Paycheck pc)
        {
            return pc.Deduction;
        }
    }
}