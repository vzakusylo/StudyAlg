using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _112_object_sort
{
    [TestClass]
    public class Template
    {
        [TestMethod]
        public void Main()
        {
            ArrayInObj arr = new ArrayInObj(100);
            arr.Insert("Evans", "Patty", 24);
            arr.Insert("Smith", "Lorraine", 37);
            arr.Insert("Yee", "Tom", 43);
            arr.Insert("Adams", "Henry", 63);
            arr.Insert("Hashimoto", "Sato", 21);
            arr.Insert("Stimson", "Henry", 21);

            Console.WriteLine("Before sorting:");
            arr.Display();
            arr.InsertionSort();

            Console.WriteLine("After sorting");
            arr.Display();
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

        public void DisplayPerson()
        {
            Console.WriteLine($" Last name : {LastName} First name : {FirstName} Age : {Age}");
        }
    }

    class ArrayInObj
    {
        private Person[] _a;
        private int _nElems;

        public ArrayInObj(int max)
        {
            _a = new Person[max];
            _nElems = 0;
        }

        public void Insert(string lastName, string firstName, int age)
        {
            _a[_nElems] = new Person(lastName, firstName, age);
            _nElems++;
        }

        public void Display()
        {
            for (int j = 0; j < _nElems; j++)
            {
                _a[j].DisplayPerson();
            }
        }

        public void InsertionSort()
        {
            int inn, outt;
            for (outt = 1; outt < _nElems; outt++)
            {
                Person temp = _a[outt];
                inn = outt;

                while (inn > 0 &&
                       string.Compare(
                           _a[inn - 1].LastName,
                           temp.LastName,
                           StringComparison.InvariantCultureIgnoreCase) > 0)
                {
                    _a[inn] = _a[inn - 1];
                    --inn;
                }

                _a[inn] = temp;
            }
        }
    }
}
