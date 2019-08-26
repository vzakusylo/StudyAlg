using System;
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
        public IPaymentSchedule Schedule { get; set; }
        public PaymentMethod Method { get; set; }
        public IAffiliation Affiliation { get; set; }

        public bool IsPayDate(DateTime payDate)
        {
            return Schedule.IsPayDate(payDate);
        }

        public void Payday(Paycheck pc)
        {
            double grossPay = Classification.CalculatePay(pc);
            double deductions = Affiliation.CalculateDeductions(pc);
            double netPay = grossPay - deductions;
            pc.GrossPay = grossPay;
            pc.Deduction = deductions;
            pc.NetPay = netPay;
            Method.Pay(pc);
        }
    }
}