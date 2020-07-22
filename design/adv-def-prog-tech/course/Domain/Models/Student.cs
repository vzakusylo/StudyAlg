using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using PersonalName = Course.Domain.Models.PersonalName;
using Professor = Course.Domain.Models.Professor;

namespace Course.Domain
{
    public abstract class Student
    {
        public PersonalName Name { get; }
        public Semester Enrolled { get; private set; }
       
        public List<IExamApplication> Exams { get; private set; }

        public Student(PersonalName name)
        {           
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public void AddGrade(Subject subject, Grade grade)
        {
            if (grade == null)            
                throw new ArgumentException(nameof(grade));
            if (subject == null || !IsEnlistedFor(subject))
                throw new ArgumentException(nameof(grade));
            //if (Grades.ContainsKey(subject) && Grades[subject] != Grade.F)
            //    throw new ArgumentException();

            //Grades[subject] = grade;
        }

        // Optional (Maybe) in functional languages either contains a value or is none
        //public optional Grade GetGrade(Subject subject)
        //{
        //    Grade grade;
        //    if (Grades.TryGetValue(subject, out grade))
        //    {
        //        return some grade;
        //    }
        //    return none;
        //}

        //public Grade GetGrade_Bad(Subject subject)
        //{
        //    Grade grade;
        //    if (Grades.TryGetValue(subject, out grade))
        //    {
        //        return grade;
        //    }
        //    return null;
        //}

        // With Grade on subject do this
        public void WithGrade(Subject subject, Action<Grade> doThis)
        {
            //Grade grade;
            //if (Grades.TryGetValue(subject, out grade))
            //{
            //    doThis(grade);
            //}
        }

        internal bool HasPassedExam(Subject onSubject)
        {
            throw new NotImplementedException();
        }

        //public double AverageGrade => Grades.Values
        //    .Select(grade => grade.NumericEquivalent)
        //    .Where(value => value > 0)
        //    .Average();
        
        private bool IsEnlistedFor(Subject subject) => true;

        public abstract bool CanEnroll(Semester semester);       

        public void Enroll(Semester semester)
        {
            if (!CanEnroll(semester))
            {
                throw new ArgumentException();
            }
            Enrolled = semester;
        }

        // Exam object bundles subject object together with a professor  
        public bool CanApplyFor([NotNull] IExam exam) =>
               Enrolled == exam.OnSubject.TaughtDuring ||
               HasPassedExam(exam.OnSubject);

        public IExamApplication ApplyFor([NotNull] IExam exam)
        {
            ExamApplicationBuilder builder = new ExamApplicationBuilder();
            builder.OnSubject(exam.OnSubject);
            builder.AdministratedBy(exam.AdministratedBy);
            builder.TakenBy(this);

            IExamApplication application = builder.Build();
            this.Exams.Add(application);

            return application;
                        
            //if (!CanApplyFor(exam))  
            //    throw new ArgumentException();

            //return new Implementation.ExamApplication(new Exam(exam.OnSubject, exam.AdministratedBy), this);
        }

        public bool CanSubstituteOnExam(Professor newAdministrator, Subject onSubject)
        {
            return Exams.FirstOrNone(app => app.ForExam.OnSubject == onSubject)
                .Map(app => app.For(app.ForExam.Substitute(newAdministrator)))
                .Map(Validate)
                .Reduce(false);
        }

        public void SubstituteOnExam(Professor newAdministrator, Subject onSubject)
        {
            if (!this.CanSubstituteOnExam(newAdministrator, onSubject))
                throw new ArgumentException();

            this.Exams
                .Select(app => app.ForExam.OnSubject == onSubject ?
                app.For(app.ForExam.Substitute(newAdministrator)) :
                    app)
                .ToList();
        }

        private bool Validate(IExamApplication app) => true;

        public bool CanAssign(Grade grade, IExam onExam) =>
            this.Exams
                .FirstOrNone(app => app.ForExam == onExam)
                .Map(CanAssignGrade)
                .Reduce(false);

        private bool CanAssignGrade(IExamApplication onExam) =>
            onExam.Grade.Map(_ => false).Reduce(true);

        public void Assign(Grade grade, IExam onExam)
        {
            if (!this.CanAssign(grade, onExam))
            {
                throw new ArgumentException();
            }

            Exams = Exams
                .Select(exam => exam.ForExam == onExam ? exam.With(grade) : exam)
                .ToList();
        }


        // Returns factory function for the exam application
        public Func<IExamApplication> GetExamApplicationFactory(Subject examOn, Professor administratedBy)
        {
            ExamApplicationBuilder builder = new ExamApplicationBuilder();
            builder.OnSubject(examOn);
            builder.AdministratedBy(administratedBy);
            builder.TakenBy(this);

            if (builder.CanBuild())
            {
                return builder.Build;
            }

            // think of something smarter;
            throw new ArgumentException();
        }

        public bool PassedExam(Subject subject)
        {
            throw new NotImplementedException();
        }

      

        private IExamApplication FindApplication(Subject onSubject) =>
            this.Exams.First(app => app.ForExam.OnSubject == onSubject);
       
    }
}
