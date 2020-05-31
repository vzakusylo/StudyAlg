using System;

namespace def_fun_domains_as_primary_line_of_defense
{
    class ExamApplicationBuilder
    {
        private Professor Administrator { get; set; }
        private Subject Subject { get; set; }

        private Student Candidate { get; set; }

        public void AdministratedBy(Professor professor)
        {
            Administrator = professor;
        }

        public void OnSubject(Subject subject)
        {
            Subject = subject;
        }

        public void TakenBy(Student candidate)
        {
            Candidate = candidate;
        }

        public bool CanBuild() =>
            Administrator != null &&
            Subject != null &&
            Candidate != null &&
            Candidate.Enrolled != Subject.TaughtDuring &&
            !Candidate.PassedExam(Subject) &&
            Subject.TaughtBy == Administrator;

        public IExamApplication Build()
        {
            if (!CanBuild())
            {
                throw new InvalidOperationException();
            }

            return new ExamApplication(Subject, Administrator, Candidate);
        }

    }

}
