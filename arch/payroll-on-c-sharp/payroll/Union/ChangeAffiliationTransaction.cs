using System.Threading.Tasks;
using payroll.ChangeEmployee;

namespace payroll.Union
{
    public abstract class ChangeAffiliationTransaction : ChangeEmployeeTransaction
    {
        protected ChangeAffiliationTransaction(int empId) : base(empId)
        {
        }

        protected override async Task ChangeAsync(Employee e)
        {
            await RecordMembershipAsync(e);
            IAffiliation affiliation = Affiliation;
            e.Affiliation = affiliation;
        }

        protected abstract IAffiliation Affiliation { get; }
        protected abstract Task RecordMembershipAsync(Employee e);
    }
}