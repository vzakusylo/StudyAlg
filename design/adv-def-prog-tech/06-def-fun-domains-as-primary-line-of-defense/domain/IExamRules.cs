using System;

namespace def_fun_domains_as_primary_line_of_defense
{
    public interface IExamRules
    {
        bool CanApply(Student candidate, IExam exam);
        IExamApplication Apply(Student student, IExam exam);
    }

    public class RegularExamRules : IExamRules
    {
        public IExamApplication Apply(Student candidate, IExam exam)
        {
            if (!CanApply(candidate, exam))
                throw new ArgumentException();

            return new Implementation.ExamApplication(exam.OnSubject, exam.AdministratedBy, candidate);
        }

        public bool CanApply(Student candidate, IExam exam)
            => candidate.Enrolled == exam.OnSubject.TaughtDuring &&
            !candidate.HasPassedExam(exam.OnSubject);
    }
}
