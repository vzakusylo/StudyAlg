namespace payroll.AddEmployee
{
    public class ChangeNameTransaction : ChangeEmployeeTransaction
    {
        public string Name { get; }

        public ChangeNameTransaction(int empId, string name) : base(empId)
        {
            Name = name;
        }
        
        protected override void Change(Employee e)
        {
            e.Name = Name;
        }
    }
}