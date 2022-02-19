using FluentResults;

namespace Domain.Components.Abstractions
{
    public interface ICommand { }

    public interface ICommand<THandler> : ICommand
        where THandler : IAggregate<THandler>
    {

        //public Result Validate(THandler handler);
        //public IEnumerable<IEvent<THandler>> Evaluate(THandler handler);

        public IResult<IEnumerable<IEvent<THandler>>> Evaluate(THandler handler);
    }

    public interface ICommand<THandler, out TEvent> : ICommand
            where THandler : IAggregate<THandler>
            where TEvent : IEvent<THandler>
    {
        //public Result Validate(THandler handler);
        //public TEvent Evaluate(THandler handler);
        public IResult<TEvent> Evaluate(THandler handler);
    }

    public interface ICommand<THandler, TEvent1, TEvent2> : ICommand
        where THandler : IAggregate<THandler>
        where TEvent1 : IEvent<THandler>
        where TEvent2 : IEvent<THandler>
    {
        //public Result Validate(THandler handler);
        //public (TEvent1, TEvent2) Evaluate(THandler handler);

        public IResult<(TEvent1, TEvent2)> Evaluate(THandler handler);
    }
}
