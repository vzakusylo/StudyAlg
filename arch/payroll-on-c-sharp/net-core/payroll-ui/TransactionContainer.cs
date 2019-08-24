using System.Collections;
using Payroll;

namespace PayrollUI
{
	public class TransactionContainer
	{
		public delegate void AddAction();

	    private readonly AddAction _addAction;

		public TransactionContainer(AddAction action)
		{
			_addAction = action;
		}

		public IList Transactions { get; } = new ArrayList();

	    public void Add(Transaction transaction)
		{
			Transactions.Add(transaction);
		    _addAction?.Invoke();
		}

		public void Clear()
		{
			Transactions.Clear();
		}
	}
}