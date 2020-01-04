using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
namespace branching_demo_2
{
    [TestClass]
    public class EntryPoint
    {
        [TestMethod]
        public void Main()
        {
        }
    }

    //Requirements:
    // Money can be deposited at any time
    // money can be withdrawn only after the account owner's identity has been verified.

    // Account holder can close the account at any time.
    // Closed account doesn't allow deposit and withdraw

    // Account should be frozen if not used for some time.

    // Account will be unfrozen automatically on deposit or withdraw.
    // Unfreezing the account triggers a custom action.

    public class Account
    {
        public decimal Balance { get; private set; } // side node
      
        private IAccountState State { get; set; }

        public Account(Action onUnfreeze)
        {
            State = new NotVerified(onUnfreeze);
        }

        // #1 Interaction: Deposit was invoked on the State
        // #2 Behavior: Result of the state.Deposit is new State
        // #5 Behavior: Deposit 10, Deposit 1 - Balance == 11
        public void Deposite(decimal amount)
        {
            State = State.Deposit(() => { Balance += amount; });
        }

        // #3 Interaction: Deposit was invoked on the State
        // #4 Behavior: Result of the state.Deposit is new State
        // #6 Behavior: Deposit 1, Verify, Withdraw 1 - Balance == 9
        public void Withdraw(decimal amount)
        {
            State = State.Withdaw(() => { Balance -= amount; });
        }

        public void HolderVerified()
        {
            State = State.HolderVerified();
        }

        public void Close()
        {
            State = State.Close();
        }

        public void Freeze()
        {
            State = State.Freeze();
        }
    }

    public interface IAccountState
    {
        IAccountState Deposit(Action addToBalance);
        IAccountState Withdaw(Action substructFromBalance);
        IAccountState Freeze();
        IAccountState HolderVerified();
        IAccountState Close();
    }

    public class Active : IAccountState
    {
        private Action OnUnfreeze { get; }

        public Active(Action onUnfreeze)
        {
            OnUnfreeze = onUnfreeze;
        }
        public IAccountState Deposit(Action addToBalance)
        {
            addToBalance();
            return this;
        }

        public IAccountState Withdaw(Action substructFromBalance) 
        {
            substructFromBalance();
            return this;
        }

        public IAccountState Freeze() => new Frozen(OnUnfreeze);

        public IAccountState HolderVerified() => this;        

        public IAccountState Close()
        {
            throw new NotImplementedException();
        }
    }

    public class Frozen : IAccountState
    {
        private Action OnUnfreeze { get; }

        public Frozen(Action onUnfreeze)
        {
            OnUnfreeze = onUnfreeze;
        }
        public IAccountState Deposit(Action addToBalance)
        {
            OnUnfreeze();
            addToBalance();
            return new Active(OnUnfreeze);
        }

        public IAccountState Withdaw(Action subsctructFromBalance)
        {
            OnUnfreeze();
            subsctructFromBalance();
            return new Active(OnUnfreeze);
        }

        public IAccountState Freeze() => this;       

      

        public IAccountState HolderVerified()
        {
            throw new NotImplementedException();
        }

        public IAccountState Close()
        {
            throw new NotImplementedException();
        }

    }

    public class Closed : IAccountState
    {
        public IAccountState Close() => this;      
        public IAccountState Deposit(Action addToBalance) => this;
        public IAccountState Freeze() => this;
        public IAccountState HolderVerified() => this;
        public IAccountState Withdaw(Action subsctructFromBalance) => this;       
    }

    public class NotVerified : IAccountState
    {
        private Action OnUnfreeze { get; }

        public NotVerified(Action onUnfreeze)
        {
            OnUnfreeze = onUnfreeze;
        }
        public IAccountState Close() => new Closed();

        public IAccountState Deposit(Action addToBalance)
        {
            addToBalance();
            return this;
        }

        public IAccountState Freeze() => this;

        public IAccountState HolderVerified() => new Active(OnUnfreeze);

        public IAccountState Withdaw(Action substractFromBalance) => this;        
    }
}


namespace branching_demo_1
{
    [TestClass]
    public class EntryPoint
    {
        [TestMethod]
        public void Main()
        {
        }
    }

    //Requirements:
    // Money can be deposited at any time
    // money can be withdrawn only after the account owner's identity has been verified.

    // Account holder can close the account at any time.
    // Closed account doesn't allow deposit and withdraw

    // Account should be frozen if not used for some time.

    // Account will be unfrozen automatically on deposit or withdraw.
    // Unfreezing the account triggers a custom action.

    public class Account
    {
        public decimal Balance { get; private set; } // side node
        private bool IsVerified { get; set; }
        private bool IsClosed { get; set; }
      //  private bool IsFrozen { get; set; }

        private Action OnUnfreeze { get; }

        private Action ManageUnfreezing { get; set; }

        public Account(Action onUnfreeze)
        {
            OnUnfreeze = onUnfreeze;
            ManageUnfreezing = StayUnfrozen;
        }

