namespace def_fun_domains_as_primary_line_of_defense
{
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

}
