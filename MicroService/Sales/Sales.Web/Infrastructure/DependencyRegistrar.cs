using Autofac;
using Shared.Infrastructure.Core.Dapper;
using Module = Autofac.Module;

namespace Sales.Web.Infrastructure
{
    public class DependencyRegistrar : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(DapperQuery<>)).As(typeof(IDapperQuery<>)).InstancePerLifetimeScope();
            //注入command
        }
    }
}
