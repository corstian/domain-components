namespace Domain.Components.Abstractions
{
    public interface ISnapshot<T>
        where T : IAggregate
    {
        public void Populate(T aggregate);
    }
}
