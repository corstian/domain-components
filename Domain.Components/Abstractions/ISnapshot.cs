namespace Domain.Components.Abstractions
{
    public interface ISnapshot<T>
        where T : IAggregate<T>
    {
        public void Populate(T aggregate);
    }
}
