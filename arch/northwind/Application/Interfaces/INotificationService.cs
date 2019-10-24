using System.Threading.Tasks;
using Application.Notifications;

namespace Application.Interfaces
{
    public interface INotificationService
    {
        Task SendAsync(Message message);
    }
}