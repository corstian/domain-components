using Domain.Components;

namespace Domain.Example.Aggregates.GroupAggregate
{
    public class Group : Aggregate<Group>
    {
        public string Name { get; internal set; } = "";

        internal List<Guid> _members = new List<Guid>();
        public IReadOnlyList<Guid> Members => _members.AsReadOnly();
    }
}
