using System.Threading.Tasks;

namespace payroll.DeleteEmployee
{
    public class DeleteEmployeeTransaction : ITransaction
    {
        public int EmpId { get; }

        public DeleteEmployeeTransaction(int empId)
        {
            EmpId = empId;
        }

        public Task ExecuteAsync()
        {
            return PayrollDatabase.DeleteEmployeeAsync(EmpId);
        }
    }
}