using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace managing_responsibilities_object_applying_chain_of_resposibility
{
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void Main()
        {
            FamilyMember dad = new FamilyMember(new Bearded("Dad", new Emotional("Dad", "hoho")));
            FamilyMember mom = new FamilyMember(new Hairy("Mom", new Emotional("Mom", "hihi")));
            FamilyMember boy = new FamilyMember(new Emotional("Boy", "haha"));
            FamilyMember dog = new FamilyMember(new Emotional("Dog", "tail waving") );
            FamilyMember uncle = new FamilyMember(new Bearded("Uncle", new Hairy("Uncle")));
            FamilyMember granddad = new FamilyMember(new Bearded("Granddad", new Hairy("Granddad", new Emotional("Granddad", "oyoyo"))));

            Family family = new Family(new FamilyMember[] { dad, mom, boy, dog, uncle, granddad });

            family.WinterBegins();
            family.SummerComes();
        }
    }
    interface IChainElement
    {
        T As<T>(T defaultValue) where T : class;
    }

    class ChainElement : IChainElement
    {
        private readonly IChainElement next;
        public ChainElement(IChainElement next)
        {
            this.next = next;
        }

        protected ChainElement() : this(NullChainElement.Instance) { }

        public virtual T As<T>(T defaultValue) where T : class
        {
            if (this is T)
            {
                return this as T;
            }
            return this.next.As<T>(defaultValue);
        }
    }

    class NullChainElement: IChainElement
    {
        private static IChainElement instance;
        private NullChainElement() { }

        public static IChainElement Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NullChainElement();
                }
                return instance;
            }
        }

        public T As<T>(T defaultValue) where T : class
        {
            return defaultValue;
        }
    }

    class FamilyMember
    {
        private readonly ChainElement components;

        public FamilyMember(ChainElement components)
        {
            this.components = components;
        }

        public T As<T>(T defaultValue) where T: class
        {
            return this.components.As<T>(defaultValue);
        }
    }

    class Hairy : ChainElement, IHairy
    {
        private readonly string owner;

        public Hairy(string owner, IChainElement next) : base(next)
        {
            this.owner = owner;
        }

        public Hairy(string owner)
        {
            this.owner = owner;
        }

        public void GrowHair()
        {
            Console.WriteLine($"{owner}: hair gets long");
        }
    }

    class Emotional : ChainElement, IEmotional
    {
        private readonly string owner;
        private readonly string laughingSound;

        public Emotional(string owner, string laughingSound, IChainElement next): base(next)
        {
            this.owner = owner;
            this.laughingSound = laughingSound;
        }

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

    class Bearded : ChainElement, IBearded
    {
        private readonly string owner;

        public Bearded(string owner, IChainElement next) : base(next)
        {
            this.owner = owner;
        }

        public Bearded(string owner)
        {
            this.owner = owner;
        }

        public void GrowBeard()
        {
            Console.WriteLine($"{owner}: bear grows");
        }
    }

    class NullBearded : IBearded
    {
        private static IBearded instance;
        private NullBearded() { }

        public static IBearded Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NullBearded();
                }
                return instance;
            }
        }

        public void GrowBeard()
        {
           
        }
    }

    class NullHairy : IHairy
    {
        private static IHairy instance;
        private NullHairy() { }

        public static IHairy Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NullHairy();
                }
                return instance;
            }
        }

        public void GrowHair()
        {
           
        }
    }

    class NullEmotional : IEmotional
    {
        private static IEmotional instance;
        private NullEmotional() { }

        public static IEmotional Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NullEmotional();
                }
                return instance;
            }
        }

        public void BeHappy()
        {
            
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
                member.As<IHairy>(NullHairy.Instance).GrowHair();
                member.As<IBearded>(NullBearded.Instance).GrowBeard();
            }
            Console.WriteLine(new string('-', 20));
        }

        public void SummerComes()
        {
            Console.WriteLine("Summer is here...");
            foreach (FamilyMember member in this.members)
            {
                member.As<IEmotional>(NullEmotional.Instance).BeHappy();
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
