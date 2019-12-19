using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.Domain.SeedWork
{
    public abstract class Enumeration : IComparable
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        protected Enumeration(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
