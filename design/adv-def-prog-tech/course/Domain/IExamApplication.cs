using System.Linq;

namespace Course.Domain
{
    public interface IExamApplication
    {
        IExam ForExam { get; }
        Student TakenBy { get; }
        Option<Grade> Grade { get; }
        IExamApplication For(IExam exam);
        IExamApplication With(Grade grade);
    }
}
