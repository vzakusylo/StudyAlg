using Course.Domain.Models;

namespace Course.Domain
{
    public interface IExam
    {
        Subject OnSubject { get; }
        Professor AdministratedBy { get; }

        IExam Substitute(Professor administrator);
    }
}