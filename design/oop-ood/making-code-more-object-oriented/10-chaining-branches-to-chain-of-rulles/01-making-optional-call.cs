using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Chained_Branching_Rule_Objects
{
    [TestClass]
    public class Client
    {
        [TestMethod]
        public void Main()
        {
            DateTime sellingDate = new DateTime(2019, 12, 9);
            TimeSpan moneyBackSpan = TimeSpan.FromDays(30);

            IWarranty warranty = new LifetimeWarranty(sellingDate);
            SoldArticle goods = new SoldArticle(warranty, warranty, null);

            ClaimWarrenty(goods);
        }

        // warning: beware of bool method arguments
        // Their appearance is the indicator of leaving the OO design principles
        private static void ClaimWarrenty(SoldArticle article)  // bool flags
        {
            DateTime now = DateTime.Now;
            article.MoneyBackGuarantee.Claim(now, () => Console.WriteLine("Offer money back"));
            article.ExpressWarranty.Claim(now, () => Console.WriteLine("Offer repair"));
        }
    }

    // New requirement: Some article have replaceable electrical parts. Circuitry is subject to separate warranty. 
    public class SoldArticle
    {
        // Idea: use IWarrenty interface to represent state of the warranty 
        // money back if looks like new
        public IWarranty MoneyBackGuarantee { get; private set; }
        public IWarranty ExpressWarranty { get; set; }

        // Only if article doesn't work
        public IWarranty NotOpperationalWarranty { get; set; }
        // If replaceable part malfunctions
        private IWarranty CircuitryWarranty { get; set; }
        private IOption<Part> Circuitry { get; set; } = Option<Part>.None;
        private IWarrantyRules WarrantyRules { get; }
      

        public SoldArticle(IWarranty moneyBack, IWarranty express, 
           // IWarrantyRulesFactory rulesFactory
            )
        {
            MoneyBackGuarantee = moneyBack;
            ExpressWarranty = VoidWarranty.Instance;
    
            CircuitryWarranty = VoidWarranty.Instance;
           // OperationStatus = DeviceStatus.AllFine();           

            //WarrantyRules = rulesFactory.Create(ClaimMoneyBack,
            //    ClaimNotOperationalWarranty,
            //    ClaimCircuitryWarranty);
        }

        private IWarranty FailedCircuitryWarranty { get; set; }


        public void CircuitryNotOpperational(DateTime detectedOn)
        {
           
        }
        public void InstallCircuitry(Part circuitry, IWarranty extendedWarranty)
        {
            Circuitry = Option<Part>.Some(circuitry);
            CircuitryWarranty = extendedWarranty;
            //WarrantyRules.CircuitryOperential();
        }

        public void CircuitryNotOperational(DateTime detectedOn)
        {
            Circuitry
                .WhenSome()
                .Do(c =>
                {
                    c.MarkDefective(detectedOn);
                   // OperationStatus = OperationStatus.CircuitryFailed();
                });
        }

        public void VisibleDamage()
        {
          //  OperationStatus = OperationStatus.WithVisibleDemage();
        }

        public void NotOperational()
        {
           // OperationStatus = OperationStatus.NotOperational();
        }

        public void Repaired()
        {
          //  OperationStatus = OperationStatus.Repaired();
        }       

        public IReadOnlyDictionary<DeviceStatus, Action<Action>> WarrantyMap { get; private set; }
        public bool IsOperational { get; private set; }
        public bool IsCircuitryOperational { get; private set; }
        public bool IsAroundCristmas { get; private set; }

        public void ClaimWarranty(Action onValidClaim) // where is problem, the problem is in representation
        {
            WarrantyRules.Claim(onValidClaim);

            //bool moneyReturned = false;
            //bool isAroundCrismas = this.IsAroundCristmas;
            //if (isAroundCrismas)
            //{
            //    MoneyBackGuarantee.Claim(DateTime.Now, () =>
            //    {
            //        moneyReturned = true;
            //        onValidClaim();
            //    });
            //}
           
            //MoneyBackGuarantee.Claim(DateTime.Now, ()=>{
            //    moneyReturned = true;
            //    onValidClaim();
            //});

            //if (!moneyReturned && !this.IsOperational)
            //{
            //    NotOpperationalWarranty.Claim(DateTime.Now, onValidClaim);
            //}
            //else if (!moneyReturned && !IsCircuitryOperational)
            //{
            //    this.Circuitry
            //        .WhenSome()
            //        .Do(c => CircuitryWarranty.Claim(c.DefecDetectedOn, onValidClaim))
            //        .Execute();
            //}
        }

        public interface IWarrantyRules
        {
            void Claim(Action onValidClaim);
        }

        private void ClaimMoneyBack(Action action) =>
            MoneyBackGuarantee.Claim(DateTime.Now, action);

        private void ClaimNotOperationalWarranty(Action action) =>
            NotOpperationalWarranty.Claim(DateTime.Now, action);

        private void ClaimCircuitryWarranty(Action action)
        {
            Circuitry
                .WhenSome()
                .Do(c => CircuitryWarranty.Claim(c.DefecDetectedOn, action))
                .Execute();
        }
    }

    public interface IWarrentyMapFactory
    {
        IReadOnlyDictionary<DeviceStatus, Action<Action>> Create(
            Action<Action> claimMoneyBack,
            Action<Action> claimNotOperential,
            Action<Action> claimCircuitry);
    }

    public class WarrantyRules : IWarrentyMapFactory
    {
        public IReadOnlyDictionary<DeviceStatus, Action<Action>> Create(
            Action<Action> claimMoneyBack,
            Action<Action> claimNotOperential,
            Action<Action> claimCircuitry) =>
           new Dictionary<DeviceStatus, Action<Action>>() // switch (deviceStatus)
           {
               [DeviceStatus.AllFine()] = claimMoneyBack, // case AllFine: ClaimMoneyBack()
               [DeviceStatus.AllFine().NotOperational()] = claimNotOperential,
               [DeviceStatus.AllFine().WithVisibleDemage()] = (action) => { },
               [DeviceStatus.AllFine().NotOperational().WithVisibleDemage()] = claimNotOperential,
               [DeviceStatus.AllFine().CircuitryFailed()] = claimCircuitry,
               [DeviceStatus.AllFine().NotOperational().CircuitryFailed()] = claimNotOperential,
               [DeviceStatus.AllFine().CircuitryFailed().WithVisibleDemage()] = claimCircuitry,
               [DeviceStatus.AllFine().NotOperational().WithVisibleDemage().CircuitryFailed()] = claimNotOperential

           };
    }

    //[Flags]
    //public enum DeviceStatus // avoid using enumeration, they only represent the state, in OOD we would like to have state and behavior
    //{ // they have nothing to say about how we should behave if this or that is happened to the system.
    //    AllFine = 0,
    //    NotOpperetional = 1,
    //    VisiblyDamaged = 2,
    //    CircuitryFailed = 4
    //}

    public sealed class DeviceStatus : IEquatable<DeviceStatus> // avoid using enumeration, they only represent the state, in OOD we would like to have state and behavior
    { // they have nothing to say about how we should behave if this or that is happened to the system.

        [Flags]
        internal enum StatusRerpesentation
        {
            AllFine = 0,
            NotOpperetional = 1,
            VisiblyDamaged = 2,
            CircuitryFailed = 4
        }

        // advice: consider making small classes immutable. cost of creating new instances will likely be negligible.
        private StatusRerpesentation Representation { get; } // immutable, once constructed device status would not be changed
        // if need to be changed we will create new object.

        private DeviceStatus(StatusRerpesentation representation)
        {
            Representation = representation; // my representation is my business
        }

        // the caller will start with the device that is fine
        public static DeviceStatus AllFine() => new DeviceStatus(StatusRerpesentation.AllFine);

        // later the caller may say
        public DeviceStatus WithVisibleDemage() =>
            new DeviceStatus(this.Representation | StatusRerpesentation.VisiblyDamaged);

        public DeviceStatus NotOperational() =>
            new DeviceStatus(this.Representation | StatusRerpesentation.NotOpperetional);

        public DeviceStatus Repaired() =>
            new DeviceStatus(this.Representation & ~StatusRerpesentation.NotOpperetional);

        public DeviceStatus CircuitryFailed() =>
            new DeviceStatus(this.Representation | StatusRerpesentation.CircuitryFailed);

        public DeviceStatus CircuitryReplaced() =>
            new DeviceStatus(this.Representation & ~StatusRerpesentation.CircuitryFailed);

        public override int GetHashCode() =>
            (int)Representation;

        public bool Equals([AllowNull] DeviceStatus other) =>
            other != null && Representation == other.Representation;

        public override bool Equals(object obj) => Equals(obj as DeviceStatus);

        public static bool operator ==(DeviceStatus a, DeviceStatus b) =>
            ReferenceEquals(a, null) && ReferenceEquals(b, null) || ReferenceEquals(a, null) && a.Equals(b);

        public static bool operator !=(DeviceStatus a, DeviceStatus b) => !(a == b);
    }

    public interface IOption<T>
    {
        IFiltered<T> WhenSome();
    }

    public interface IFiltered<T> : IFilteredActionable<T>
    {
        IMapped<T, TestResult> MapTo<TResult>(Func<T, TResult> mapped);
    }

    public interface IMapped<T, TResult>
    {
        IFilteredMapped<T, TResult> WhenSome();
    }

    public interface IFilteredMapped<T, TResult>
    {
        IMapped<T, TResult> MapTo(Func<T, TResult> mapping);
    }

    public interface IActionable<T>
    {
        IFilteredActionable<T> When(Func<T, bool> func);
        IFilteredActionable<T> WhenSome();

        void Execute();
    }

    public interface IFilteredActionable<T>
    {
        IActionable<T> Do(Action<T> action);
    }

    public class Part // this class know the date when it was marked as defected
    {
        public DateTime InstallmentDate { get; }
        public DateTime DefecDetectedOn { get; private set; }
        public Part(DateTime instalmentDate)
        {
            InstallmentDate = instalmentDate;
        }

        public void MarkDefective(DateTime withDate)
        {
            DefecDetectedOn = withDate;
        }
    }

    public interface IWarranty
    {
        void Claim(DateTime onDate, Action onValidClaim);
    }

    public class TimeLimitedWarranty : IWarranty
    {
        private DateTime DateIssued { get; set; }
        private TimeSpan Duration { get; set; }

        public TimeLimitedWarranty(DateTime dateIssued, TimeSpan duration)
        {
            DateIssued = dateIssued.Date;
            Duration = TimeSpan.FromDays(duration.Days);
        }

        private bool IsValidOn(DateTime date) =>
            date.Date >= DateIssued &&
            date.Date < DateIssued + Duration;

        public void Claim(DateTime onDate, Action onValidClaim)
        {
            if (!IsValidOn(onDate))
            {
                return;
            }
            onValidClaim();
        }
    }


    public class VoidWarranty : IWarranty // Null object class
    {
        [ThreadStatic] //Indicates that the value of a static field is unique for each thread.
        private static VoidWarranty instance;
        private VoidWarranty() { }
        public static VoidWarranty Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new VoidWarranty();
                }
                return instance;
            }
        }

        public void Claim(DateTime onDate, Action onValidClaim)
        {

        }

        public bool IsValidOn(DateTime date) => false;
    }

    public class LifetimeWarranty : IWarranty // Special case class
    {
        private DateTime IssuingDate { get; }

        public LifetimeWarranty(DateTime issuingDate) => IssuingDate = issuingDate;

        public bool IsValidOn(DateTime date) => date >= IssuingDate;

        public void Claim(DateTime onDate, Action onValidClaim)
        {
            if (!IsValidOn(onDate))
            {
                return;
            }
            onValidClaim();
        }
    }

    // Responsibility of the option type: contain the value or contain nothing, ensure that there is never more than one value. 
    public class Option<T> : IOption<T>
    {
        private IEnumerable<T> Content { get; }

        private Option(IEnumerable<T> content)
        {
            Content = content;
        }

        public static Option<T> Some(T value) // static factory method 
        => new Option<T>(new[] { value });

        public static Option<T> None
        => new Option<T>(new T[0]);

        public IEnumerator<T> GetEnumerator() => Content.GetEnumerator();

        public IFiltered<T> WhenSome()
        {
            throw new NotImplementedException();
        }
    }

    static class EnumExt
    {
        public static void Do<T>(this IEnumerable<T> sequence, Action<T> action)
        {
            sequence.ToList().ForEach(action); // ToList call requires O(n) memory! Sequence itself might require O(1) only.
        }
    }
}
