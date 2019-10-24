using System;
using Application.Tests.Infrastructure;
using Persistence;

namespace Application.Tests
{
    public class CommandTestBase : IDisposable
    {
        protected readonly NorthwindDbContext _context;
        public CommandTestBase()
        {
            _context = NorthwindContextFactory.Create();
        }

        public void Dispose()
        {
            NorthwindContextFactory.Destroy(_context);
        }
    }
}