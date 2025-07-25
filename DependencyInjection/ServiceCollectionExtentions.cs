﻿using System.Reflection;
using System.Runtime.CompilerServices;
using EnterpriseBlog.DependencyInjection.Interfaces;

namespace EnterpriseBlog.DependencyInjection
{
    public static class ServiceCollectionExtentions
    {
        public static IServiceCollection RegisterVMsByLifeTime(this IServiceCollection services, Assembly assembly)
        {
            var allClasses = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract);

            foreach (var type in allClasses)
            {
                if (typeof(IScoped).IsAssignableFrom(type))
                {
                    services.AddScoped(type);
                }
                else if (typeof(ITransient).IsAssignableFrom(type))
                {
                    services.AddTransient(type);
                }
                else if (typeof(ISingleton).IsAssignableFrom(type))
                {
                    services.AddSingleton(type);
                }
            }

            return services;
        }
    }
}
