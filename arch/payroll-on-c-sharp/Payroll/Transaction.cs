namespace Payroll
{
	public abstract class Transaction
	{
		protected readonly IPayrollDatabase Database;

	    protected Transaction(IPayrollDatabase database)
		{
			Database = database;
		}

		public abstract void Execute();
	}
}