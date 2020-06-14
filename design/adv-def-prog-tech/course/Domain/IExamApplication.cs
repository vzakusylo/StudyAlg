namespace Course.Domain
{
    public interface IExamApplication
    {
        IExam ForExam { get; }
        Student TakenBy { get; }  
        IExamApplication For(IExam exam);
        IExamApplication With(Grade grade);
    }
}
