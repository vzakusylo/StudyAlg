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
            ManageUnfreezing();

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