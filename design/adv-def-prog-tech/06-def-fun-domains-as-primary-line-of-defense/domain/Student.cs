using System;
using System.Collections.Generic;
using System.Linq;

namespace def_fun_domains_as_primary_line_of_defense
{
    abstract class Student
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

}
