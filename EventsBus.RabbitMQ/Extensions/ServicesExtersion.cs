﻿using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EventBus.RabbitMQ.Extensions
{
    public static class ServicesExtersion
    {
        public static void AddClassesAsImplementedInterface(this IServiceCollection services, Assembly assembly, 
            Type compareType, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            assembly.GetTypesAssignableTo(compareType).ForEach((type) =>
            {
                foreach (var implementedInterface in type.ImplementedInterfaces)
                {
                    switch (lifetime)
                    {
                        case ServiceLifetime.Scoped:
                            services.AddScoped(implementedInterface, type);
                            break;
                        case ServiceLifetime.Singleton:
                            services.AddSingleton(implementedInterface, type);
                            break;
                        case ServiceLifetime.Transient:
                            services.AddTransient(implementedInterface, type);
                            break;
                    }
                }
            });
        }

        public static List<TypeInfo> GetTypesAssignableTo(this Assembly assembly, Type compareType)
        {
            var typeInfoList = assembly.DefinedTypes.Where(x => x.IsClass
                                && !x.IsAbstract
                                && x != compareType
                                && x.GetInterfaces().Any(i => i.IsGenericType
                                && i.GetGenericTypeDefinition() == compareType))?.ToList();

            return typeInfoList;
        }
    }
}
