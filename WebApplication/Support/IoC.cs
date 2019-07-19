using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using System.Reflection;
using Repository;
using Autofac.Extensions.DependencyInjection;
using Repository.Core.EntityFramework;
using Microsoft.AspNetCore.Http;

namespace WebApplication
{
    public static class IoC
    {
        public static IServiceProvider Configure(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc().AddControllersAsServices();
            services.AddMvcCore().AddControllersAsServices();

            var builder = new ContainerBuilder();
            builder.Populate(services);
            
            LoadAssemblies(builder);

            builder.RegisterGeneric(typeof(Repository<>))
                .As(typeof(IRepository<>))
                .SingleInstance();

            return new AutofacServiceProvider(builder.Build());
        }


        private static void LoadAssemblies(ContainerBuilder builder)
        {
            string[] assemblyNames = new string[] { "Repository", "Repository.Core.EntityFramework", "WebApplication" };
            foreach (var assemblyName in assemblyNames)
            {
                var assemblies = Assembly.Load(new AssemblyName(assemblyName));
                builder.RegisterAssemblyTypes(assemblies)
                    .Where(x => x.GetCustomAttributes(true)
                        .Any(y => y is ExportAttribute && ((ExportAttribute)y).InstanceType ==
                                  InstanceType.SingleInstance))
                    .SingleInstance()
                    .AsImplementedInterfaces()
                    .OnActivated(e =>
                    {
                        var instance = e.Instance as IExportInitializable;
                        if (instance != null)
                            instance.Start();
                    });

                builder.RegisterAssemblyTypes(assemblies)
                    .Where(x => x.GetCustomAttributes(true)
                        .Any(y => y is ExportAttribute && ((ExportAttribute)y).InstanceType ==
                                  InstanceType.InstancePerRequest))
                    .InstancePerLifetimeScope()
                    .AsImplementedInterfaces()
                    .OnActivated(e =>
                    {
                        var instance = e.Instance as IExportInitializable;
                        if (instance != null)
                            instance.Start();
                    });

                builder.RegisterAssemblyTypes(assemblies)
                    .Where(x => x.GetCustomAttributes(true)
                        .Any(y => y is ExportAttribute && ((ExportAttribute)y).InstanceType ==
                                  InstanceType.InstancePerDependency))
                    .InstancePerDependency()
                    .AsImplementedInterfaces()
                    .OnActivated(e =>
                    {
                        var instance = e.Instance as IExportInitializable;
                        if (instance != null)
                            instance.Start();
                    });
            }
        }
    }
}
