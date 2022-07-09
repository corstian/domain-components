using Domain.Components.Abstractions;

namespace Domain.Components.Experiment
{
    public interface IService<TComposable>
        where TComposable : IComposable
    {
        public Task<IResult<TComposable>> Evaluate();
    }

    public interface IService<TComposable1, TComposable2>
        where TComposable1 : IComposable
        where TComposable2 : IComposable
    {
        public Task<IResult<(TComposable1, TComposable2)>> Evaluate();
    }

    public interface IService<TComposable1, TComposable2, TComposable3>
        where TComposable1 : IComposable
        where TComposable2 : IComposable
        where TComposable3 : IComposable
    {
        public Task<IResult<(TComposable1, TComposable2, TComposable3)>> Evaluate();
    }

    public interface IService<TComposable1, TComposable2, TComposable3, TComposable4>
        where TComposable1 : IComposable
        where TComposable2 : IComposable
        where TComposable3 : IComposable
        where TComposable4 : IComposable
    {
        public Task<IResult<(TComposable1, TComposable2, TComposable3, TComposable4)>> Evaluate();
    }
}
