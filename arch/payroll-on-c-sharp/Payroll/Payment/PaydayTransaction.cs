using System;
using System.Collections;
using System.Threading.Tasks;

namespace payroll.Payment
{
    public class PaydayTransaction : ITransaction
    {
        public DateTime PayDate { get; }
        public Hashtable Paychecks = new Hashtable();

        public PaydayTransaction(in DateTime payDate)
        {
            PayDate = payDate;
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
            return (Paycheck)Paychecks[empId];
        }
    }
}
