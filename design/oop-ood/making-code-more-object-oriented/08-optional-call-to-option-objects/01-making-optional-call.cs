using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using static Chained_Branching_Rule_Objects.SoldArticle;

namespace Optional_Calls_6
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
        public DeviceStatus OperationStatus { get; private set; }

        public SoldArticle(IWarranty moneyBack, IWarranty express,
           IWarrentyMapFactory rulesFactory)
        {
            MoneyBackGuarantee = moneyBack;
            ExpressWarranty = VoidWarranty.Instance;
            NotOpperationalWarranty = express;
            CircuitryWarranty = VoidWarranty.Instance;
            OperationStatus = DeviceStatus.AllFine();

            WarrantyMap = rulesFactory.Create(
                ClaimMoneyBack,
                ClaimNotOperationalWarranty,
                ClaimCircuitryWarranty);            
        }

        private IWarranty FailedCircuitryWarranty { get; set; }


        public void CircuitryNotOpperational(DateTime detectedOn)
        {
            this.Circuitry
                 .WhenSome()
                 .Do(c =>
                 {
                     c.MarkDefective(detectedOn);
                     this.OperationStatus = OperationStatus.CircuitryFailed();
                 })
                 .Execute();
        }
        public void InstallCircuitry(Part circuitry, IWarranty extendedWarranty)
        {
            Circuitry = Option<Part>.Some(circuitry);
            CircuitryWarranty = extendedWarranty;
            OperationStatus = this.OperationStatus.CircuitryReplaced(); // state representation
        }

        public void CircuitryNotOperational(DateTime detectedOn)
        {
            Circuitry
                .WhenSome()
                .Do(c =>
                {
                    c.MarkDefective(detectedOn);
                    OperationStatus = OperationStatus.CircuitryFailed();
                });
        }

        public void VisibleDamage()
        {
            OperationStatus = OperationStatus.WithVisibleDemage();
        }

        public void NotOperational()
        {
            OperationStatus = OperationStatus.NotOperational();
        }

        public void Repaired()
        {
            OperationStatus = OperationStatus.Repaired();
        }       

        public IReadOnlyDictionary<DeviceStatus, Action<Action>> WarrantyMap { get; private set; }
        public bool IsOperational { get; private set; }
        public bool IsCircuitryOperational { get; private set; }

        public void ClaimWarranty(Action onValidClaim) // where is problem, the problem is in representation
        {
            WarrantyMap[OperationStatus].Invoke(onValidClaim);

            bool moneyReturned = false;
            MoneyBackGuarantee.Claim(DateTime.Now, ()=>{
                moneyReturned = true;
                onValidClaim();
            });

            if (!moneyReturned && !this.IsOperational)
            {
                NotOpperationalWarranty.Claim(DateTime.Now, onValidClaim);
            }
            else if (!moneyReturned && !IsCircuitryOperational)
            {
                this.Circuitry
                    .WhenSome()
                    .Do(c => CircuitryWarranty.Claim(c.DefecDetectedOn, onValidClaim))
                    .Execute();
            }
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

namespace Optional_Calls_5
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
            SoldArticle goods = null; // new SoldArticle(warranty, warranty);

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
        public DeviceStatus OperationStatus { get; private set; }

        public SoldArticle(IWarranty moneyBack, IWarranty express,
            Func<Action<Action>, 
                Action<Action>, 
                Action<Action>, 
                IReadOnlyDictionary<DeviceStatus, Action<Action>>> rulesFactory)
        {
            MoneyBackGuarantee = moneyBack;
            ExpressWarranty = VoidWarranty.Instance;
            NotOpperationalWarranty = express;
            CircuitryWarranty = VoidWarranty.Instance;
            OperationStatus = DeviceStatus.AllFine();

            WarrantyMap = rulesFactory(
                ClaimMoneyBack, 
                ClaimNotOperationalWarranty,
                ClaimCircuitryWarranty);
        }

        private IWarranty FailedCircuitryWarranty { get; set; }


        public void CircuitryNotOpperational(DateTime detectedOn)
        {
            this.Circuitry
                 .WhenSome()
                 .Do(c =>
                 {
                     c.MarkDefective(detectedOn);
                     this.OperationStatus = OperationStatus.CircuitryFailed();
                 })
                 .Execute();
        }
        public void InstallCircuitry(Part circuitry, IWarranty extendedWarranty)
        {
            Circuitry = Option<Part>.Some(circuitry);
            CircuitryWarranty = extendedWarranty;
            OperationStatus = this.OperationStatus.CircuitryReplaced(); // state representation
        }

        public void CircuitryNotOperational(DateTime detectedOn)
        {
            Circuitry
                .WhenSome()
                .Do(c =>
                {
                    c.MarkDefective(detectedOn);
                    OperationStatus = OperationStatus.CircuitryFailed();
                });
        }

        public void VisibleDamage()
        {
            OperationStatus = OperationStatus.WithVisibleDemage();
        }

        public void NotOperational()
        {
            OperationStatus = OperationStatus.NotOperational();
        }

        public void Repaired()
        {
            OperationStatus = OperationStatus.Repaired();
        }

        //public void ClaimWarranty(Action onValidClaim) // where is problem, the problem is in representation
        //{
        //    switch (this.OperationStatus) // computed jump based on 
        //    {
        //        case DeviceStatus.AllFine: // state representation
        //            MoneyBackGuarantee.Claim(DateTime.Now, onValidClaim);
        //            break;
        //        case DeviceStatus.NotOpperetional: // state representation
        //        case DeviceStatus.NotOpperetional | DeviceStatus.VisiblyDamaged: // state representation
        //        case DeviceStatus.NotOpperetional | DeviceStatus.CircuitryFailed: // state representation
        //        case DeviceStatus.NotOpperetional | DeviceStatus.VisiblyDamaged | DeviceStatus.CircuitryFailed: // state representation
        //            NotOpperationalWarranty.Claim(DateTime.Now, onValidClaim);
        //            break;
        //        case DeviceStatus.VisiblyDamaged: // state representation
        //            break;
        //        case DeviceStatus.CircuitryFailed: // state representation
        //        case DeviceStatus.VisiblyDamaged | DeviceStatus.CircuitryFailed: // state representation
        //            Circuitry
        //                .WhenSome()
        //                .Do(c => CircuitryWarranty.Claim(c.DefecDetectedOn, onValidClaim))
        //                .Execute();
        //            break;
        //    }
        //}

        public IReadOnlyDictionary<DeviceStatus, Action<Action>> WarrantyMap { get; private set; }

        public void ClaimWarranty(Action onValidClaim) // where is problem, the problem is in representation
        {
            WarrantyMap[OperationStatus].Invoke(onValidClaim);
        }

        //private IReadOnlyDictionary<DeviceStatus, Action<Action>> InitializeWarrentyMap() =>
        //    new Dictionary<DeviceStatus, Action<Action>>() // switch (deviceStatus)
        //    {
        //        [DeviceStatus.AllFine()] = ClaimMoneyBack, // case AllFine: ClaimMoneyBack()
        //        [DeviceStatus.AllFine().NotOperational()] = ClaimNotOperationalWarranty,
        //        [DeviceStatus.AllFine().WithVisibleDemage()] = (action) => { },
        //        [DeviceStatus.AllFine().NotOperational().WithVisibleDemage()] = ClaimNotOperationalWarranty,
        //        [DeviceStatus.AllFine().CircuitryFailed()] = ClaimCircuitryWarranty,
        //        [DeviceStatus.AllFine().NotOperational().CircuitryFailed()] = ClaimNotOperationalWarranty,
        //        [DeviceStatus.AllFine().CircuitryFailed().WithVisibleDemage()] = ClaimCircuitryWarranty,
        //        [DeviceStatus.AllFine().NotOperational().WithVisibleDemage().CircuitryFailed()] = ClaimNotOperationalWarranty

        //    };

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

    interface IWarrentyMapFactory
    {
        IReadOnlyDictionary<DeviceStatus, Action<Action>> Create(
            Action<Action> claimMoneyBack,
            Action<Action> claimNotOperential,
            Action<Action> claimCircuitry);
    }

    static class WarrantyRules
    {
        public static IReadOnlyDictionary<DeviceStatus, Action<Action>> GetCommonRules(
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

namespace Optional_Calls_4
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
            SoldArticle goods = new SoldArticle(warranty, warranty);

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
        public DeviceStatus OperationStatus { get; private set; }

        public SoldArticle(IWarranty moneyBack, IWarranty express)
        {
            MoneyBackGuarantee = moneyBack;
            ExpressWarranty = VoidWarranty.Instance;
            NotOpperationalWarranty = express;
            CircuitryWarranty = VoidWarranty.Instance;
            OperationStatus = DeviceStatus.AllFine();

            WarrantyMap = WarrantyRules.GetCommonRules(
                ClaimMoneyBack, 
                ClaimNotOperationalWarranty,
                ClaimCircuitryWarranty);
        }

        private IWarranty FailedCircuitryWarranty { get; set; }
       

        public void CircuitryNotOpperational(DateTime detectedOn)
        {
            this.Circuitry
                 .WhenSome()
                 .Do(c =>
                 {
                     c.MarkDefective(detectedOn);
                     this.OperationStatus = OperationStatus.CircuitryFailed();
                 })
                 .Execute();
        }
        public void InstallCircuitry(Part circuitry, IWarranty extendedWarranty)
        {
            Circuitry = Option<Part>.Some(circuitry);
            CircuitryWarranty = extendedWarranty;
            OperationStatus = this.OperationStatus.CircuitryReplaced(); // state representation
        }

        public void CircuitryNotOperational(DateTime detectedOn)
        {
            Circuitry
                .WhenSome()
                .Do(c =>
                {
                    c.MarkDefective(detectedOn);
                    OperationStatus = OperationStatus.CircuitryFailed(); 
                });
        }

        public void VisibleDamage()
        {
            OperationStatus = OperationStatus.WithVisibleDemage();
        }

        public void NotOperational()
        {
            OperationStatus = OperationStatus.NotOperational();
        }

        public void Repaired()
        {
            OperationStatus = OperationStatus.Repaired();
        }

        //public void ClaimWarranty(Action onValidClaim) // where is problem, the problem is in representation
        //{
        //    switch (this.OperationStatus) // computed jump based on 
        //    {
        //        case DeviceStatus.AllFine: // state representation
        //            MoneyBackGuarantee.Claim(DateTime.Now, onValidClaim);
        //            break;
        //        case DeviceStatus.NotOpperetional: // state representation
        //        case DeviceStatus.NotOpperetional | DeviceStatus.VisiblyDamaged: // state representation
        //        case DeviceStatus.NotOpperetional | DeviceStatus.CircuitryFailed: // state representation
        //        case DeviceStatus.NotOpperetional | DeviceStatus.VisiblyDamaged | DeviceStatus.CircuitryFailed: // state representation
        //            NotOpperationalWarranty.Claim(DateTime.Now, onValidClaim);
        //            break;
        //        case DeviceStatus.VisiblyDamaged: // state representation
        //            break;
        //        case DeviceStatus.CircuitryFailed: // state representation
        //        case DeviceStatus.VisiblyDamaged | DeviceStatus.CircuitryFailed: // state representation
        //            Circuitry
        //                .WhenSome()
        //                .Do(c => CircuitryWarranty.Claim(c.DefecDetectedOn, onValidClaim))
        //                .Execute();
        //            break;
        //    }
        //}

        public IReadOnlyDictionary<DeviceStatus, Action<Action>> WarrantyMap { get; private set; }

        public void ClaimWarranty(Action onValidClaim) // where is problem, the problem is in representation
        {
            WarrantyMap[OperationStatus].Invoke(onValidClaim);
        }

        //private IReadOnlyDictionary<DeviceStatus, Action<Action>> InitializeWarrentyMap() =>
        //    new Dictionary<DeviceStatus, Action<Action>>() // switch (deviceStatus)
        //    {
        //        [DeviceStatus.AllFine()] = ClaimMoneyBack, // case AllFine: ClaimMoneyBack()
        //        [DeviceStatus.AllFine().NotOperational()] = ClaimNotOperationalWarranty,
        //        [DeviceStatus.AllFine().WithVisibleDemage()] = (action) => { },
        //        [DeviceStatus.AllFine().NotOperational().WithVisibleDemage()] = ClaimNotOperationalWarranty,
        //        [DeviceStatus.AllFine().CircuitryFailed()] = ClaimCircuitryWarranty,
        //        [DeviceStatus.AllFine().NotOperational().CircuitryFailed()] = ClaimNotOperationalWarranty,
        //        [DeviceStatus.AllFine().CircuitryFailed().WithVisibleDemage()] = ClaimCircuitryWarranty,
        //        [DeviceStatus.AllFine().NotOperational().WithVisibleDemage().CircuitryFailed()] = ClaimNotOperationalWarranty

        //    };

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

    static class WarrantyRules
    {
        public static IReadOnlyDictionary<DeviceStatus, Action<Action>> GetCommonRules(
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

        public bool Equals([AllowNull] DeviceStatus other)=>        
            other != null && Representation == other.Representation;        

        public override bool Equals(object obj) => Equals(obj as DeviceStatus);
        
        public static bool operator == (DeviceStatus a, DeviceStatus b) => 
            ReferenceEquals(a,null) && ReferenceEquals(b,null) || ReferenceEquals(a, null) && a.Equals(b);

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

namespace Optional_Calls_3
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
            SoldArticle goods = new SoldArticle(warranty, warranty);

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
        public DeviceStatus OperationStatus { get; private set; }

        public SoldArticle(IWarranty moneyBack, IWarranty express)
        {
            MoneyBackGuarantee = moneyBack;
            ExpressWarranty = VoidWarranty.Instance;
            NotOpperationalWarranty = express;
            CircuitryWarranty = VoidWarranty.Instance;
        }

        private IWarranty FailedCircuitryWarranty { get; set; }

        public void CircuitryNotOpperational(DateTime detectedOn)
        {
            this.Circuitry
                 .WhenSome()
                 .Do(c =>
                    {
                        c.MarkDefective(detectedOn);
                        this.OperationStatus |= DeviceStatus.CircuitryFailed;
                    })
                 .Execute();
        }


        public void InstallCircuitry(Part circuitry, IWarranty extendedWarranty)
        {
            Circuitry = Option<Part>.Some(circuitry);
            CircuitryWarranty = extendedWarranty;
            OperationStatus &= ~DeviceStatus.CircuitryFailed; // state representation
        }

        public void CircuitryNotOperational(DateTime detectedOn)
        {
            Circuitry
                .WhenSome()
                .Do(c =>
                {
                    c.MarkDefective(detectedOn);
                    OperationStatus |= DeviceStatus.CircuitryFailed; // state representation 
                });
        }

        public void VisibleDamage()
        {
            OperationStatus |= DeviceStatus.VisiblyDamaged; // state representation
        }

        public void NotOperational()
        {
            OperationStatus |= DeviceStatus.NotOpperetional; // state representation
        }        

        public void Repaired()
        {
            OperationStatus &= ~DeviceStatus.NotOpperetional; // state representation
        }
      
        public void ClaimWarranty(Action onValidClaim) // where is problem, the problem is in representation
        {
            switch (this.OperationStatus) // computed jump based on 
            {
                case DeviceStatus.AllFine: // state representation
                    MoneyBackGuarantee.Claim(DateTime.Now, onValidClaim);
                    break;
                case DeviceStatus.NotOpperetional: // state representation
                case DeviceStatus.NotOpperetional | DeviceStatus.VisiblyDamaged: // state representation
                case DeviceStatus.NotOpperetional | DeviceStatus.CircuitryFailed: // state representation
                case DeviceStatus.NotOpperetional | DeviceStatus.VisiblyDamaged | DeviceStatus.CircuitryFailed: // state representation
                    NotOpperationalWarranty.Claim(DateTime.Now, onValidClaim);
                    break;
                case DeviceStatus.VisiblyDamaged: // state representation
                    break;
                case DeviceStatus.CircuitryFailed: // state representation
                case DeviceStatus.VisiblyDamaged | DeviceStatus.CircuitryFailed: // state representation
                    Circuitry
                        .WhenSome()
                        .Do(c => CircuitryWarranty.Claim(c.DefecDetectedOn, onValidClaim))
                        .Execute();
                    break;
            }
        }
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

    [Flags]
    public enum DeviceStatus
    {
        AllFine = 0,
        NotOpperetional = 1,
        VisiblyDamaged = 2,
        CircuitryFailed = 4
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

namespace Optional_Calls_2
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
            SoldArticle goods = new SoldArticle(warranty, warranty);

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

        public SoldArticle(IWarranty moneyBack, IWarranty express)
        {
            MoneyBackGuarantee = moneyBack;
            ExpressWarranty = VoidWarranty.Instance;
            NotOpperationalWarranty = express;
            CircuitryWarranty = VoidWarranty.Instance;
        }

        public void VisibleDamage()
        {
            this.MoneyBackGuarantee = VoidWarranty.Instance;
        }
        public void NotOperational()
        {
            MoneyBackGuarantee = VoidWarranty.Instance;
            ExpressWarranty = NotOpperationalWarranty;
        }

        private IWarranty FailedCircuitryWarranty { get; set; }


        public void CircuitryNotOpperational(DateTime detectedOn)
        {
            //Circuitry.Do(circuitry => {
            //    circuitry.MarkDefective(detectedOn);
            //    CircuitryWarranty = FailedCircuitryWarranty;
            //});
        }

        public void InstallCircuitry(Part circuitry, IWarranty extendedWarranty)
        {
            Circuitry = Option<Part>.Some(circuitry);
            FailedCircuitryWarranty = extendedWarranty;
        }

        public void ClaimCircuitryWarranty(Action onValidClaim)
        {
            //Circuitry.Do(circuitry => {
            //    CircuitryWarranty.Claim(circuitry.DefecDetectedOn, onValidClaim);
            //});
        }

        public DeviceStatus OperationStatus { get; private set; }
        public void ClaimWarranty(Action onValidClaim)
        {
            switch (this.OperationStatus)
            {
                case DeviceStatus.AllFine:
                    MoneyBackGuarantee.Claim(DateTime.Now, onValidClaim);
                    break;
                case DeviceStatus.NotOpperetional:
                case DeviceStatus.NotOpperetional | DeviceStatus.VisiblyDamaged:
                case DeviceStatus.NotOpperetional | DeviceStatus.CircuitryFailed:
                case DeviceStatus.NotOpperetional | DeviceStatus.VisiblyDamaged | DeviceStatus.CircuitryFailed:
                    NotOpperationalWarranty.Claim(DateTime.Now, onValidClaim);
                    break;
                case DeviceStatus.VisiblyDamaged:
                    break;
                case DeviceStatus.CircuitryFailed:
                case DeviceStatus.VisiblyDamaged | DeviceStatus.CircuitryFailed:
                    Circuitry
                        .WhenSome()
                        .Do(c => CircuitryWarranty.Claim(c.DefecDetectedOn, onValidClaim))
                        .Execute();
                    break;
            }
        }
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

    [Flags]
    public enum DeviceStatus
    {
        AllFine = 0,
        NotOpperetional = 1,
        VisiblyDamaged = 2,
        CircuitryFailed = 4
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

namespace Optional_Calls_1
{
    [TestClass]
    public class Client
    {
        [TestMethod]
        public void Main()
        {
            DateTime sellingDate = new DateTime(2019, 12, 9);
            TimeSpan moneyBackSpan = TimeSpan.FromDays(30);
            TimeSpan warrantySpan = TimeSpan.FromDays(365);

            IWarranty moneyBack = new TimeLimitedWarranty(sellingDate, moneyBackSpan);
            //IWarranty warranty = new TimeLimitedWarranty(sellingDate, warrantySpan);

            //SoldArticle goods = new SoldArticle(moneyBack, warranty);
            //  Offer money back
            //  Offer repair

            //SoldArticle goods = new SoldArticle(VoidWarranty.Instance, warranty);
            // Offer repair
            IWarranty warranty = new LifetimeWarranty(sellingDate);
            SoldArticle goods = new SoldArticle(warranty, warranty);

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
        public IWarranty MoneyBackGuarantee { get; set; }
        public IWarranty ExpressWarranty { get; set; }
        public IWarranty NotOpperationalWarranty { get; set; }
        private IWarranty CircuitryWarranty { get; set; }
        private Option<Part> Circuitry { get; set; } = Option<Part>.None; 

        public SoldArticle(IWarranty moneyBack, IWarranty express)
        {
            MoneyBackGuarantee = moneyBack;
            ExpressWarranty = VoidWarranty.Instance;
            NotOpperationalWarranty = express;
            CircuitryWarranty = VoidWarranty.Instance;
        }

        public void VisibleDamage()
        {
            this.MoneyBackGuarantee = VoidWarranty.Instance;
        }
        public void NotOperational()
        {
            MoneyBackGuarantee = VoidWarranty.Instance;
            ExpressWarranty = NotOpperationalWarranty;
        }

        // Responsibility of the option type: contain the value or contain nothing, ensure that there is never more than one value. 
        public class Option<T> : IEnumerable<T>
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

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        private IWarranty FailedCircuitryWarranty { get; set; }

        public void CircuitryNotOpperational(DateTime detectedOn)
        {
            Circuitry.ToList().ForEach(circuitry => {
                circuitry.MarkDefective(detectedOn);
                CircuitryWarranty = FailedCircuitryWarranty;
            });          
        }

        public void InstallCircuitry(Part circuitry, IWarranty extendedWarranty)
        {
            Circuitry = Option<Part>.Some(circuitry);
            FailedCircuitryWarranty = extendedWarranty;
        }

        public void ClaimCircuitryWarranty(Action onValidClaim)
        {
            Circuitry.ToList().ForEach(circuitry => {
                CircuitryWarranty.Claim(circuitry.DefecDetectedOn, onValidClaim);
            });           
        }
    }

    public class Part
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
        // bool IsValidOn(DateTime date);

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

        public bool IsValidOn(DateTime date) =>
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
}

namespace Optional_Calls
{
    [TestClass]
    public class Client
    {
        [TestMethod]
        public void Main()
        {
            DateTime sellingDate = new DateTime(2019, 12, 9);
            TimeSpan moneyBackSpan = TimeSpan.FromDays(30);
            TimeSpan warrantySpan = TimeSpan.FromDays(365);

            IWarranty moneyBack = new TimeLimitedWarranty(sellingDate, moneyBackSpan);
            //IWarranty warranty = new TimeLimitedWarranty(sellingDate, warrantySpan);

            //SoldArticle goods = new SoldArticle(moneyBack, warranty);
            //  Offer money back
            //  Offer repair

            //SoldArticle goods = new SoldArticle(VoidWarranty.Instance, warranty);
            // Offer repair
            IWarranty warranty = new LifetimeWarranty(sellingDate);
            SoldArticle goods = new SoldArticle(warranty, warranty);

            ClaimWarrenty(goods);
        }

        // warning: beware of bool method arguments
        // Their appearance is the indicator of leaving the OO design principles
        private static void ClaimWarrenty(SoldArticle article)  // bool flags
        {
            DateTime now = DateTime.Now;
            article.MoneyBackGuarantee.Claim(now, () => Console.WriteLine("Offer money back"));
            article.ExpressWarranty.Claim(now, () => Console.WriteLine("Offer repair"));

            //if (article.MoneyBackGuarantee.IsValidOn(now))
            //{
            //    Console.WriteLine("Offer money back");
            //}
            //if (article.ExpressWarranty.IsValidOn(now))
            //{
            //    Console.WriteLine("Offer repair");
            //}
        }
    }

    // New requirement: Some article have replaceable electrical parts. Circuitry is subject to separate warranty. 
    public class SoldArticle
    {
        public IWarranty MoneyBackGuarantee { get; set; }
        public IWarranty ExpressWarranty { get; set; }
        public IWarranty NotOpperationalWarranty { get; set; }
        private IWarranty CircuitryWarranty { get; set; }
        private List<Part> Circuitry { get; set; } = new List<Part>();

        public SoldArticle(IWarranty moneyBack, IWarranty express)
        {
            MoneyBackGuarantee = moneyBack;
            ExpressWarranty = VoidWarranty.Instance;
            NotOpperationalWarranty = express;
            CircuitryWarranty = VoidWarranty.Instance;
        }

        public void VisibleDamage()
        {
            this.MoneyBackGuarantee = VoidWarranty.Instance;
        }
        public void NotOperational()
        {
            MoneyBackGuarantee = VoidWarranty.Instance;
            ExpressWarranty = NotOpperationalWarranty;
        }

        // Responsibility of the option type: contain the value or contain nothing, ensure that there is never more than one value. 
        public class Option<T> : IEnumerable<T>
        {
            private IEnumerable<T> Content { get; }

            private Option(IEnumerable<T> content)
            {
                Content = content;
            }

            public static Option<T> Some(T value) // static factory method 
            => new Option<T>(new [] { value });

            public static Option<T> None()
            => new Option<T>(new T[0]);

            public IEnumerator<T> GetEnumerator() => Content.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();            
        }

        private IWarranty FailedCircuitryWarranty { get; set; }

        public void CircuitryNotOpperational(DateTime detectedOn) 
        {
            Circuitry.ForEach(circuitry => {
                circuitry.MarkDefective(detectedOn);
                CircuitryWarranty = FailedCircuitryWarranty;
            });
            //Circuitry.MarkDefective(detectedOn);
        }

        public void InstallCircuitry(Part circuitry, IWarranty extendedWarranty)
        {
            Circuitry = new List<Part> { circuitry };
            FailedCircuitryWarranty = extendedWarranty;
        }

        public void ClaimCircuitryWarranty(Action onValidClaim)
        {
            Circuitry.ForEach(circuitry => {
                CircuitryWarranty.Claim(circuitry.DefecDetectedOn, onValidClaim);
            });
           // CircuitryWarranty.Claim(Circuitry.DefecDetectedOn, onValidClaim);
            //                   ^
            //                   |
            //                   L Actual Claim() implementation only know on runtime.
        }
    }

    public class Part
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
        // bool IsValidOn(DateTime date);

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

        public bool IsValidOn(DateTime date) =>
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
}
