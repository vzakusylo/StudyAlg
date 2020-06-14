using System;
using System.Linq;

namespace Course.Implementation
{
    public class ExamApplication : IExamApplication
    {
        public IExam ForExam { get; }
        public Student TakenBy { get; }

        public Option<Grade> Grade { get; }

        public ExamApplication(IExam forExam, Student candidate): this(forExam, candidate, Option.None<Grade>())
        {
            if (forExam is null)            
                throw new ArgumentNullException(nameof(forExam));            

            if (candidate is null)            
                throw new ArgumentNullException(nameof(candidate));
        }

        private ExamApplication(IExam forExam, Student candidate, Option<Grade> grade)
        {
            this.ForExam = forExam;
            this.TakenBy = candidate;
            this.Grade = grade;
        }

        public IExamApplication For(IExam exam) =>
            new ExamApplication(exam, this.TakenBy);

        public IExamApplication With(Grade grade) =>
            new ExamApplication(this.ForExam, this.TakenBy, Option.Some(grade));
       
    }
}
