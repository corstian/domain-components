using Domain.Components.Experiment;

namespace Domain.Components.Abstractions
{
    public interface IServiceResult : IComposable
    {

    }

    public interface IServiceResult<TComposable> : IServiceResult
        where TComposable : IComposable
    {
        public TComposable Composable { get; }
    }

    public interface IServiceResult<TComposable1, TComposable2> : IServiceResult
        where TComposable1 : IComposable
        where TComposable2 : IComposable
    {
        public TComposable1 Composable1 { get; }
        public TComposable2 Composable2 { get; }
    }

    public interface IServiceResult<TComposable1, TComposable2, TComposable3> : IServiceResult
        where TComposable1 : IComposable
        where TComposable2 : IComposable
        where TComposable3 : IComposable
    {
        public TComposable1 Composable1 { get; }
        public TComposable2 Composable2 { get; }
        public TComposable3 Composable3 { get; }
    }

    public interface IServiceResult<TComposable1, TComposable2, TComposable3, TComposable4> : IServiceResult
        where TComposable1 : IComposable
        where TComposable2 : IComposable
        where TComposable3 : IComposable
        where TComposable4 : IComposable
    {
        public TComposable1 Composable1 { get; }
        public TComposable2 Composable2 { get; }
        public TComposable3 Composable3 { get; }
        public TComposable4 Composable4 { get; }
    }
}
