using System;
using System.Collections.Generic;
using System.Text;

namespace _03_moq_collections
{
    public class MyArray : IMyArray
    {
        public int Lenght => Data.Length;
        private int[] Data { get; set; }

        public MyArray()
        {
            Data = new int[0];
        }

        public void Append(int value)
        {
            int[] data = Data;
            Array.Resize(ref data, data.Length + 1);
            data[data.Length - 1] = value;
            Data = data;
        }

        public int GetElementAt(int index) => Data[index];
    }
}
