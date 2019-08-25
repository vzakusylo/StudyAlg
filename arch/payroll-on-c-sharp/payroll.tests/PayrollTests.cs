using System;
using System.Threading.Tasks;
using payroll.AddEmployee;
using payroll.ChangeEmployee;
using payroll.DeleteEmployee;
using payroll.SalariedClassification;
using Xunit;

using SalariedClassification = payroll.SalariedClassification.SalariedClassification;

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

        [Fact]
        public async Task ChangeNameTransaction()
        {
            int empId = 2;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25);
            await t.ExecuteAsync();
            ChangeNameTransaction cnt = new ChangeNameTransaction(empId, "Bob");
            await cnt.ExecuteAsync();
            Employee e = await PayrollDatabase.GetEmployeeAsync(empId);
            Assert.NotNull(e);
            Assert.Equal("Bob", e.Name);
        }

        [Fact]
        public async Task ChangeHourlyTransaction()
        {
            int empId = 3;
            AddCommissionedEmployee t = new AddCommissionedEmployee(empId, "Lance", "Home", 2500, 3.2);
            await t.ExecuteAsync();
            ChangeHourlyTransaction cht = new ChangeHourlyTransaction(empId, 27.52);
            await cht.ExecuteAsync();

            Employee e = await PayrollDatabase.GetEmployeeAsync(empId);
            Assert.NotNull(e);
            PaymentClassification pc = e.Classification;
            Assert.NotNull(pc);
            Assert.True(pc is HourlyClassification);
            HourlyClassification hc = pc as HourlyClassification;
            Assert.Equal(27.52, hc.HourlyRate);
            PaymentSchedule ps = e.Schedule;
            Assert.True(ps is WeeklySchedule);
        }

        [Fact]
        public async Task ChangeSalariedTransaction()
        {
            int empId = 3;
            AddSalariedEmployee t = new AddSalariedEmployee(empId, "Lance", "Home", 2500);
            await t.ExecuteAsync();
            ChangeSalariedTransaction cht = new ChangeSalariedTransaction(empId);
            await cht.ExecuteAsync();

            Employee e = await PayrollDatabase.GetEmployeeAsync(empId);
            Assert.NotNull(e);
            PaymentClassification pc = e.Classification;
            Assert.NotNull(pc);
            Assert.True(pc is SalariedClassification.SalariedClassification);
            SalariedClassification.SalariedClassification sc = pc as SalariedClassification.SalariedClassification;
            Assert.Equal(2500, sc.Salary);
            PaymentSchedule ps = e.Schedule;
            Assert.True(ps is MonthlySchedule);
        }


        [Fact]
        public async Task ChangeCommissionedTransaction()
        {
            int empId = 3;
            AddCommissionedEmployee t = new AddCommissionedEmployee(empId, "Lance", "Home", 2500, 15);
            await t.ExecuteAsync();
            ChangeCommissionedTransaction cht = new ChangeCommissionedTransaction(empId);
            await cht.ExecuteAsync();

            Employee e = await PayrollDatabase.GetEmployeeAsync(empId);
            Assert.NotNull(e);
            PaymentClassification pc = e.Classification;
            Assert.NotNull(pc);
            Assert.True(pc is SalariedClassification.SalariedClassification);
            SalariedClassification.SalariedClassification sc = pc as SalariedClassification.SalariedClassification;
            Assert.Equal(2500, sc.Salary);
            PaymentSchedule ps = e.Schedule;
            Assert.True(ps is MonthlySchedule);
        }
    }
}
