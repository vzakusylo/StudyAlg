using System;
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Heap
{
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void Main()
        {
            int value, value2;
            Heap theHeap = new Heap(31);
            bool success;

            theHeap.Insert(70);
            theHeap.Insert(40);
            theHeap.Insert(50);
            theHeap.Insert(20);
            theHeap.Insert(60);
            theHeap.Insert(100);
            theHeap.Insert(80);
            theHeap.Insert(30);
            theHeap.Insert(10);
            theHeap.Insert(90);

            var choise = "s" ; // s-enter, i-insert, r-remove, c-change priority
            switch (choise)
            {
                case "s":
                    theHeap.Display();
                    break;
                case "i":
                    var newValue = (new Random()).Next(1, 100);
                    success = theHeap.Insert(newValue);
                    if (!success)
                    {
                        Console.WriteLine("Can't insert: the heap is full");
                        break;
                    }
                    break;
                case "r":
                    if(!theHeap.IsEmpty())
                    {
                        theHeap.Remove();
                    }
                    else
                    {
                        Console.WriteLine("Can't remove: the heap full");
                    }
                    break;
                case "c":
                    value = (new Random()).Next(1, 5);
                    value2 = (new Random()).Next(2, 3);
                    success = theHeap.Change(value, value2);
                    if (!success)
                    {
                        Console.WriteLine("Invalid index");
                    }
                    break;
                default:
                    break;
            }
        }
    }

    public class Node
    {
        public int Key { get; set; }

        public Node(int key)
        {
            Key = key;
        }
    }

    public class Heap
    {
        private int v;
        private Node[] heapArray;
        private int maxSize;
        private int currentSize;

        public Heap(int mx)
        {
            maxSize = mx;
            currentSize = 0;
            heapArray = new Node[maxSize];
        }

        internal bool Change(int index, int newValue)
        {
            if (index < 0 || index>=currentSize)            
                return false;            

            int oldValue = heapArray[index].Key;
            heapArray[index].Key = newValue;

            if (oldValue < newValue)            
                TrickleUp(index);            
            else            
                TrickleDown(index);
            
            return true;
        }

        internal void Display()
        {
            Console.Write("heap array: ");
            for (int m = 0; m < currentSize; m++)
            {
                if (heapArray[m] != null)
                {
                    Console.Write(heapArray[m].Key + " ");
                }
                else
                {
                    Console.Write("--");
                }
            }
            Console.WriteLine();

            int nBlanks = 32;
            int itemsPerRow = 1;
            int column = 0;
            int j = 0;
            string dots = "................................";
            Console.WriteLine(dots + dots);

            while (currentSize > 0)
            {
                if (column == 0)
                {
                    for (int k = 0; k < nBlanks; k++)
                    {
                        Console.Write(" ");
                    }
                }
                Console.Write(heapArray[j].Key);
                if (++j == currentSize)
                {
                    break;
                }

                if (++column == itemsPerRow)
                {
                    nBlanks /= 2;
                    itemsPerRow *= 2;
                    column = 0;
                    Console.WriteLine();
                }
                else
                {
                    for (int k = 0; k < nBlanks*2-2; k++)
                    {
                        Console.Write(" ");
                    }
                }
            }
            Console.WriteLine("\n" + dots + dots);
        }

        internal bool Insert(int key)
        {
            if (currentSize == maxSize)       // если массив заполнен
            {
                return false;                 // возращаем признак неудачи
            }
            Node newNode = new Node(key);     // создание нового узла
            heapArray[currentSize] = newNode; // размещение в конце массива
            TrickleUp(currentSize++);         // смещение вверх
            return true;
        }

        private void TrickleUp(int index) //смещение вверх
        {
            int parent = (index - 1) / 2;
            Node bottom = heapArray[index];
            while (index > 0 && heapArray[parent].Key < bottom.Key)
            {
                heapArray[index] = heapArray[parent]; // узел перемещается вниз
                index = parent;                       // index перемещается вверх
                parent = (parent - 1) / 2;
            }
            heapArray[index] = bottom;
        }

        internal bool IsEmpty()
        {
            return currentSize == 0;
        }

        internal Node Remove()                        // удаление элемента с максимальным ключем
        {
            Node root = heapArray[0];                 // сохранение корня
            heapArray[0] = heapArray[--currentSize];  // корень <- последний узел
            TrickleDown(0);                           // корневой узел смещается вниз
            return root;                              // метод возращает удаленный узел
        }

        private void TrickleDown(int index)
        {
            int largerChild;
            Node top = heapArray[index];                                   //сохранение корня
            while (index < currentSize/2)                                  // пока у узла имеется хотя бы один потомок
            {
                int leftChild = 2 * index + 1;
                int rightChild = leftChild + 1;
                                                                           // определение большего потомка
                if (rightChild < currentSize &&                            // правый потомок существует ?
                    heapArray[leftChild].Key < heapArray[rightChild].Key)
                {
                    largerChild = rightChild;
                }
                else
                {
                    largerChild = leftChild;
                }

                if (top.Key >= heapArray[largerChild].Key)
                {
                    break;
                }
                                                                            // потомок сдвигается вверх
                heapArray[index] = heapArray[largerChild];
                index = largerChild;                                        // переход вниз
            }
            heapArray[index] = top;                                         // index <- корень
        }
    }
}
