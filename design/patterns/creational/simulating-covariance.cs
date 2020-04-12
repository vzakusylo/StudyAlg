using System;
using System.Collections.Generic;
using Xunit;

namespace SimulatingCovarianceWrapped
{
    public class Program
    {
        [Fact]
        public void Run()
        {
            Cat cat = new WrappedDog(new Dog());
        }
    }

    class Cat { }

    class Dog { }

    class WrappedDog : Cat
    {
        public WrappedDog(Dog dog) { }
    }
}

namespace SimulatingCovarianceExplicit
{
    public class Program
    {
        [Fact]
        public void Run()
        {
            Cat cat = (Cat) new Dog();
        }
    }

    class Cat
    {
        public static explicit operator Cat(Dog dog) =>
            new Cat();
    }

    class Dog { }

}
namespace SimulatingCovarianceImplicit
{
    public class Program
    {
        [Fact]
        public void Run()
        {
            Cat cat = new Dog();
        }
    }

    class Cat {
        public static implicit operator Cat(Dog dog) => 
            new Cat();
    }

    class Dog { }

}