using Course.Domain;
using Course.Domain.Models;


namespace Course
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
