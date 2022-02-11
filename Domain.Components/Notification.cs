namespace Domain.Components
{
    public class Notification
    {
        public string? Description { get; init; }
        public string? Code { get; init; }
        public string? Source { get; init; }

        public Type Type { get; init; }
    }

    public enum Type
    {
        None = 0,
        Error = 1,
        Warning = 2,
        Authorization = 3
    }
}
