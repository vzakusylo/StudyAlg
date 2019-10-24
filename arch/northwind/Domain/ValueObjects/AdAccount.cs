using System;
using System.Collections.Generic;
using Domain.Exceptions;
using Domain.Infrastructure;

namespace Domain.ValueObjects
{
    public class AdAccount : ValueObject
    {
        private AdAccount()
        {

        }

        public static AdAccount For(string accountString)
        {
            var account = new AdAccount();

            try
            {
                account.Domain = "domain";
                account.Name = "name";
            }
            catch (Exception e)
            {
                throw new AdAccountInvalidException(accountString, e);
            }

            return account;
        }

        public string Domain { get; private set; }
        public string Name { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Domain;
            yield return Name;
        }
    }
}
