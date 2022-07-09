using Domain.Components.Abstractions;

namespace Domain.Components.Extensions
{
    public static class CommandPackageBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TAggregate"></typeparam>
        /// <param name="commandPackage"></param>
        /// <param name="aggregate"></param>
        /// <returns></returns>
        public static CommandPackage<TAggregate> 
            Target<TAggregate>(
                this CommandPackage commandPackage, 
                TAggregate aggregate)

            where TAggregate : IAggregate

            => new CommandPackage<TAggregate>
            {
                Target = aggregate
            };

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TAggregate"></typeparam>
        /// <typeparam name="TCommand"></typeparam>
        /// <param name="commandPackage"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public static CommandPackage<TAggregate, TCommand>
            WithCommand<TAggregate, TCommand>(
                this CommandPackage<TAggregate> commandPackage,
                TCommand command)

            where TAggregate : IAggregate
            where TCommand : ICommand<TAggregate>

            => new CommandPackage<TAggregate, TCommand>
            {
                Target = commandPackage.Target,
                Command = command
            };

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TAggregate"></typeparam>
        /// <typeparam name="TCommand1"></typeparam>
        /// <typeparam name="TCommand2"></typeparam>
        /// <param name="commandPackage"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public static CommandPackage<TAggregate, TCommand1, TCommand2>
            WithCommand<TAggregate, TCommand1, TCommand2>(
                this CommandPackage<TAggregate, TCommand1> commandPackage,
                TCommand2 command)

            where TAggregate : IAggregate
            where TCommand1 : ICommand<TAggregate>
            where TCommand2 : ICommand<TAggregate>

            => new CommandPackage<TAggregate, TCommand1, TCommand2>
            {
                Target = commandPackage.Target,
                Command1 = commandPackage.Command,
                Command2 = command
            };

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TAggregate"></typeparam>
        /// <typeparam name="TCommand1"></typeparam>
        /// <typeparam name="TCommand2"></typeparam>
        /// <typeparam name="TCommand3"></typeparam>
        /// <param name="commandPackage"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public static CommandPackage<TAggregate, TCommand1, TCommand2, TCommand3>
            WithCommand<TAggregate, TCommand1, TCommand2, TCommand3>(
                this CommandPackage<TAggregate, TCommand1, TCommand2> commandPackage,
                TCommand3 command)

            where TAggregate : IAggregate
            where TCommand1 : ICommand<TAggregate>
            where TCommand2 : ICommand<TAggregate>
            where TCommand3 : ICommand<TAggregate>

            => new CommandPackage<TAggregate, TCommand1, TCommand2, TCommand3>
            {
                Target = commandPackage.Target,
                Command1 = commandPackage.Command1,
                Command2 = commandPackage.Command2,
                Command3 = command
            };

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TAggregate"></typeparam>
        /// <typeparam name="TCommand1"></typeparam>
        /// <typeparam name="TCommand2"></typeparam>
        /// <typeparam name="TCommand3"></typeparam>
        /// <typeparam name="TCommand4"></typeparam>
        /// <param name="commandPackage"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public static CommandPackage<TAggregate, TCommand1, TCommand2, TCommand3, TCommand4>
            WithCommand<TAggregate, TCommand1, TCommand2, TCommand3, TCommand4>(
                this CommandPackage<TAggregate, TCommand1, TCommand2, TCommand3> commandPackage,
                TCommand4 command)

            where TAggregate : IAggregate
            where TCommand1 : ICommand<TAggregate>
            where TCommand2 : ICommand<TAggregate>
            where TCommand3 : ICommand<TAggregate>
            where TCommand4 : ICommand<TAggregate>

            => new CommandPackage<TAggregate, TCommand1, TCommand2, TCommand3, TCommand4>
            {
                Target = commandPackage.Target,
                Command1 = commandPackage.Command1,
                Command2 = commandPackage.Command2,
                Command3 = commandPackage.Command3,
                Command4 = command
            };
    }
}
