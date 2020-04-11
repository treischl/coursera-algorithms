using Autofac;
using System;
using System.IO;
using System.Reflection;

namespace Algorithms.Core
{
    public static class ContainerSetup
    {
        public static IContainer InitializeConsole(Assembly consoleAssembly) =>
            BaseInitialization(builder =>
            {
                builder.RegisterAssemblyTypes(consoleAssembly).AsImplementedInterfaces();
                builder.Register(_ => Console.Out).As<TextWriter>();
            });

        public static IContainer BaseInitialization(Action<ContainerBuilder> setupAction = null)
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(typeof(ContainerSetup).Assembly)
                .AsImplementedInterfaces();

            setupAction?.Invoke(builder);
            return builder.Build();
        }
    }
}
