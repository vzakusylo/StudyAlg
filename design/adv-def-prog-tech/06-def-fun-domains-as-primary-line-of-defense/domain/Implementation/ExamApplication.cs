namespace def_fun_domains_as_primary_line_of_defense.Implementation
{
    public class ExamApplication : IExamApplication
    {
        public Subject OnSubject { get; }
        public Models.Professor AdministratedBy { get; }
        public Student TakenBy { get; }

        public IExam ForExam => throw new System.NotImplementedException();

        public ExamApplication(Subject subject, Models.Professor admin, Student candidate)
        {
            OnSubject = subject;
            AdministratedBy = admin;
            TakenBy = candidate;
        }
    }

}
