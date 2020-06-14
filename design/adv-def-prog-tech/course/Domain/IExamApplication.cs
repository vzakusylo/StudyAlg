namespace Course
{
    public interface IExamApplication
    {
        IExam ForExam { get; }
        Student TakenBy { get; }
        //Models.Professor AdministratedBy { get; }
        //Subject OnSubject { get; }
    }
}
