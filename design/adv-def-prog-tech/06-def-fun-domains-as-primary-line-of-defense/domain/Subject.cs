using System;
using System.Collections.Generic;
using System.Linq;

namespace def_fun_domains_as_primary_line_of_defense
{
    public class Subject
    {
        public Semester TaughtDuring { get; }
        public Professor TaughtBy { get; }
        public Professor AssistedBy { get; }

        public string Name { get; }
        public List<Student> EnlistedStudents { get; private set; }

        public Subject(string name, Semester taughtDuring, Professor taughtBy, Professor teachingAssistant)
        {
            Name = name;
            TaughtDuring = taughtDuring;
            TaughtBy = taughtBy;
            AssistedBy = teachingAssistant;
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

        // BAD DESIGN - Object tells the caller whether it can perform an operation or not.
        public bool CanAdministerExam_BAD(Professor professor) =>
            professor == TaughtBy;

        // Implicit validation principle - One consisten object construct another consistent object. 
        // The process builds on data that are already valid.
        public IExam CanAdministerExam() =>
            new Implementation.Exam(this, TaughtBy);

    }
}
