using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace payroll.Pyamemt
{
    public class PaydayTransaction : ITransaction
    {
        public DateTime PayDate { get; }
        public Hashtable Paychecks = new Hashtable();

        public PaydayTransaction(in DateTime payDate)
        {
            PayDate = payDate;
            throw new NotImplementedException();
        }

        public async Task ExecuteAsync()
        {
            ArrayList empIds = await PayrollDatabase.GetAllEmployeeIds();
            foreach (int empId in empIds)
            {
                Employee employee = await PayrollDatabase.GetEmployeeAsync(empId);
                if (employee.IsPayDate(PayDate))
                {
                    Paycheck pc = new Paycheck(PayDate);
                    Paychecks[empId] = pc;
                    employee.Payday(pc);
                }
            }
        }

        public Paycheck GetPaycheck(int empId)
        {
            throw new NotImplementedException();
        }
    }
}
