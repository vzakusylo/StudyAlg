using System;

namespace _112_object_sort
{
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
            for (outt = 1; outt <_nElems; outt++)
            {
                Person temp = _a[outt];
                inn = outt;

                while (inn > 0 && 
                       string.Compare(
                           _a[inn-1].LastName, 
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