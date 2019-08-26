using System;
using System.Collections;

namespace Payroll
{
	public class PaydayTransaction : Transaction
	{
		private readonly DateTime payDate;
		private Hashtable paychecks = new Hashtable();

		public PaydayTransaction(DateTime payDate, IPayrollDatabase database)
			: base (database)
		{
			this.payDate = payDate;
		}

		public override void Execute()
		{
			ArrayList empIds = Database.GetAllEmployeeIds();
			  
			foreach(int empId in empIds)
			{
				Employee employee = Database.GetEmployee(empId);
				if (employee.IsPayDate(payDate)) 
				{
					DateTime startDate = 
						employee.GetPayPeriodStartDate(payDate);
					Paycheck pc = new Paycheck(startDate, payDate);
					paychecks[empId] = pc;
					employee.Payday(pc);
				}
			}
		}

		public Paycheck GetPaycheck(int empId)
		{
			return paychecks[empId] as Paycheck;
		}
	}
}