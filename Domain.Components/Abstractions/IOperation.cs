namespace Domain.Components.Abstractions
{
    internal interface IOperation : IServiceResult
    {
        // Public to provide results
        IAggregate Aggregate { get; }
        ICommandResult Result { get; }

        IEnumerable<IServiceResult> IServiceResult.Operations => new[] { this };

        ICommand Command { get; }

        internal Task<IResult<ICommandResult>> Evaluate();
        internal void SignalEvaluation(IResult<ICommandResult> result);
    }

    internal interface IOperation<TAggregate> : IOperation
        where TAggregate : class, IAggregate<TAggregate>
    {
        public new IAggregate<TAggregate> Aggregate { get; }
        IAggregate IOperation.Aggregate => Aggregate;

        public new ICommandResult<TAggregate> Result { get; }
        ICommandResult IOperation.Result => Result;

        internal new ICommand<TAggregate> Command { get; }
        ICommand IOperation.Command => this.Command;


        internal new Task<IResult<ICommandResult<TAggregate>>> Evaluate();
        async Task<IResult<ICommandResult>> IOperation.Evaluate()
        {
            var result = await Evaluate();

            return new DomainResult<ICommandResult>()
                .WithValue(result.IsSuccess
                    ? result.Value
                    : null)
                .WithReasons(result.Reasons);
        }

        internal void SignalEvaluation(IResult<ICommandResult<TAggregate>> result);

        void IOperation.SignalEvaluation(IResult<ICommandResult> result)
            => SignalEvaluation((IResult<ICommandResult<TAggregate>>)result);
    }

    internal interface IOperation<TAggregate, TResult> : IOperation<TAggregate>
        where TAggregate : class, IAggregate<TAggregate>
        where TResult : class, ICommandResult<TAggregate>
    {
        public new TResult Result { get; }
        ICommandResult<TAggregate> IOperation<TAggregate>.Result => this.Result;

        internal new ICommand<TAggregate, TResult> Command { get; }
        ICommand<TAggregate> IOperation<TAggregate>.Command => this.Command;

        internal new Task<IResult<TResult>> Evaluate();
        async Task<IResult<ICommandResult<TAggregate>>> IOperation<TAggregate>.Evaluate() {
            var result = await Evaluate();

            return new DomainResult<ICommandResult<TAggregate>>()
                .WithValue(result.IsSuccess
                    ? result.Value
                    : null)
                .WithReasons(result.Reasons);
        }

        internal void SignalEvaluation(IResult<TResult> result);
        void IOperation<TAggregate>.SignalEvaluation(IResult<ICommandResult<TAggregate>> result)
            => SignalEvaluation(new DomainResult<TResult>()
                .WithValue(result.IsSuccess
                    ? result.Value as TResult
                    : null)
                .WithReasons(result.Reasons));
    }
}
