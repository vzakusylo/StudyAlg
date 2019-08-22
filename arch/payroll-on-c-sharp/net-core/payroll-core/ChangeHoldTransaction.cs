namespace Payroll
{
	public class ChangeHoldTransaction : ChangeMethodTransaction
	{
		public ChangeHoldTransaction(int empId, IPayrollDatabase database)
			: base(empId, database)
		{
		}

		protected override PaymentMethod Method
		{
			get { return new HoldMethod(); }
		}

	}
}