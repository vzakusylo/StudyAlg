using JetBrains.Annotations;

namespace def_fun_domains_as_primary_line_of_defense.Implementation
{
    public class Exam : IExam
    {
        public Subject OnSubject { get; }
        public Professor AdministratedBy { get; }

        public Exam([NotNull] Subject subject, [NotNull] Professor taughtBy)
        {
            this.OnSubject = subject;
            this.AdministratedBy = taughtBy;
        }
    }
}