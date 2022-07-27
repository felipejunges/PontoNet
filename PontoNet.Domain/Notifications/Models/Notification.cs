namespace PontoNet.Domain.Notifications.Models
{
    public class Notification
    {
        public string Message { get; }
        public NotificationType? Type { get; }

        public Notification(string message, NotificationType? type = null)
        {
            Message = message;
            Type = type;
        }
    }
}