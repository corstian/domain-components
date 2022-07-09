using Domain.Components.Abstractions;

namespace Domain.Components.Extensions
{
    public static class ServiceResultBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCommandPackage"></typeparam>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static ServiceResult<TCommandPackage>
            AddCommandPackage<TCommandPackage>(Func<CommandPackage, TCommandPackage> builder)
            where TCommandPackage : ICommandPackage

            => new ServiceResult<TCommandPackage>
            {
                Composable = builder.Invoke(new CommandPackage())
            };

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCommandPackage1"></typeparam>
        /// <typeparam name="TCommandPackage2"></typeparam>
        /// <param name="serviceResult"></param>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static ServiceResult<TCommandPackage1, TCommandPackage2>
            AddCommandPackage<TCommandPackage1, TCommandPackage2>(
                this ServiceResult<TCommandPackage1> serviceResult,
                Func<CommandPackage, TCommandPackage2> builder)
            where TCommandPackage1 : ICommandPackage
            where TCommandPackage2 : ICommandPackage
            => new ServiceResult<TCommandPackage1, TCommandPackage2>
            {
                Composable1 = serviceResult.Composable,
                Composable2 = builder.Invoke(new CommandPackage())
            };

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCommandPackage1"></typeparam>
        /// <typeparam name="TCommandPackage2"></typeparam>
        /// <typeparam name="TCommandPackage3"></typeparam>
        /// <param name="serviceResult"></param>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static ServiceResult<TCommandPackage1, TCommandPackage2, TCommandPackage3>
            AddCommandPackage<TCommandPackage1, TCommandPackage2, TCommandPackage3>(
                this ServiceResult<TCommandPackage1, TCommandPackage2> serviceResult,
                Func<CommandPackage, TCommandPackage3> builder)
            where TCommandPackage1 : ICommandPackage
            where TCommandPackage2 : ICommandPackage
            where TCommandPackage3 : ICommandPackage
            => new ServiceResult<TCommandPackage1, TCommandPackage2, TCommandPackage3>
            {
                Composable1 = serviceResult.Composable1,
                Composable2 = serviceResult.Composable2,
                Composable3 = builder.Invoke(new CommandPackage())
            };

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCommandPackage1"></typeparam>
        /// <typeparam name="TCommandPackage2"></typeparam>
        /// <typeparam name="TCommandPackage3"></typeparam>
        /// <typeparam name="TCommandPackage4"></typeparam>
        /// <param name="serviceResult"></param>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static ServiceResult<TCommandPackage1, TCommandPackage2, TCommandPackage3, TCommandPackage4>
            AddCommandPackage<TCommandPackage1, TCommandPackage2, TCommandPackage3, TCommandPackage4>(
                this ServiceResult<TCommandPackage1, TCommandPackage2, TCommandPackage3> serviceResult,
                Func<CommandPackage, TCommandPackage4> builder)
            where TCommandPackage1 : ICommandPackage
            where TCommandPackage2 : ICommandPackage
            where TCommandPackage3 : ICommandPackage
            where TCommandPackage4 : ICommandPackage
            => new ServiceResult<TCommandPackage1, TCommandPackage2, TCommandPackage3, TCommandPackage4>
            {
                Composable1 = serviceResult.Composable1,
                Composable2 = serviceResult.Composable2,
                Composable3 = serviceResult.Composable3,
                Composable4 = builder.Invoke(new CommandPackage())
            };

        
        public static ServiceResult<TCommandPackage1, TCommandPackage2>
            AddService<TCommandPackage1, TCommandPackage2>(Func<Task<IServiceResult<TCommandPackage1, TCommandPackage2>>> service)
            where TCommandPackage1 : ICommandPackage
            where TCommandPackage2 : ICommandPackage
        {
            throw new NotImplementedException();
        }
    }
}
