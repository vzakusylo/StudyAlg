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
            Classification.PaymentClassification pc = MakeClassification();
            IPaymentSchedule ps = MakeSchedule();
            IPaymentMethod pm = new HoldMethod();
            Employee e = new Employee(EmpId, Name, Address);
            e.Classification = pc;
            e.Schedule = ps;
            e.Method = pm;

            await PayrollDatabase.AddEmployeeAsync(EmpId, e);
        }

        protected AddEmployeeTransaction(int empId, string name, string address)
        {
            EmpId = empId;
            Name = name;
            Address = address;
        }

        protected abstract Classification.PaymentClassification MakeClassification();

        protected abstract IPaymentSchedule MakeSchedule();
    }
}