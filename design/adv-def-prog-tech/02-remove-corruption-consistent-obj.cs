using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace remove_corruption_consistent_obj
{
    [TestClass]
    public class Solution
    {
        [TestMethod]
        public void Main()
        {
            ExamApplicationBuilder builder = new ExamApplicationBuilder();
            var app = builder.Build();
        }
    }

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
            Candidate.Enrolled != Subject.TaughDuring &&
            !Candidate.PassedExam(Subject) &&
            Subject.TaughBy == Administrator;

        public IExamApplication Build()
        {
            if (!CanBuild())
            {
                throw new InvalidOperationException();
            }

            return new ExamApplication(Subject, Administrator, Candidate);
        }

    }

    interface IExamApplication
    {
        Professor AdministratedBy { get; }
        Subject OnSubject { get; }
        Student TakenBy { get; }
    }

    class ExamApplication : IExamApplication
    {
        public Subject OnSubject { get; }
        public Professor AdministratedBy { get; }
        public Student TakenBy { get; }

        public ExamApplication(Subject subject, Professor admin, Student candidate)
        {
            OnSubject = subject;
            AdministratedBy = admin;
            TakenBy = candidate;
        }
    }

    class RegularStudent : Student
    {
        public RegularStudent(string name) : base(name) { }

        public override bool CanEnroll(Semester semester)
        {
            return semester != null && semester.Predecessor == base.Enrolled;
        }
    }

    class ExchangeStudent : Student
    {
        public ExchangeStudent(string name) : base(name) { }

        public override bool CanEnroll(Semester semester)
        {
            return semester != null;
        }
    }

    abstract class Student
    {
        public string Name { get; }
        public Semester Enrolled { get; private set; }

        public Student(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name can't be empty", nameof(name));
            }

            Name = name;
        }

        public abstract bool CanEnroll(Semester semester);
       

        public void Enroll(Semester semester)
        {
            if (!CanEnroll(semester))
            {
                throw new ArgumentException();
            }
            Enrolled = semester;
        }

        public IExamApplication ApplyFor(Subject examOn, Professor administratedBy)
        {
            ExamApplicationBuilder builder = new ExamApplicationBuilder();
            builder.OnSubject(examOn);
            builder.AdministratedBy(administratedBy);
            builder.TakenBy(this);

            if (builder.CanBuild())
            {
                 return builder.Build();
            }

            // think of something smarter;
            throw new ArgumentException();
        }

        // Returns factory function for the exam application
        public Func<IExamApplication> GetExamApplicationFactory(Subject examOn, Professor administratedBy)
        {
            ExamApplicationBuilder builder = new ExamApplicationBuilder();
            builder.OnSubject(examOn);
            builder.AdministratedBy(administratedBy);
            builder.TakenBy(this);

            return builder.Build;
        }

        public bool PassedExam(Subject subject)
        {
            throw new NotImplementedException();
        }
    }

    class Semester
    {
        public Semester Predecessor { get; internal set; }
    }

    class Professor { }

    class Subject
    {
        public Semester TaughDuring { get; internal set; }
        public Professor TaughBy { get; internal set; }
    }

}
