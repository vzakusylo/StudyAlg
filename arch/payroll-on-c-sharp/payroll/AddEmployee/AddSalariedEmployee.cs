using payroll.PaymentSchedule;

namespace payroll.AddEmployee
{
    public class AddSalariedEmployee : AddEmployeeTransaction
    {
        public double Salary { get; }

        public AddSalariedEmployee(int empId, string name, string address, double salary)
            :base(empId, name, address)
        {
            Salary = salary;
        }

        protected override Classification.PaymentClassification MakeClassification()
        {
            return new Payroll.Classification.SalariedClassification(Salary);
        }

        protected override IPaymentSchedule MakeSchedule()
        {
            return new MonthlySchedule();
        }
    }
}