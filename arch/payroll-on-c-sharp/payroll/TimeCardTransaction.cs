using System;
using System.Threading.Tasks;
using payroll.SalariedClassification;

namespace payroll
{
    public class TimeCardTransaction : ITransaction
    {
        public DateTime Date { get; }
        public double Hours { get; }
        public int EmpId { get; }

        public TimeCardTransaction(DateTime dateTime, double hours, int empId)
        {
            Date = dateTime;
            Hours = hours;
            EmpId = empId;
        }

        public async Task ExecuteAsync()
        {
            Employee e = await PayrollDatabase.GetEmployeeAsync(EmpId);
            if (e == null)
            {
                throw new InvalidOperationException("Could not find employee");
            }

            if (e.Classification is HourlyClassification hc)
            {
                await hc.AddTimeCardAsync(new TimeCard(Date, Hours));
            }
            else
            {
                throw new InvalidOperationException("Can't add time card to non hour employee");
            }
        }
    }
}