        // #1: Deposit 10, Close, Deposit 1 - Balance  == 10
        // #2: Deposit 10, Deposit 1 - Balance  ==  11
        // #6: Deposit 10, Freeze, Deposit 1 - OnUnfreeze was called
        // #7: Deposit 10, Freeze, Deposit 1 - IsFrozen == false
        // #8: Deposit 10, Deposit 1 - OnUnfreeze was not called
        public void Deposite(decimal amount)
        {
            if (IsClosed)
            {
                return;
            }
            ManageUnfreezing ();

            Balance += amount;

            // Deposit money
        }

      

        //private void ManagingUnfreezing()
        //{
        //    if (IsFrozen)
        //    {
        //        Unfreeze();
        //    }
        //    else
        //    {
        //        StayUnfrozen();
        //    }
        //}

        private void StayUnfrozen()
        {
           
        }

        private void Unfreeze()
        {
    //        IsFrozen = false;
            OnUnfreeze();
            ManageUnfreezing = StayUnfrozen;
        }

        // #3: Deposit 10, Withdraw 1 - Balance == 10
        // #4: Deposit 10, Verify, Close, Withdraw 1 - Balance == 10
        // #5: Deposit 10, Verify, Withdraw 1 - Balance == 9
        // #9: Deposit 10, Verify, Freeze, Withdraw 1 - OnUnfreeze was called
        // #10: Deposit 10, Verify, Freeze, Withdraw 1, - IsFrozen == false
        // #11: Deposit 10, Verify, Withdraw 1 - OnUnfreeze was called
        public void Withdraw(decimal amount)
        {
            if (!IsVerified) // if instruction in fresh code
            {
                return;// not verified
            }
            if (IsClosed)
            {
                return;
            }
            ManageUnfreezing();

            Balance -= amount;
            // withdraw money
        }

        public void HolderVerified()
        {
            IsVerified = true;
        }

        public void Close()
        {
            IsClosed = true;
        }

        public void Freeze()
        {
            if (IsClosed)
            {
                return; // Account must not be closed
            }
            if (!IsVerified)
            {
                return; // Account must be verified
            }
      //      this.IsFrozen = true;
            ManageUnfreezing = Unfreeze;
        }
    }
}

namespace branching_demo
{
    [TestClass]
    public class EntryPoint
    {
        [TestMethod]
        public void Main()
        {
        }
    }

    //Requirements:
    // Money can be deposited at any time
    // money can be withdrawn only after the account owner's identity has been verified.

    // Account holder can close the account at any time.
    // Closed account doesn't allow deposit and withdraw

    // Account should be frozen if not used for some time.

    // Account will be unfrozen automatically on deposit or withdraw.
    // Unfreezing the account triggers a custom action.

    public class Account
    {
        public decimal Balance { get; private set; } // side node
        private bool IsVerified { get; set; }
        private bool IsClosed { get; set; }
        private bool IsFrozen { get; set; }

        private Action OnUnfreeze { get; }

        public Account(Action onUnfreeze)
        {
            OnUnfreeze = onUnfreeze;
        }

        // #1: Deposit 10, Close, Deposit 1 - Balance  == 10
        // #2: Deposit 10, Deposit 1 - Balance  ==  11
        // #6: Deposit 10, Freeze, Deposit 1 - OnUnfreeze was called
        // #7: Deposit 10, Freeze, Deposit 1 - IsFrozen == false
        // #8: Deposit 10, Deposit 1 - OnUnfreeze was not called
        public void Deposite(decimal amount)
        {
            if (IsClosed)
            {
                return;
            }
            if (IsFrozen)
            {
                IsFrozen = false;
                OnUnfreeze();
            }

            Balance += amount;

            // Deposit money
        }

        // #3: Deposit 10, Withdraw 1 - Balance == 10
        // #4: Deposit 10, Verify, Close, Withdraw 1 - Balance == 10
        // #5: Deposit 10, Verify, Withdraw 1 - Balance == 9
        // #9: Deposit 10, Verify, Freeze, Withdraw 1 - OnUnfreeze was called
        // #10: Deposit 10, Verify, Freeze, Withdraw 1, - IsFrozen == false
        // #11: Deposit 10, Verify, Withdraw 1 - OnUnfreeze was called
        public void Withdraw(decimal amount)
        {
            if (!IsVerified) // if instruction in fresh code
            {
                return;// not verified
            }
            if (IsClosed)
            {
                return;
            }
            if (IsFrozen)
            {
                IsFrozen = false;
                OnUnfreeze();
            }

            Balance -= amount;
            // withdraw money
        }

        public void HolderVerified()
        {
            IsVerified = true;
        }

        public void Close()
        {
            IsClosed = true;
        }

        public void Freeze()
        {
            if (IsClosed)
            {
                return; // Account must not be closed
            }
            if (!IsVerified)
            {
                return; // Account must be verified
            }
            this.IsFrozen = true;
        }
    }
}