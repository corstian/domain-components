using Domain.Components.Experiment;

namespace Domain.Components.Abstractions
{
    public interface IServiceResult
    {

    }

    public interface IServiceResult<TCommandPackage> : IServiceResult
        where TCommandPackage : ICommandPackage
    {
        public TCommandPackage CommandPackage { get; }
    }

    public interface IServiceResult<TCommandPackage1, TCommandPackage2> : IServiceResult
        where TCommandPackage1 : ICommandPackage
        where TCommandPackage2 : ICommandPackage
    {
        public TCommandPackage1 CommandPackage1 { get; }
        public TCommandPackage2 CommandPackage2 { get; }
    }

    public interface IServiceResult<TCommandPackage1, TCommandPackage2, TCommandPackage3> : IServiceResult
        where TCommandPackage1 : ICommandPackage
        where TCommandPackage2 : ICommandPackage
        where TCommandPackage3 : ICommandPackage
    {
        public TCommandPackage1 CommandPackage1 { get; }
        public TCommandPackage2 CommandPackage2 { get; }
        public TCommandPackage3 CommandPackage3 { get; }
    }

    public interface IServiceResult<TCommandPackage1, TCommandPackage2, TCommandPackage3, TCommandPackage4> : IServiceResult
        where TCommandPackage1 : ICommandPackage
        where TCommandPackage2 : ICommandPackage
        where TCommandPackage3 : ICommandPackage
        where TCommandPackage4 : ICommandPackage
    {
        public TCommandPackage1 CommandPackage1 { get; }
        public TCommandPackage2 CommandPackage2 { get; }
        public TCommandPackage3 CommandPackage3 { get; }
        public TCommandPackage4 CommandPackage4 { get; }
    }
}
