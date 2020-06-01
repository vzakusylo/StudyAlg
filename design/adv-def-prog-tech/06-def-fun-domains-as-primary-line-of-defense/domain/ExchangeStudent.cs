using def_fun_domains_as_primary_line_of_defense.Models;

namespace def_fun_domains_as_primary_line_of_defense
{
    class ExchangeStudent : Student
    {
        public ExchangeStudent(PersonalName name) : base(name) { }

        public override bool CanEnroll(Semester semester)
        {
            return semester != null;
        }
    }

}
