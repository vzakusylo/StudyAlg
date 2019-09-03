using System;
using System.Collections.Generic;
using Xunit;

namespace Visitor
{
    public class Program
    {
        [Fact]
        public void Run()
        {
            Employees employees = new Employees();
            employees.Attach(new Clerk());
            employees.Attach(new Director());
            employees.Attach(new President());

            employees.Accept(new IncomeVisitor());
            employees.Accept(new VacationVisitor());
        }
    }

    class Client
    {

    }

    abstract class Element
    {
        public abstract void Accept(IVisitor visitor);
    }

    class Employee : Element
    {
        protected Employee(string name, double income, int vacationDays)
        {
            Name = name;
            Income = income;
            VacationDays = vacationDays;
        }

        public double Income { get; set; }
        public string Name { get; set; }
        public int VacationDays { get; set; }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    interface IVisitor
    {
        void Visit(Element element);
    }

    class IncomeVisitor : IVisitor
    {
        public void Visit(Element element)
        {
            Employee employee = element as Employee;

            employee.Income *= 1.10;
            Console.WriteLine("{0} {1}'s new income: {2:C}", employee.GetType().Name,
                employee.Name,
                employee.Income);

        }
    }

    class VacationVisitor : IVisitor
    {
        public void Visit(Element element)
        {
            Employee employee = element as Employee;

            employee.VacationDays += 3;
            Console.WriteLine("{0} {1}'s new vacation days: {2}",
                employee.GetType().Name,
                employee.Name,
                employee.VacationDays);
        }
    }

    class Employees
    {
        private List<Employee> _employees = new List<Employee>();

        public void Attach(Employee employee)
        {
            _employees.Add(employee);
        }

        public void Detach(Employee employee)
        {
            _employees.Remove(employee);
        }

        public void Accept(IVisitor visitor)
        {
            foreach (var employee in _employees)
            {
                employee.Accept(visitor);
            }
            Console.WriteLine();
        }
    }

    class Clerk : Employee
    {
        public Clerk() : base("Harry", 25000.0, 14)
        {

        }
    }

    class Director : Employee
    {
        public Director() : base("Edward", 35000, 16) { }
    }

    class President : Employee
    {
        public President() : base("Damond", 45000, 21)
        { }
    }
}