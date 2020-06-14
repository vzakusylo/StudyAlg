using JetBrains.Annotations;
using Course.Domain.Models;

namespace Course.Domain.Implementation
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

        public IExam Substitute(Professor administrator) =>
            new Exam(this.OnSubject, administrator);
    }
}