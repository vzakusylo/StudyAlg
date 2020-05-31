namespace def_fun_domains_as_primary_line_of_defense
{
    public interface IExam
    {
        Subject OnSubject { get; }
        Professor AdministratedBy { get; }
    }
}