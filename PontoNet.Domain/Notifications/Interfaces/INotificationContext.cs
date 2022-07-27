using FluentValidation.Results;
using PontoNet.Domain.Notifications.Models;

namespace PontoNet.Domain.Notifications.Interfaces
{
    public interface INotificationContext
    {
        IReadOnlyCollection<Notification> Notifications { get; }
        bool HasNotifications { get; }

        void AddNotification(string message, NotificationType? type = null);
        void AddNotification(Notification notification);
        void AddNotifications(IReadOnlyCollection<Notification> notifications);
        void AddNotifications(IList<Notification> notifications);
        void AddNotifications(ICollection<Notification> notifications);
        void AddNotifications(ValidationResult validationResult);
    }
}