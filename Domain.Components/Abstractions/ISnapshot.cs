namespace Domain.Components.Abstractions
{
    public interface ISnapshot { }

    public interface ISnapshot<T> : ISnapshot
        where T : IAggregate
    {
        public void Populate(T aggregate);
    }
}
