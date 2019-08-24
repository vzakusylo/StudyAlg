using System;
using System.Threading.Tasks;

namespace payroll
{
    public class ServiceChargeTransaction : ITransaction
    {
        public int MemberId { get; }
        public DateTime Time { get; }
        public double Charge { get; }

        public ServiceChargeTransaction(int memberId, DateTime time, double charge)
        {
            MemberId = memberId;
            Time = time;
            Charge = charge;
        }
        
        public async Task ExecuteAsync()
        {
            Employee e = await PayrollDatabase.GetUnionMember(MemberId);
            if (e == null)
            {
                throw new InvalidOperationException("Unit member not found");
            }
            if (e.Affiliation == null)
            {
                throw new InvalidOperationException("Can't add payment for union charge");
            }

            e.Affiliation.AddServiceCharge(new ServiceCharge(Time, Charge));
        }
    }
}