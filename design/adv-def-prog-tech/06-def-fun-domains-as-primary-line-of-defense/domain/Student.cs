using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using remove_corruption_consistent_obj;
using def_fun_domains_as_primary_line_of_defense.Models;
using PersonalName = def_fun_domains_as_primary_line_of_defense.Models.PersonalName;
using Professor = def_fun_domains_as_primary_line_of_defense.Models.Professor;

namespace def_fun_domains_as_primary_line_of_defense
{

    public abstract class Student
    {
        public PersonalName Name { get; }
        public Semester Enrolled { get; private set; }
        public Dictionary<Subject, Grade> Grades { get; }

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
            if (Grades.ContainsKey(subject) && Grades[subject] != Grade.F)
                throw new ArgumentException();

            Grades[subject] = grade;
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

        public Grade GetGrade_Bad(Subject subject)
        {
            Grade grade;
            if (Grades.TryGetValue(subject, out grade))
            {
                return grade;
            }
            return null;
        }

        // With Grade on subject do this
        public void WithGrade(Subject subject, Action<Grade> doThis)
        {
            Grade grade;
            if (Grades.TryGetValue(subject, out grade))
            {
                doThis(grade);
            }
        }

        internal bool HasPassedExam(Subject onSubject)
        {
            throw new NotImplementedException();
        }

        public double AverageGrade => Grades.Values
            .Select(grade => grade.NumericEquivalent)
            .Where(value => value > 0)
            .Average();

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
            if (!CanApplyFor(exam))  
                throw new ArgumentException();

            return new Implementation.ExamApplication(exam.OnSubject, exam.AdministratedBy, this);
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
    }

}
