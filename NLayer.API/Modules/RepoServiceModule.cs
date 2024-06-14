using Autofac;
using NLayer.Caching;
using NLayer.Core.Repositoties;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository;
using NLayer.Repository.Repositories;
using NLayer.Repository.UnitOfWorks;
using NLayer.Service.Mapping;
using NLayer.Service.Services;
using System.Reflection;
using Module = Autofac.Module;

namespace NLayer.API.Modules
{
    public class RepoServiceModule:Module
    {

        protected override void Load(ContainerBuilder builder)

        { 
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>)).InstancePerLifetimeScope(); //GenericRepository için
            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerLifetimeScope(); //Service için
             
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>(); //// UnitOfWork ler için 

            var apiAssembly = Assembly.GetExecutingAssembly();   // API katmanındaki eklentiler için
            var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext)); // Repository katmanındaki eklentiler için
            var serviceAssembly = Assembly.GetAssembly(typeof(MapProfile)); // Service katmanındaki eklentiler için

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x=>x.Name.EndsWith("Repository")).AsImplementedInterfaces().InstancePerLifetimeScope();  // Repository ler için 
            //AutoFac teki InstancePerLifetimeScope() , .net core deki Scope ye karşılık geliyor

            builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly).Where(x => x.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerLifetimeScope(); // Service ler ler için 

            //builder.RegisterType<ProductServiceWithCaching>().As<IProductService>();   // Cache den değil de DB den okusun. Cache den okumasını istersek bunu aktif edeceğiz

        }


    }
}
