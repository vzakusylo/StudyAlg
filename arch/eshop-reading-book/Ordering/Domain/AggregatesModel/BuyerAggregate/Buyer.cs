using Ordering.Domain.SeedWork;
using System;

namespace Ordering.Domain.AggregatesModel.BuyerAggregate
{
    public class Buyer : IAggregateRoot
    {
        private string userGuid;

        public Buyer(string userGuid)
        {
            this.userGuid = userGuid;
        }

        public Guid Id { get; internal set; }
        public string Name { get; internal set; }

        internal void VerifyOrAddPaymentMethod(int cardTypeId, string v, string cardNumber, string cardSecurityNumber, string cardHolderName, DateTime cardExpiration, int id)
        {
            throw new NotImplementedException();
        }
    }
}