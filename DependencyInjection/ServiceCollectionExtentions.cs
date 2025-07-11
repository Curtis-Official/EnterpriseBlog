using System.Reflection;
using System.Runtime.CompilerServices;
using EnterpriseBlog.DependencyInjection.Interfaces;

namespace EnterpriseBlog.DependencyInjection
{
    public static class ServiceCollectionExtentions
    {
        public static IServiceCollection RegisterObjectsByLifeTime(this IServiceCollection services, Assembly[] assemblies)
        {
            var allClasses = assemblies.SelectMany(a => a.GetTypes())
                .Where(t => t.IsClass && !t.IsAbstract);

            foreach (var type in allClasses)
            {
                var interfaces = type.GetInterfaces()
                    .Except(new[] { typeof(IScoped), typeof(ITransient), typeof(ISingleton) })
                    .ToList();

                if (typeof(IScoped).IsAssignableFrom(type))
                {
                    foreach (var iface in interfaces)
                        services.AddScoped(iface, type);
                }
                else if (typeof(ITransient).IsAssignableFrom(type))
                {
                    foreach (var iface in interfaces)
                        services.AddTransient(iface, type);
                }
                else if (typeof(ISingleton).IsAssignableFrom(type))
                {
                    foreach (var iface in interfaces)
                        services.AddSingleton(iface, type);
                }
            }

            return services;
        }
    }
}
