using payroll.SalariedClassification;

namespace payroll
{
    public class Employee
    {
        public int EmpId { get; }
        public string Name { get; set; }
        public string Address { get; }

        public Employee(int empId, string name, string address)
        {
            EmpId = empId;
            Name = name;
            Address = address;
        }
        public PaymentClassification Classification { get; set; }
        public PaymentSchedule Schedule { get; set; }
        public PaymentMethod Method { get; set; }
        public IAffiliation Affiliation { get; set; }
    }
}