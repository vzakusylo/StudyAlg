using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;

namespace def_fun_domains_as_primary_line_of_defense
{
    public interface ICandidateSelector
    {
        IEnumerable<IExamApplication> Filter(IEnumerable<Student> candidates, IExam exam);
    }

    public class CandidateSelector : ICandidateSelector
    {
        public IEnumerable<IExamApplication> Filter([NotNull] IEnumerable<Student> candidates, [NotNull] IExam exam) =>
            candidates
                    .Where(candidate => CanApply(candidate, exam))
                    .Select(candidate => 
                        new Implementation.ExamApplication(
                                exam.OnSubject, exam.AdministratedBy, candidate));

        private bool CanApply(Student candidate, IExam exam)
            => candidate.Enrolled == exam.OnSubject.TaughtDuring &&
            !candidate.HasPassedExam(exam.OnSubject);
    }

    public interface IExamRules
    {
       // bool CanApply(Student candidate, IExam exam);
      //  IExamApplication Apply(Student candidate, IExam exam);

        void WhenAplicable(Student candidate, IExam exam, Action<IExamApplication> action);
    }

    public class RegularExamRules : IExamRules
    {
        private IExamApplication Apply(Student candidate, IExam exam)
        {
            if (!CanApply(candidate, exam))
                throw new ArgumentException();

            return new Implementation.ExamApplication(exam.OnSubject, exam.AdministratedBy, candidate);
        }

        private bool CanApply(Student candidate, IExam exam)
            => candidate.Enrolled == exam.OnSubject.TaughtDuring &&
            !candidate.HasPassedExam(exam.OnSubject);

        public void WhenAplicable(
            [NotNull] Student candidate, 
            [NotNull] IExam exam, 
            [NotNull] Action<IExamApplication> action)
        {
            if (CanApply(candidate, exam))
            {
                IExamApplication app = new Implementation.ExamApplication(exam.OnSubject, exam.AdministratedBy, candidate);
                action(app);
            }
        }
    }
}
