using Algorithms.Commands;
using Algorithms.Core;
using Autofac;
using Autofac.Core;
using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Algorithms
{
    public static class Program
    {
        private static IContainer _container { get; } =
            ContainerSetup.InitializeConsole(typeof(Program).Assembly);

        public static void Main(string[] args)
        {
            var verbs = LoadVerbs();
            Parser.Default.ParseArguments(args, verbs)
                .WithParsed(Run)
                .WithNotParsed(HandleErrors);
        }

        private static Type[] LoadVerbs() => typeof(Program).Assembly
            .GetTypes()
            .Where(type => type.IsClass
                && type.GetCustomAttribute<VerbAttribute>() != null)
            .ToArray();

        public static void Run(object options)
        {
            using var scope = _container.BeginLifetimeScope();
            var resolve = Array.Find(
                typeof(ResolutionExtensions).GetMethods(),
                m => m.Name == nameof(ResolutionExtensions.Resolve)
                    && m.GetParameters().Length == 2
                    && m.ReturnType == typeof(object));
            var commandType = typeof(ICommand<>).MakeGenericType(options.GetType());

            var command = GetCommand(resolve!, scope, commandType);
            var execute = command.GetType().GetMethod(nameof(ICommand<object>.Execute));
            execute!.Invoke(command, new object[] { options });
        }

        private static object GetCommand(MethodInfo resolve, ILifetimeScope scope, Type commandType)
        {
            var command = resolve.Invoke(null, new object[] { scope, commandType });

            if (command == default)
            {
                throw new DependencyResolutionException($"Unable to resolve type: {commandType.Name}");
            }

            return command;
        }

        public static void HandleErrors(IEnumerable<Error> errors)
        {
            throw new NotImplementedException();
        }
    }
}
