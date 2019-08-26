namespace payroll
{
    public interface IAffiliation {
        void AddServiceCharge(ServiceCharge serviceCharge);
        double CalculateDeductions(Paycheck pc);
    }
}