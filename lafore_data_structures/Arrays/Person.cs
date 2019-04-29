using System;

namespace Arrays
{
    public class DataArray
    {
        private Person[] a;
        private int nElems;

        public DataArray(int max)
        {
            a = new Person[max];
            nElems = 0;
        }
    }
    
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

        public void Display()
        {
            Console.WriteLine($"Last name {LastName} First Name {FirstName} Age {Age}");
        }
        
    }
}