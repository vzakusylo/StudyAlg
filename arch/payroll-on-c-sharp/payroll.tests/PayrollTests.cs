using System;
using System.Threading.Tasks;
using payroll.AddEmployee;
using payroll.DeleteEmployee;
using payroll.SalariedClassification;
using Xunit;

namespace payroll.tests
{
    public class PayrollTests
    {
        [Fact]
        public async Task AddSalariedEmployee()
        {
            int empId = 1;
            AddSalariedEmployee t = new AddSalariedEmployee(empId, "Bob", "Home", 1000.0);
            await t.ExecuteAsync();

            Employee e = await PayrollDatabase.GetEmployeeAsync(empId);
            Assert.Equal("Bob", e.Name);

            PaymentClassification pc = e.Classification;
            Assert.True(pc is SalariedClassification.SalariedClassification);
            SalariedClassification.SalariedClassification sc = pc as SalariedClassification.SalariedClassification;
            Assert.Equal(1000.00, sc.Salary);
            PaymentSchedule ps = e.Schedule;
            Assert.True(ps is MonthlySchedule);

            PaymentMethod pm = e.Method;
            Assert.True(pm is HoldMethod);
        }

        [Fact]
        public async Task DeleteEmployee()
        {
            int empId = 4;
            AddCommissionedEmployee t = new AddCommissionedEmployee(empId, "Bill", "Home", 2500, 3.2);
            await t.ExecuteAsync();

            Employee e = await PayrollDatabase.GetEmployeeAsync(empId);
            Assert.NotNull(e);

            DeleteEmployeeTransaction dt = new DeleteEmployeeTransaction(empId);
            await dt.ExecuteAsync();

            e = await PayrollDatabase.GetEmployeeAsync(empId);
            Assert.Null(e);
        }

        [Fact]
        public async Task TimeCardTransaction()
        {
            int empId = 5;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25);
            await t.ExecuteAsync();

            TimeCardTransaction tct = new TimeCardTransaction(new DateTime(2005, 7, 31), 8.0, empId);
            await tct.ExecuteAsync();

            Employee e = await PayrollDatabase.GetEmployeeAsync(empId);
            Assert.NotNull(e);

            PaymentClassification pc = e.Classification;
            Assert.True(pc is HourlyClassification);
            HourlyClassification hc = pc as HourlyClassification;

            TimeCard tc = hc.GetTimeCard(new DateTime(2005, 7, 31));
            Assert.NotNull(tc);
            Assert.Equal(8.0, tc.Hours);
        }

        [Fact]
        public async Task AddServiceCharge()
        {
            int empId = 2;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25);
            await t.ExecuteAsync();

            Employee e = await PayrollDatabase.GetEmployeeAsync(empId);
            Assert.NotNull(e);
            UnionAffiliation af = new UnionAffiliation();
            e.Affiliation = af;
            int memberId = 86; // Maxwell Smart
            await PayrollDatabase.AddUnionMemberAsync(memberId, e);
            ServiceChargeTransaction sct = new ServiceChargeTransaction(memberId, new DateTime(2005, 8, 8), 12.95);
            await sct.ExecuteAsync();
            ServiceCharge sc = af.GetServiceCharge(new DateTime(2005, 8, 8));
            Assert.NotNull(sc);
            Assert.Equal(12.95, sc.Amount);
        }
    }
}
