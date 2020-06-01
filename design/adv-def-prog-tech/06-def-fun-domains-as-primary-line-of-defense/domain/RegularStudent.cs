using def_fun_domains_as_primary_line_of_defense.Models;

namespace def_fun_domains_as_primary_line_of_defense
{
    class RegularStudent : Student
    {
        public RegularStudent(PersonalName name) : base(name) { }

        public override bool CanEnroll(Semester semester)
        {
            return semester != null && semester.Predecessor == base.Enrolled;
        }
    }

}
