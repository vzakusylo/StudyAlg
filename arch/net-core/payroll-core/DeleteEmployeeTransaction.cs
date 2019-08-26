namespace Payroll
{
	public class DeleteEmployeeTransaction : Transaction
	{
		private readonly int id;

		public DeleteEmployeeTransaction(int id, IPayrollDatabase database)
			: base (database)
		{
			this.id = id;
		}

		public override void Execute()
		{
			Database.DeleteEmployee(id);
		}
	}
}