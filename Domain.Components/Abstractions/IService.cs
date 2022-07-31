﻿namespace Domain.Components.Abstractions
{
    public interface IService
    {

    }
    
    public interface IService<TResult> : IService
        where TResult : IServiceResult<TResult>
    {
        public Task<IResult<IPromise<TResult>>> Stage(IServiceProvider serviceProvider);
    }
}
