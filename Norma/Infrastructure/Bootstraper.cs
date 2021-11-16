using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Norma.Infrastructure
{
    public static class Bootstraper
    {
        //DI 
        public static IHostBuilder Initialize(IEnumerable<Assembly> assemblies) =>
            Host.CreateDefaultBuilder()
                .ConfigureServices(delegate (HostBuilderContext _, IServiceCollection services)
                {
                    foreach (var assembly in assemblies)
                        foreach (var type in assembly.DefinedTypes)
                        {
                            if (type.IsAbstract)
                                continue;

                            foreach (var attribute in type.GetCustomAttributes())
                            {
                                switch (attribute)
                                {
                                    case ExportAttribute exported:
                                        if (exported.SingleInstance)
                                        {
                                            services.AddSingleton(exported.ContractType??type, type);
                                        }
                                        else
                                        {
                                            services.AddScoped(exported.ContractType??type, type);
                                        }
                                        break;
                                    default:
                                        break;
                                }

                            }
                        }

                    // third party dependency


                });

        public static IHost Initialize(IEnumerable<string> assemblyNames) => Initialize(assemblyNames.Select(Assembly.Load)).Build();
            
    }
}
