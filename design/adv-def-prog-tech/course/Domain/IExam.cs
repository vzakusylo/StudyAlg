using Course.Models;

namespace Course
{
    public interface IExam
    {
        Subject OnSubject { get; }
        Professor AdministratedBy { get; }
    }
}