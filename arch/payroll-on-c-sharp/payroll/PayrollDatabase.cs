using System.Collections;
using System.Threading.Tasks;

namespace payroll
{
    public class PayrollDatabase
    {

        private static Hashtable _hashtable = new Hashtable();

        public static Task AddEmployeeAsync(int id, Employee employee)
        {
            _hashtable.Add(id, employee);
            return Task.CompletedTask;
        }

        public static Task<Employee> GetEmployeeAsync(int employeeId)
        {
            return Task.FromResult((Employee)_hashtable[employeeId]);
        }

        public static Task DeleteEmployeeAsync(int empId)
        {
            _hashtable.Remove(empId);
            return Task.CompletedTask;
        }

        public static Task AddUnionMemberAsync(int memberId, Employee employee)
        {
            _hashtable.Add(memberId, employee);
            return Task.CompletedTask;
        }

        public static Task<Employee> GetUnionMemberAsync(int memberId)
        {
            return Task.FromResult((Employee)_hashtable[memberId]);
        }

        public static void RemoveUnionMember(int memeberId)
        {
            _hashtable.Remove(memeberId);
        }
    }
}