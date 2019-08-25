using payroll.SalariedClassification;

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

        protected override PaymentClassification MakeClassification()
        {
            return new SalariedClassification.SalariedClassification(Salary);
        }

        protected override PaymentSchedule MakeSchedule()
        {
            return new MonthlySchedule();
        }
    }
}