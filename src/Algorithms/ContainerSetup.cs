using Autofac;

namespace Algorithms
{
    public static class ContainerSetup
    {
        public static IContainer Initialize()
        {
            var builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(typeof(ContainerSetup).Assembly)
                .AsImplementedInterfaces();

            return builder.Build();
        }
    }
}
