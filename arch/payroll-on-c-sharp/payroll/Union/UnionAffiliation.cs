using System;
using System.Collections;

namespace payroll.Union
{
    public class UnionAffiliation : IAffiliation
    {
        private readonly Hashtable _serviceCharges = new Hashtable();

        public UnionAffiliation() { }

        public UnionAffiliation(int memberId, double dues)
        {
            MemberId = memberId;
            Dues = dues;
        }

        public double Dues { get; }
        public int MemberId { get; }

        public ServiceCharge GetServiceCharge(DateTime dateTime)
        {
            return (ServiceCharge)_serviceCharges[dateTime];
        }

        public void AddServiceCharge(ServiceCharge serviceCharge)
        {
            _serviceCharges.Add(serviceCharge.Time, serviceCharge);
        }

        public double CalculateDeductions(Paycheck pc)
        {
            return pc.Deduction;
        }
    }
}