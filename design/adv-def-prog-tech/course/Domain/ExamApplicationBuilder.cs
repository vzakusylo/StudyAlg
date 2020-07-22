using System;
using Course.Domain.Implementation;
using Course.Domain.Models;

namespace Course.Domain
{
    class ExamApplicationBuilder
    {
        private Professor Administrator { get; set; }
        private Subject Subject { get; set; }

        private Student Candidate { get; set; }

        public void AdministratedBy(Models.Professor professor)
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

            return new ExamApplication(new Exam(Subject, Administrator), Candidate);
        }
    }
}
