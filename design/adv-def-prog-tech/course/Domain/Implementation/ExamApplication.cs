using System;

namespace Course.Implementation
{
    public class ExamApplication : IExamApplication
    {
        public IExam ForExam { get; }
        public Student TakenBy { get; }

        public ExamApplication(IExam forExam, Student candidate)
        {
            if (forExam is null)
            {
                throw new ArgumentNullException(nameof(forExam));
            }

            if (candidate is null)
            {
                throw new ArgumentNullException(nameof(candidate));
            }
            ForExam = forExam;
            TakenBy = candidate;
        }

        public IExamApplication For(IExam exam) =>
            new ExamApplication(exam, this.TakenBy);
    }
}
