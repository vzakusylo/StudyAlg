using System;
using System.Threading.Tasks;

namespace payroll.ChangeEmployee
{
    public abstract class ChangeEmployeeTransaction : ITransaction
    {
        public int EmpId { get; }

        protected ChangeEmployeeTransaction(int empId)
        {
            EmpId = empId;
        }

        public async Task ExecuteAsync()
        {
            Employee e = await PayrollDatabase.GetEmployeeAsync(EmpId);
            if (e == null)
            {
                throw new InvalidOperationException("Could not find employee");
            }
            await ChangeAsync(e);
        }

        protected abstract Task ChangeAsync(Employee e);
    }
}