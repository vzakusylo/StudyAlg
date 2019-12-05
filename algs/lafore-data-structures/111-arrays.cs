using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _111_arrays
{
    [TestClass]
    public class Template
    {
        [TestMethod]
        public void Main()
        {
            int maxSize = 8;
            ArraySel arr = new ArraySel(maxSize);
            arr.insert(77);
            arr.insert(99);
            arr.insert(44);
            arr.insert(55);
            arr.insert(22);
            arr.insert(88);

            arr.selectionSort();

            // OrderedArrayDemo();
        }


        private static void OrderedArrayDemo()
        {
            int maxSize = 8;
            OrdArray arr = new OrdArray(maxSize);

            arr.insert(77);
            arr.insert(99);
            arr.insert(44);
            arr.insert(55);
            arr.insert(22);
            arr.insert(88);

            var res = arr.find(55);
            res = arr.find(99);

            arr.delete(00);
            arr.delete(55);
        }
    }

    class ArraySel
    {
        private long[] a;
        private int nElem;

        public ArraySel(int max)
        {
            a = new long[max];
            nElem = 0;
        }

        public void insert(long value)
        {
            a[nElem] = value;
            nElem++;
        }

        public void selectionSort()
        {
            int aut, inn, min;
            for (aut = 0; aut < nElem - 1; aut++)
            {
                min = aut;
                for (inn = aut + 1; inn < nElem; inn++)
                {
                    if (a[inn] < a[min])
                    {
                        min = inn;
                    }
                }
                swap(aut, min);
            }
        }

        private void swap(int aut, int min)
        {
            long temp = a[aut];
            a[aut] = a[min];
            a[min] = temp;
        }
    }


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


    public class OrdArray
    {
        private long[] _a;
        private int _nElements;

        public OrdArray(int max)
        {
            _a = new long[max];
            _nElements = 0;
        }

        public int find(long searchKey)
        {
            int lowerBound = 0;
            int upperBound = _nElements - 1;
            int curin;

            while (true)
            {
                curin = (lowerBound + upperBound) / 2;
                if (_a[curin] == searchKey)
                {
                    return curin;
                }
                else if (lowerBound > upperBound)
                {
                    return _nElements;
                }
                else
                {
                    if (_a[curin] < searchKey)
                    {
                        lowerBound = curin + 1;
                    }
                    else
                    {
                        upperBound = curin - 1;
                    }
                }
            }
        }

        public void insert(int value)
        {
            int j;
            for (j = 0; j < _nElements; j++)
            {
                double current = _a[j];
                if (current > value)
                {
                    break;
                }
            }
            for (int k = _nElements; k > j; k--)
            {
                _a[k] = _a[k - 1];
            }

            _a[j] = value;
            _nElements++;
        }

        public bool delete(long value)
        {
            int j = find(value);
            if (j == _nElements)
            {
                return false;
            }
            else
            {
                for (int k = j; k < _nElements; k++)
                {
                    _a[k] = _a[k + 1];
                }

                _nElements--;
                return true;
            }
        }
    }

}
