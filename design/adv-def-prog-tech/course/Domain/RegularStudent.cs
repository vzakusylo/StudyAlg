using Course.Models;

namespace Course
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
