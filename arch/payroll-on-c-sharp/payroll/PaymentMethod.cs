namespace payroll
{
    public interface IPaymentMethod
    {
        void Pay(Paycheck paycheck);
    }
}