using System.Threading.Tasks;

namespace payroll.Union
{
    public class ChangeMemberTransaction : ChangeAffiliationTransaction
    {
        public int MemberId { get; }
        public double Dues { get; }

        public ChangeMemberTransaction(int empId, int memberId, double dues):base(empId)
        {
            MemberId = memberId;
            Dues = dues;
        }

        protected override IAffiliation Affiliation => new UnionAffiliation(MemberId, Dues);

        protected override async Task RecordMembershipAsync(Employee e)
        {
            await PayrollDatabase.AddUnionMemberAsync(MemberId, e);
        }
    }
}