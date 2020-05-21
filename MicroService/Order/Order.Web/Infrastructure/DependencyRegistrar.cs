using Autofac;
using Order.Infrastructure.Repositories;
using Order.Web.Application.CommandHandler;
using Shared.Infrastructure.Core.Dapper;
using System.Reflection;
using Module = Autofac.Module;

namespace Order.Web.Infrastructure
{
    public class DependencyRegistrar : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OrderRepository>().As<IOrderRepository>().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(DapperQuery<>)).As(typeof(IDapperQuery<>)).InstancePerLifetimeScope();
            //注入command
            builder.RegisterAssemblyTypes(typeof(OrderCommandHandler).GetTypeInfo().Assembly);
        }
    }
}
