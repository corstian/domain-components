using Domain.Components.Abstractions;
using Domain.Components.Experiment;

namespace Domain.Components
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCommandPackage"></typeparam>
    public class ServiceResult<TCommandPackage> : IServiceResult<TCommandPackage>
        where TCommandPackage : ICommandPackage
    {
        public TCommandPackage CommandPackage { get; init; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCommandPackage1"></typeparam>
    /// <typeparam name="TCommandPackage2"></typeparam>
    public class ServiceResult<TCommandPackage1, TCommandPackage2> : IServiceResult<TCommandPackage1, TCommandPackage2>
        where TCommandPackage1 : ICommandPackage
        where TCommandPackage2 : ICommandPackage
    {
        public TCommandPackage1 CommandPackage1 { get; init; }
        public TCommandPackage2 CommandPackage2 { get; init; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCommandPackage1"></typeparam>
    /// <typeparam name="TCommandPackage2"></typeparam>
    /// <typeparam name="TCommandPackage3"></typeparam>
    public class ServiceResult<TCommandPackage1, TCommandPackage2, TCommandPackage3> : IServiceResult<TCommandPackage1, TCommandPackage2, TCommandPackage3>
        where TCommandPackage1 : ICommandPackage
        where TCommandPackage2 : ICommandPackage
        where TCommandPackage3 : ICommandPackage
    {
        public TCommandPackage1 CommandPackage1 { get; init; }
        public TCommandPackage2 CommandPackage2 { get; init; }
        public TCommandPackage3 CommandPackage3 { get; init; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCommandPackage1"></typeparam>
    /// <typeparam name="TCommandPackage2"></typeparam>
    /// <typeparam name="TCommandPackage3"></typeparam>
    /// <typeparam name="TCommandPackage4"></typeparam>
    public class ServiceResult<TCommandPackage1, TCommandPackage2, TCommandPackage3, TCommandPackage4> : IServiceResult<TCommandPackage1, TCommandPackage2, TCommandPackage3, TCommandPackage4>
        where TCommandPackage1 : ICommandPackage
        where TCommandPackage2 : ICommandPackage
        where TCommandPackage3 : ICommandPackage
        where TCommandPackage4 : ICommandPackage
    {
        public TCommandPackage1 CommandPackage1 { get; init; }
        public TCommandPackage2 CommandPackage2 { get; init; }
        public TCommandPackage3 CommandPackage3 { get; init; }
        public TCommandPackage4 CommandPackage4 { get; init; }
    }
}
