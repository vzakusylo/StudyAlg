﻿namespace def_fun_domains_as_primary_line_of_defense
{
    public interface IExamApplication
    {
        IExam ForExam { get; }
        Student TakenBy { get; }
        Models.Professor AdministratedBy { get; }
        Subject OnSubject { get; }
    }
}
