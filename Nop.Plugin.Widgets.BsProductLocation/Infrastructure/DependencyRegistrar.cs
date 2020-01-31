using Autofac;
using Autofac.Core;
using Nop.Core.Configuration;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using Nop.Plugin.Widgets.BsProductLocation.Data;
using Nop.Plugin.Widgets.BsProductLocation.Domain;
using Nop.Plugin.Widgets.BsProductLocation.Services;
using Nop.Web.Framework.Mvc;

namespace Nop.Plugin.Widgets.BsProductLocation.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {

            const string CONTEXT = "nop_object_context_Pl_product_location";

            builder.RegisterType<ProductLocationRecordService>().As<IProductLocationRecordService>().InstancePerLifetimeScope();

            builder.RegisterType<ServiceAreaService>().As<IServiceAreaService>().InstancePerLifetimeScope();

            //data context
            this.RegisterPluginDataContext<ProductLocationObjectContext>(builder, CONTEXT);

            //override required repository with our custom context
            builder.RegisterType<EfRepository<ServiceAreaRecord>>()
    .As<IRepository<ServiceAreaRecord>>()
    .WithParameter(ResolvedParameter.ForNamed<IDbContext>(CONTEXT))
    .InstancePerLifetimeScope();
            builder.RegisterType<EfRepository<ProductLocationRecord>>()
    .As<IRepository<ProductLocationRecord>>()
    .WithParameter(ResolvedParameter.ForNamed<IDbContext>(CONTEXT))
    .InstancePerLifetimeScope();
           
            
           
        }

        public int Order
        {
            get { return 2; }
        }


       
    }
}
