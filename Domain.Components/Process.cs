using Domain.Components.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Components
{
    public class Process<T> : IProcess<T>
        where T : IProcess<T>
    {
        private readonly IServiceProvider _serviceProvider;

        public Process(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IRepository<T1> GetRepository<T1>() where T1 : IAggregate
            => _serviceProvider.GetRequiredService<IRepository<T1>>();

        public IService<T1> GetService<T1>() where T1 : IService<T1>
            => _serviceProvider.GetRequiredService<IService<T1>>();
    }
}
