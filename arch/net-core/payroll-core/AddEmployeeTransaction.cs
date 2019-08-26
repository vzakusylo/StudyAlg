namespace Payroll
{
	public abstract class AddEmployeeTransaction : Transaction
	{
		private readonly int _empid;
		private readonly string _name;
		private readonly string _address;

	    protected AddEmployeeTransaction(int empid, 
			string name, string address, IPayrollDatabase database)
			: base (database)
		{
			_empid = empid;
			_name = name;
			_address = address;
		}

		protected abstract PaymentClassification MakeClassification();
		protected abstract PaymentSchedule MakeSchedule();

		public override void Execute()
		{
			var pc = MakeClassification();
			var ps = MakeSchedule();
			var pm = new HoldMethod();

		    var e = new Employee(_empid, _name, _address)
		    {
		        Classification = pc,
		        Schedule = ps,
		        Method = pm
		    };
		    Database.AddEmployee(e);
		}

		public override string ToString()
		{
			return $"{GetType().Name}  id:{_empid}   name:{_name}   address:{_address}";
		} 
	}
}