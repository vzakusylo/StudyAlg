using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace managing_responsibilities_object_composition
{
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void Main()
        {
            FamilyMember dad = new FamilyMember(new object[] { new Bearded("Dad"), new Emotional("Dad", "hoho") });
            FamilyMember mom = new FamilyMember(new object[] { new Hairy("Mom"), new Emotional("Mom", "hihi") });
            FamilyMember boy = new FamilyMember(new object[] { new Emotional("Boy", "haha") });
            FamilyMember dog = new FamilyMember(new object[] { new Emotional("Dog", "tail waving") });
            FamilyMember uncle = new FamilyMember(new object[] { new Bearded("Uncle"), new Hairy("Uncle") });

            Family family = new Family(new FamilyMember[] { dad, mom, boy, dog, uncle });

            family.WinterBegins();
            family.SummerComes();
        }
    }

    class FamilyMember
    {
        private readonly IEnumerable<object> parts;

        public FamilyMember(IEnumerable<object> parts)
        {
            this.parts = parts;
        }

        public T As<T>()
        {
            foreach (var obj in parts)            
                if (obj is T)                
                    return (T)obj;            
            return default;
        }
    }

    class Hairy : IHairy
    {
        private readonly string owner;

        public Hairy(string owner)
        {
            this.owner = owner;
        }

        public void GrowHair()
        {
            Console.WriteLine($"{owner}: hair gets long");
        }
    }

    class Emotional : IEmotional
    {
        private readonly string owner;
        private readonly string laughingSound;

        public Emotional(string owner, string laughingSound)
        {
            this.owner = owner;
            this.laughingSound = laughingSound;
        }

        public void BeHappy()
        {
            Console.WriteLine($"{owner}: {laughingSound}");
        }
    }

    class Bearded : IBearded
    {
        private readonly string owner;

        public Bearded(string owner)
        {
            this.owner = owner;
        }

        public void GrowBeard()
        {
            Console.WriteLine($"{owner}: bear grows");
        }
    }

    class Family
    {
        private IEnumerable<FamilyMember> members;

        public Family(IEnumerable<FamilyMember> members)
        {
            this.members = new List<FamilyMember>(members); 
        }

        public void WinterBegins()
        {
            Console.WriteLine("Winter just came....");
            foreach (FamilyMember member in this.members)
            {
                IHairy hairy = member.As<IHairy>();
                if (hairy != null)
                {
                    hairy.GrowHair();
                }

                IBearded bearded = member.As<IBearded>();
                if (bearded != null)
                {
                    bearded.GrowBeard();
                }
            }
            Console.WriteLine(new string('-', 20));
        }

        public void SummerComes()
        {
            Console.WriteLine("Summer is here...");
            foreach (FamilyMember member in this.members)
            {
                IEmotional emotional = member.As<IEmotional>();
                if (emotional != null)
                {
                    emotional.BeHappy();
                }
            }
            Console.WriteLine(new string('-', 20));
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
