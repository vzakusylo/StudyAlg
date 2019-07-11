using System;

namespace _112_object_sort
{
    public class Person
    {
        public string LastName { get; private set; }
        public string FirstName { get; private set; }
        public int Age { get; private set; }

        public Person(string lastName, string firstName, int age)
        {
            LastName = lastName;
            FirstName = firstName;
            Age = age;
        }

        public void DisplayPerson()
        {
            Console.WriteLine($" Last name : {LastName} First name : {FirstName} Age : {Age}");
        }
    }
}