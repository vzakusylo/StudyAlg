using System.Threading.Tasks;

namespace payroll.ChangeEmployee
{
    public class ChangeNameTransaction : ChangeEmployeeTransaction
    {
        public string Name { get; }

        public ChangeNameTransaction(int empId, string name) : base(empId)
        {
            Name = name;
        }
        
        protected override Task ChangeAsync(Employee e)
        {
            e.Name = Name;
            return Task.CompletedTask;
        }
    }
}