using Autofac;
using Autofac.Integration.WebApi;
using Service;
using System.Reflection;
using System.Web.Http;

namespace Api.App_Start
{
	public class AutofacConfig
	{
		public static void Initialize(HttpConfiguration config)
		{
			Initialize(config, RegisterServices(new ContainerBuilder()));
		}

		public static void Initialize(HttpConfiguration config, IContainer container)
		{
			config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
		}

		private static IContainer RegisterServices(ContainerBuilder builder)
		{
			builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

			builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).PropertiesAutowired();

			// service
			builder.RegisterType<ProductService>().As<IProductService>().InstancePerRequest();
			builder.RegisterType<CouponService>().As<ICouponService>().InstancePerRequest();
			builder.RegisterType<UserService>().As<IUserService>().InstancePerRequest();
            builder.RegisterType<FeatureService>().As<IFeatureService>().InstancePerRequest();
            builder.RegisterType<ShoppingCartService>().As<IShoppingCartService>().InstancePerRequest();
            builder.RegisterType<AddressService>().As<IAddressService>().InstancePerRequest();
            builder.RegisterType<OrderService>().As<IOrderService>().InstancePerRequest();
            builder.RegisterType<OrderItemService>().As<IOrderItemService>().InstancePerRequest();

			return builder.Build();
		}
	}
}