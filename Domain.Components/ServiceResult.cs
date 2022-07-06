using Domain.Components.Abstractions;
using Domain.Components.Experiment;

namespace Domain.Components
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCommandPackage"></typeparam>
    public class ServiceResult<TComposable> : IServiceResult<TComposable>
        where TComposable : IComposable
    {
        public TComposable Composable { get; init; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCommandPackage1"></typeparam>
    /// <typeparam name="TCommandPackage2"></typeparam>
    public class ServiceResult<TComposable1, TComposable2> : IServiceResult<TComposable1, TComposable2>
        where TComposable1 : IComposable
        where TComposable2 : IComposable
    {
        public TComposable1 Composable1 { get; init; }
        public TComposable2 Composable2 { get; init; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCommandPackage1"></typeparam>
    /// <typeparam name="TCommandPackage2"></typeparam>
    /// <typeparam name="TCommandPackage3"></typeparam>
    public class ServiceResult<TComposable1, TComposable2, TComposable3> : IServiceResult<TComposable1, TComposable2, TComposable3>
        where TComposable1 : ICommandPackage
        where TComposable2 : ICommandPackage
        where TComposable3 : ICommandPackage
    {
        public TComposable1 Composable1 { get; init; }
        public TComposable2 Composable2 { get; init; }
        public TComposable3 Composable3 { get; init; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCommandPackage1"></typeparam>
    /// <typeparam name="TCommandPackage2"></typeparam>
    /// <typeparam name="TCommandPackage3"></typeparam>
    /// <typeparam name="TCommandPackage4"></typeparam>
    public class ServiceResult<TComposable1, TComposable2, TComposable3, TComposable4> : IServiceResult<TComposable1, TComposable2, TComposable3, TComposable4>
        where TComposable1 : ICommandPackage
        where TComposable2 : ICommandPackage
        where TComposable3 : ICommandPackage
        where TComposable4 : ICommandPackage
    {
        public TComposable1 Composable1 { get; init; }
        public TComposable2 Composable2 { get; init; }
        public TComposable3 Composable3 { get; init; }
        public TComposable4 Composable4 { get; init; }
    }
}
