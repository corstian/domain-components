using Domain.Components.Abstractions;

namespace Domain.Components.Experiment
{
    public interface IService<TCommandPackage>
        where TCommandPackage : ICommandPackage
    {
        public Task<IServiceResult<TCommandPackage>> Evaluate();
    }

    public interface IService<TCommandPackage1, TCommandPackage2>
        where TCommandPackage1 : ICommandPackage
        where TCommandPackage2 : ICommandPackage
    {
        public Task<IServiceResult<TCommandPackage1, TCommandPackage2>> Evaluate();
    }

    public interface IService<TCommandPackage1, TCommandPackage2, TCommandPackage3>
        where TCommandPackage1 : ICommandPackage
        where TCommandPackage2 : ICommandPackage
        where TCommandPackage3 : ICommandPackage
    {
        public Task<IServiceResult<TCommandPackage1, TCommandPackage2, TCommandPackage3>> Evaluate();
    }

    public interface IService<TCommandPackage1, TCommandPackage2, TCommandPackage3, TCommandPackage4>
        where TCommandPackage1 : ICommandPackage
        where TCommandPackage2 : ICommandPackage
        where TCommandPackage3 : ICommandPackage
        where TCommandPackage4 : ICommandPackage
    {
        public Task<IServiceResult<TCommandPackage1, TCommandPackage2, TCommandPackage3, TCommandPackage4>> Evaluate();
    }
}
