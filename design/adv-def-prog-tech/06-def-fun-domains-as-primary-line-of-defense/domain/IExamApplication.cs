namespace def_fun_domains_as_primary_line_of_defense
{
    interface IExamApplication
    {
        Professor AdministratedBy { get; }
        Subject OnSubject { get; }
        Student TakenBy { get; }
    }

}
