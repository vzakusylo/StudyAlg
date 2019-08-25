using System.Threading.Tasks;

namespace payroll.Union
{
    public class ChangeUnaffiliatedTransaction : ChangeAffiliationTransaction {
        public ChangeUnaffiliatedTransaction(int empId) : base(empId)
        {
        }

        protected override IAffiliation Affiliation => new NoAffiliation();
        protected override Task RecordMembershipAsync(Employee e)
        {
            IAffiliation affiliation = e.Affiliation;
            if (!(affiliation is UnionAffiliation)) return Task.CompletedTask;

            UnionAffiliation unionAffiliation = affiliation as UnionAffiliation;
            int memeberId = unionAffiliation.MemberId;
            PayrollDatabase.RemoveUnionMember(memeberId);
            return Task.CompletedTask;
        }
    }
}