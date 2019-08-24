using System.Threading.Tasks;
using payroll.SalariedClassification;

namespace payroll.AddEmployee
{
    public abstract class AddEmployeeTransaction : ITransaction
    {
        public int EmpId { get; }
        public string Name { get; }
        public string Address { get; }

        public async Task ExecuteAsync()
        {
            PaymentClassification pc = MakeClassification();
            PaymentSchedule ps = MakeSchedule();
            PaymentMethod pm = new HoldMethod();
            Employee e = new Employee(EmpId, Name, Address);
            e.Classification = pc;
            e.Schedule = ps;
            e.Method = pm;

            await PayrollDatabase.AddEmployeeAsync(EmpId, e);
        }

        public AddEmployeeTransaction(int empId, string name, string address)
        {
            EmpId = empId;
            Name = name;
            Address = address;
        }

        protected abstract PaymentClassification MakeClassification();

        protected abstract PaymentSchedule MakeSchedule();
    }
}