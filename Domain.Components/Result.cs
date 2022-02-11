namespace Domain.Components
{
    public class Result<T>
    {
        public T? Value { get; init; }
        public IEnumerable<Notification> Notifications { get; init; } = new List<Notification>();

        public static Result<T> FromNotifications(IEnumerable<Notification> notifications)
        {
            return new Result<T>
            {
                Notifications = notifications
            };
        }

        public static Result<T> Fail(string reason)
        {
            return new Result<T>
            {
                Notifications = new List<Notification>
                {
                    new Notification
                    {
                        Description = reason
                    }
                }
            };
        }
    }
}
