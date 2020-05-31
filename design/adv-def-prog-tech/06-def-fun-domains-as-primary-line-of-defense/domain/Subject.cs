using System;
using System.Collections.Generic;
using System.Linq;

namespace def_fun_domains_as_primary_line_of_defense
{
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

}
