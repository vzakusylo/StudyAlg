using System;
using System.Collections.Generic;
using System.Linq;
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
        public RegularStudent(PersonalName name) : base(name) { }

        public override bool CanEnroll(Semester semester)
        {
            return semester != null && semester.Predecessor == base.Enrolled;
        }
    }

    class ExchangeStudent : Student
    {
        public ExchangeStudent(PersonalName name) : base(name) { }

        public override bool CanEnroll(Semester semester)
        {
            return semester != null;
        }
    }

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

    class PersonalName
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleNames { get; set; }

        public PersonalName(string firstName, string middleNames, string lastName)
        {
            if (IsBadMandatoryPart(firstName) ||
                IsBadOptionalPart(MiddleNames) ||
                IsBadMandatoryPart(lastName))
                throw new ArgumentException();

            FirstName = firstName;
            MiddleNames = middleNames;
            LastName = lastName;
        }

        private bool IsBadOptionalPart(string part) => part == null ||
            part.Length > 0 && char.IsHighSurrogate(part[part.Length - 1]);

        private bool IsBadMandatoryPart(string part) =>
            IsBadOptionalPart(part) || part == string.Empty;

        public override bool Equals(object obj)
        {
            return Equals(obj as PersonalName);
        }

        private bool Equals(PersonalName other) =>
            other != null &&
            ArePartsEqual(other.FirstName, FirstName) &&
            ArePartsEqual(other.LastName, LastName) &&
            ArePartsEqual(other.MiddleNames, MiddleNames);


        public override int GetHashCode() =>
            FirstName.GetHashCode() ^
            MiddleNames.GetHashCode() ^
            LastName.GetHashCode();

        private bool ArePartsEqual(string part1, string part2) =>
            string.Compare(part1, part2, StringComparison.OrdinalIgnoreCase) == 0;
        // string.Equals(p.CountryCode, countryCode, StringComparison.OrdinalIgnoreCase)

    }

    class Semester
    {
        public Semester Predecessor { get; internal set; }
    }

    class Professor { }

    class Subject
    {
        public Semester TaughtDuring { get; internal set; }
        public Professor TaughtBy { get; internal set; }
        public string Name { get; }
        public List<Student> EnlistedStudents { get; private set; }

        public Subject(string name, Semester taughtDuring, Professor taughtBy)
        {
            Name = name;
            TaughtDuring = taughtDuring;
            TaughtBy = taughtBy;
        }

        public void Enlist(Student student) {
            if (student is null)
            {
                throw new ArgumentNullException(nameof(student));
            }

            EnlistedStudents.Add(student);
        }

        public void AssignGrades(IEnumerable<Tuple<PersonalName, Grade>> grades)
        {
            var listedGrades = grades
                .Select(tuple => new
                {
                    Student = EnlistedStudents.First(student =>  // Subject's only responsibility is to assign grades, not to compare names.
                        student.Name.Equals(tuple.Item1)),
                    Grade = tuple.Item2
                });

            foreach (var studentGrade in listedGrades)
            {
                studentGrade.Student.AddGrade(this, studentGrade.Grade);
            }
        }
    }

    public class Grade
    {
        private Grade(double numericEquivalent) { NumericEquivalent = numericEquivalent; }

        public double NumericEquivalent { get; }

        public static Grade A { get; } = new Grade(4);
        public static Grade B { get; } = new Grade(3);
        public static Grade C { get; } = new Grade(2);
        public static Grade D { get; } = new Grade(1);
        public static Grade F { get; } = new Grade(0);
    }

}
