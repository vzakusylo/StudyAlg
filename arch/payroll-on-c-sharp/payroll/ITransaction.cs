using System;
using System.Threading.Tasks;

namespace payroll
{
    public interface ITransaction
    {
        Task ExecuteAsync();
    }
}
