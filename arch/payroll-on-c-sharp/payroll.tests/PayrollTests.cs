using System;
using System.Threading.Tasks;
using payroll.AddEmployee;
using payroll.ChangeEmployee;
using Payroll.Classification;
using payroll.DeleteEmployee;
using payroll.Payment;
using payroll.PaymentSchedule;
using Payroll.PaymentSchedule;
using payroll.SalariedClassification;
using payroll.Union;
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

            Classification.PaymentClassification pc = e.Classification;
            Assert.True(pc is Payroll.Classification.SalariedClassification);
            Payroll.Classification.SalariedClassification sc = pc as Payroll.Classification.SalariedClassification;
            Assert.Equal(1000.00, sc.Salary);
            IPaymentSchedule ps = e.Schedule;
            Assert.True(ps is MonthlySchedule);

            IPaymentMethod pm = e.Method;
            Assert.True(pm is HoldMethod);
        }

        [Fact]
        public async Task DeleteEmployee()
        {
            int empId = 2;
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
            int empId = 3;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25);
            await t.ExecuteAsync();

            TimeCardTransaction tct = new TimeCardTransaction(new DateTime(2005, 7, 31), 8.0, empId);
            await tct.ExecuteAsync();

            Employee e = await PayrollDatabase.GetEmployeeAsync(empId);
            Assert.NotNull(e);

            Classification.PaymentClassification pc = e.Classification;
            Assert.True(pc is HourlyClassification);
            HourlyClassification hc = pc as HourlyClassification;

            TimeCard tc = hc.GetTimeCard(new DateTime(2005, 7, 31));
            Assert.NotNull(tc);
            Assert.Equal(8.0, tc.Hours);
        }

        [Fact]
        public async Task AddServiceCharge()
        {
            int empId = 4;
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
            int empId = 5;
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
            int empId = 6;
            AddCommissionedEmployee t = new AddCommissionedEmployee(empId, "Lance", "Home", 2500, 3.2);
            await t.ExecuteAsync();
            ChangeHourlyTransaction cht = new ChangeHourlyTransaction(empId, 27.52);
            await cht.ExecuteAsync();

            Employee e = await PayrollDatabase.GetEmployeeAsync(empId);
            Assert.NotNull(e);
            Classification.PaymentClassification pc = e.Classification;
            Assert.NotNull(pc);
            Assert.True(pc is HourlyClassification);
            HourlyClassification hc = pc as HourlyClassification;
            Assert.Equal(27.52, hc.HourlyRate);
            IPaymentSchedule ps = e.Schedule;
            Assert.True(ps is WeeklySchedule);
        }

        [Fact]
        public async Task ChangeSalariedTransaction()
        {
            int empId = 7;
            AddCommissionedEmployee t = new AddCommissionedEmployee(empId, "Lance", "Home", 2500, 3.2);
            await t.ExecuteAsync();
            ChangeSalariedTransaction cht = new ChangeSalariedTransaction(empId, 2500);
            await cht.ExecuteAsync();

            Employee e = await PayrollDatabase.GetEmployeeAsync(empId);
            Assert.NotNull(e);
            Classification.PaymentClassification pc = e.Classification;
            Assert.NotNull(pc);
            Assert.True(pc is Payroll.Classification.SalariedClassification);
            Payroll.Classification.SalariedClassification sc = pc as Payroll.Classification.SalariedClassification;
            Assert.Equal(2500, sc.Salary);
            IPaymentSchedule ps = e.Schedule;
            Assert.True(ps is BiWeeklySchedule);
        }

        [Fact]
        public async Task ChangeCommissionedTransaction()
        {
            int empId = 8;
            AddSalariedEmployee t = new AddSalariedEmployee(empId, "Lance", "Home", 2500);
            await t.ExecuteAsync();
            ChangeCommissionedTransaction cht = new ChangeCommissionedTransaction(empId, 2600, 4.4);
            await cht.ExecuteAsync();

            Employee e = await PayrollDatabase.GetEmployeeAsync(empId);
            Assert.NotNull(e);
            Classification.PaymentClassification pc = e.Classification;
            Assert.NotNull(pc);
            Assert.True(pc is CommissionedClassification);
            CommissionedClassification sc = pc as CommissionedClassification;
            Assert.Equal(2600, sc.BaseRate);
            Assert.Equal(4.4, sc.CommissionedRate);
            IPaymentSchedule ps = e.Schedule;
            Assert.True(ps is MonthlySchedule);
        }

        [Fact]
        public async Task ChangeUnionMember()
        {
            int empId = 9;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25);
            await t.ExecuteAsync();
            int memberId = 7743;
            ChangeMemberTransaction cmt = new ChangeMemberTransaction(empId, memberId, 99.42);
            await cmt.ExecuteAsync();
            Employee e = await PayrollDatabase.GetEmployeeAsync(empId);
            Assert.NotNull(e);
            IAffiliation affiliation = e.Affiliation;
            Assert.NotNull(affiliation);
            Assert.True(affiliation is UnionAffiliation);
            UnionAffiliation uf = affiliation as UnionAffiliation;
            Assert.Equal(99.42, uf.Dues);
            Employee member = await PayrollDatabase.GetUnionMemberAsync(memberId);
            Assert.NotNull(member);
            Assert.Equal(e,member);
        }

        [Fact]
        public async Task PaySingleSalariedEmployee()
        {
            int empId = 10;
            AddSalariedEmployee t = new AddSalariedEmployee(empId, "Bob", "Home", 1000.00);
            await t.ExecuteAsync();
            DateTime payDate = new DateTime(2001, 11, 30);
            PaydayTransaction pt = new PaydayTransaction(payDate);
            await pt.ExecuteAsync();
            Paycheck pc = pt.GetPaycheck(empId);
            Assert.NotNull(pc);
            Assert.Equal(payDate, pc.PayDate);
            Assert.Equal(1000, pc.GrossPay);
            Assert.Equal("Hold", pc.GetField("Disposition"));
            Assert.Equal(0, pc.Deduction);
            Assert.Equal(1000, pc.NetPay);
        }

        [Fact]
        public async Task PyaSingleSalariedEmployeeOnWrongDate()
        {
            int empId = 11;
            AddSalariedEmployee t = new AddSalariedEmployee(empId, "Bob", "Home", 1000);
            await t.ExecuteAsync();
            DateTime payDate = new DateTime(2001, 11, 29);
            PaydayTransaction pt = new PaydayTransaction(payDate);
            await pt.ExecuteAsync();
            Paycheck pc = pt.GetPaycheck(empId);
            Assert.Null(pc);
        }

        [Fact]
        public async Task PayingSingleHorlyEmployeeNoTimeCards()
        {
            int empId = 12;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25);
            await t.ExecuteAsync();
            DateTime payDate = new DateTime(2001, 11,9);
            PaydayTransaction pt = new PaydayTransaction(payDate);
            await pt.ExecuteAsync();
            ValidatePaycheck(pt, empId, payDate, 0.0);
        }

        private void ValidatePaycheck(PaydayTransaction pt, int empId, in DateTime payDate, double pay)
        {
            Paycheck pc = pt.GetPaycheck(empId);
            Assert.NotNull(pc);
            Assert.Equal(payDate, pc.PayDate);
            Assert.Equal(pay, pc.GrossPay);
            Assert.Equal("Hold", pc.GetField("Disposition"));
            Assert.Equal(0.0, pc.Deduction);
            Assert.Equal(pay,pc.NetPay);
        }

        [Fact]
        public async Task PaySingleHourlyEmployeeOneTimeCard()
        {
            int empId = 13;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25);
            await t.ExecuteAsync();
            DateTime payDate = new DateTime(2001, 11,9);
            TimeCardTransaction tc = new TimeCardTransaction(payDate, 2, empId);
            await tc.ExecuteAsync();
            PaydayTransaction pt = new PaydayTransaction(payDate);
            await pt.ExecuteAsync();
            ValidatePaycheck(pt, empId, payDate, 30.5);
        }

        [Fact]
        public async Task PaySingleHourlyEmployeeOvertimeOneTimeCard()
        {
            int empId = 14;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25);
            await t.ExecuteAsync();
            DateTime payDate = new DateTime(2001, 11, 9);
            TimeCardTransaction tc = new TimeCardTransaction(payDate, 9.0, empId);
            await tc.ExecuteAsync();
            PaydayTransaction pt = new PaydayTransaction(payDate);
            await pt.ExecuteAsync();
            ValidatePaycheck(pt, empId, payDate, (8 + 1.5) * 15.25);
        }

        [Fact]
        public async Task PaySingleHourlyEmployeeOnWrongDate()
        {
            int empId = 15;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25);
            await t.ExecuteAsync();
            DateTime payDate = new DateTime(2001, 11, 8);
            TimeCardTransaction tc = new TimeCardTransaction(payDate, 9.0, empId);
            await tc.ExecuteAsync();
            PaydayTransaction pt = new PaydayTransaction(payDate);
            await pt.ExecuteAsync();
            Paycheck pc = pt.GetPaycheck(empId);
            Assert.Null(pc);
        }

        [Fact]
        public async Task PaySingleHourlyEmployeeTwoTimeCards()
        {
            int empId = 16;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25);
            await t.ExecuteAsync();
            DateTime payDate = new DateTime(2001, 11,9);
            TimeCardTransaction tc = new TimeCardTransaction(payDate, 2.0, empId);
            await tc.ExecuteAsync();
            TimeCardTransaction tc2 = new TimeCardTransaction(payDate.AddDays(-1), 5, empId);
            await tc2.ExecuteAsync();
            PaydayTransaction pt = new PaydayTransaction(payDate);
            await pt.ExecuteAsync();
            ValidatePaycheck(pt, empId, payDate, 7*15.25);
        }

        [Fact]
        public async Task SalariedUnionMemberDues()
        {
            int empId = 17;
            AddSalariedEmployee t = new AddSalariedEmployee(empId, "Bill", "Home", 1000);
            await t.ExecuteAsync();
            int memberId = 7734;
            ChangeMemberTransaction cmt = new ChangeMemberTransaction(empId, memberId, 9.42);
            await cmt.ExecuteAsync();
            DateTime payDate = new DateTime(2001, 11, 30);
            PaydayTransaction pt = new PaydayTransaction(payDate);
            await pt.ExecuteAsync();
            Paycheck pc = pt.GetPaycheck(empId);
            Assert.Equal(payDate, pc.PayDate);
            Assert.Equal(1000, pc.GrossPay);
            Assert.Equal("Hold", pc.GetField("Disposition"));
            Assert.Equal(-1, pc.Deduction);
            Assert.Equal(1000 - -1, pc.NetPay);
        }

        [Fact]
        public async Task HourlyUnionMemberServiceChare()
        {
            int empId = 18;
            AddHourlyEmployee t = new AddHourlyEmployee(empId, "Bill", "Home", 15.24);
            await t.ExecuteAsync();
            int memberId = 7734;
            ChangeMemberTransaction cmt = new ChangeMemberTransaction(empId, memberId, 9.42);
            await cmt.ExecuteAsync();
            DateTime payDate = new DateTime(2001, 11, 9);
            ServiceChargeTransaction sct = new ServiceChargeTransaction(memberId, payDate, 19.42);
            await sct.ExecuteAsync();
            TimeCardTransaction tct = new TimeCardTransaction(payDate, 8.0, empId);
            await tct.ExecuteAsync();
            PaydayTransaction pt = new  PaydayTransaction(payDate);
            await pt.ExecuteAsync();
            Paycheck pc = pt.GetPaycheck(empId);

            Assert.Equal(payDate, pc.PayPeriodEndDate);
            Assert.Equal(8*15.24, pc.GrossPay);
            Assert.Equal("Hold", pc.GetField("Disposition"));
            Assert.Equal(9.42 * 19.42, pc.Deduction);
            Assert.Equal((8*15.24) - (9.42 * 19.42), pc.NetPay);
        }
    }
}
