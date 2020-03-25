using Autofac;
using System;
using System.IO;

namespace Algorithms
{
    public static class ContainerSetup
    {
        public static IContainer Initialize()
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(typeof(ContainerSetup).Assembly)
                .AsImplementedInterfaces();

            builder.Register(_ => Console.Out).As<TextWriter>();

            return builder.Build();
        }
    }
}
