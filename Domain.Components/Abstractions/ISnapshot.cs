namespace Domain.Components.Abstractions
{
    public interface ISnapshot {
        public void Populate(object aggregate);
    }

    public interface ISnapshot<T> : ISnapshot
        where T : IAggregate
    {
        public void Populate(T aggregate);
        void ISnapshot.Populate(object aggregate) => Populate((T)aggregate);
    }
}
