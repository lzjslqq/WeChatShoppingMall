using Autofac;
using Autofac.Integration.Mvc;
using Service;
using System.Reflection;
using System.Web.Mvc;

namespace Backend.App_Start
{
	public sealed class AutofacConfig
	{
		public static void Initialize()
		{
			Initialize(RegisterServices(new ContainerBuilder()));
		}

		public static void Initialize(IContainer container)
		{
			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
		}

		private static IContainer RegisterServices(ContainerBuilder builder)
		{
			builder.RegisterControllers(Assembly.GetExecutingAssembly());

			builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).PropertiesAutowired();

			// service
			builder.RegisterType<ProductService>().As<IProductService>().InstancePerRequest();
			builder.RegisterType<FeatureService>().As<IFeatureService>().InstancePerRequest();
			builder.RegisterType<CouponService>().As<ICouponService>().InstancePerRequest();
			builder.RegisterType<OrderService>().As<IOrderService>().InstancePerRequest();
            builder.RegisterType<OrderItemService>().As<IOrderItemService>().InstancePerRequest();
            builder.RegisterType<OrderShippingService>().As<IOrderShippingService>().InstancePerRequest();
            builder.RegisterType<AdminService>().As<IAdminService>().InstancePerRequest();

			return builder.Build();
		}
	}
}