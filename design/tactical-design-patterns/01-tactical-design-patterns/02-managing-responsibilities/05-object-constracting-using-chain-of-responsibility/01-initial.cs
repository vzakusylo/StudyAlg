using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace managing_responsibilities
{
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void Main()
        {
            Family family = new Family(new object[] { 
                new Dad(), 
                new Mom(), 
                new Boy(), 
                new Dog() });

            family.WinterBegins();
            family.SummerComes();
        }
    }

    class Family
    {
        private IEnumerable<object> members;

        public Family(IEnumerable<object> members)
        {
            this.members = new List<object>(members); 
        }

        public void WinterBegins()
        {
            Console.WriteLine("Winter just came....");
            foreach (var item in this.members)
            {
                this.GrowHair(item);
                this.GrowBeard(item);
            }
            Console.WriteLine(new string('-', 20));
        }

        public void SummerComes()
        {
            Console.WriteLine("Summer is here...");
            foreach (var item in this.members)
            {
                this.BeHappy(item);               
            }
            Console.WriteLine(new string('-', 20));
        }

        private void BeHappy(object item)
        {
            IEmotional emotional = item as IEmotional; // conditional down casting
            if (emotional != null)
            {
                Console.Write($"{item.GetType().Name}: ");
                emotional.BeHappy();
            }
        }

        private void GrowBeard(object obj)
        {
            IBearded emotional = obj as IBearded;
            if (emotional != null)
            {
                Console.Write($"{obj.GetType().Name}: ");
                emotional.GrowBeard();
            }
        }

        private void GrowHair(object item)
        {
            IHairy emotional = item as IHairy;
            if (emotional != null)
            {
                Console.Write($"{item.GetType().Name}: ");
                emotional.GrowHair();
            }
        }
    }


    public class Dog : IEmotional
    {
        public void BeHappy()
        {
            Console.WriteLine("tail waiving");
        }
    }

    class Boy : IEmotional
    {
        public void BeHappy()
        {
            Console.WriteLine("haha");
        }
    }

    public class Mom : IHairy, IEmotional
    {
        public void BeHappy()
        {
            Console.WriteLine("hihi");
        }

        public void GrowHair()
        {
            Console.WriteLine("hair gets long");
        }
    }

    public class Dad : IBearded, IEmotional
    {
        public void BeHappy()
        {
            Console.WriteLine("hoho");
        }

        public void GrowBeard()
        {
            Console.WriteLine("beard grows");
        }
    }

    public interface IBearded
    {
        void GrowBeard();
    }

    public interface IEmotional
    {
        void BeHappy();
    }

    public interface IHairy
    {
        void GrowHair();
    }
}
