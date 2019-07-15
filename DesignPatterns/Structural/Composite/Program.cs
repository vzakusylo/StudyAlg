using System;
using System.Collections.Generic;

namespace Composite
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee ricky = new Employee { Id = 1, Name = "ricky", Rating = 3 };
            Employee mike = new Employee { Id = 2, Name = "mike", Rating = 4 };
            Employee maryann = new Employee { Id = 3, Name = "maryann", Rating = 5 };
            Employee ginger = new Employee { Id = 4, Name = "ginger", Rating = 3 };
            Employee olive = new Employee { Id = 5, Name = "olive", Rating = 4 };
            Employee candy = new Employee { Id = 5, Name = "candy", Rating = 5 };

            Supervisor ronny = new Supervisor {Id = 7, Name = "ronny", Rating = 3};
            Supervisor bob = new Supervisor { Id = 8, Name = "bob", Rating = 3 };

            ronny.AddSubordinate(ricky);
            ronny.AddSubordinate(mike);
            ronny.AddSubordinate(maryann);

            bob.AddSubordinate(ginger);
            bob.AddSubordinate(olive);
            bob.AddSubordinate(candy);

            Console.WriteLine("Employee can see their Performance ");
            Console.WriteLine("summary");

            ricky.PerformanceSummary();

            Console.WriteLine("Supervisor can also see their subordinates performance summary -----");
            ronny.PerformanceSummary();

            Console.WriteLine("\nSubordinate Performance Record:");
            foreach (var employee in ronny.ListSubordinates)
            {
                employee.PerformanceSummary();
            }

        }

        //IComponent interface
        public interface IEmployee
        {
            int Id { get; set; }
            string Name { get; set; }
            int Rating { get; set; }
            void PerformanceSummary();
        }

        public class Employee : IEmployee
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Rating { get; set; }
            public void PerformanceSummary()
            {
                Console.WriteLine($"Performance summary of employee: {Name} is {Rating} out of 5");
            }
        }

        public class Supervisor : IEmployee
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Rating { get; set; }

            public List<IEmployee> ListSubordinates = new List<IEmployee>();

            public void PerformanceSummary()
            {
                Console.WriteLine($"Performance summary of supervisor: {Name} is {Rating} out of 5");
            }

            public void AddSubordinate(IEmployee employee)
            {
                ListSubordinates.Add(employee);
            }
        }
    }
}
