using System;
using System.Collections;


namespace payroll
{
    public class UnionAffiliation
    {
        private Hashtable _serviceCharges = new Hashtable();

        public ServiceCharge GetServiceCharge(DateTime dateTime)
        {
            return (ServiceCharge)_serviceCharges[dateTime];
        }

        public void AddServiceCharge(ServiceCharge serviceCharge)
        {
            _serviceCharges.Add(serviceCharge.Time, serviceCharge);
        }
    }
}