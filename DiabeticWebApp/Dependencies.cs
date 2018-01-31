using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using AutoMapper;
using DiabeticWebApp.Models;

namespace DiabeticWebApp
{
    public class Dependencies
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            var assemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>().ToArray();

            builder.RegisterApiControllers(assemblies);

            builder.RegisterType<ApplicationDbContext>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .PreserveExistingDefaults();

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .PreserveExistingDefaults();
            
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}