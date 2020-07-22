using System;
using System.Collections.Generic;
using Xunit;

namespace SimulatingCovarianceSpecification
{
    public class Program
    {
        [Fact]
        public void Run()
        {
            IUser user =
                 UserSpecification
                     .ForPerson()
                     .WithName("Max")
                     .WithSurname("Plank")
                     .WithPrimaryContact(
                         ContactSpecification.ForEmailAddress("max.planck@my.institure.com"))
                     .WithAlternativeContact(
                         ContactSpecification.ForEmailAddress("max.planck@my.institute.com"))
                     .AndNoMoreContacts()
                     .Build();
        }
    }

    public class ConvertingSpecification<TBase, TProduct> :
        IBuildingSpecification<TBase> where TProduct : TBase
    {
        private IBuildingSpecification<TProduct> ContainedSpec { get; }

        public ConvertingSpecification(IBuildingSpecification<TProduct> containedSpec)
        {
            if (containedSpec == null)
                throw new ArgumentException();

            ContainedSpec = containedSpec;
        }

        public TBase Build() => ContainedSpec.Build();
    }

    public interface IBuildingSpecification<out T>
    {
        T Build();
    }

    internal class ContactSpecification
    {
        internal static object ForEmailAddress(string v)
        {
            throw new NotImplementedException();
        }
    }

    internal class UserSpecification
    {
        internal static IForPerson ForPerson()
        {
            throw new NotImplementedException();
        }
    } 

    interface IForPerson { IUser ForPerson();
        IWithName WithName(string v);
    }
    
    interface IWithName { IUser WithName();
        IWithSurname WithSurname(string v);
    }

    interface IWithSurname { IPrimaryContact WithPrimaryContact(object p); IUser WithSurname(); }


    interface IPrimaryContact { IUser PrimaryContact();
        IUser WithAlternativeContact(object v);
    }

    internal interface IUser
    {
        IUser AndNoMoreContacts();
        IUser Build();
    }

 
}
