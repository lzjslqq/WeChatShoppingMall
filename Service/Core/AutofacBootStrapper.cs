using Autofac;
using Autofac.Integration.Mvc;
using DapperExtension.Core;
using System.Configuration;
using System.Reflection;
using System.Web.Mvc;

namespace Service.Core
{
	public class AutofacBootStrapper
	{
		// Controller Assembly
		private Assembly _controllerAssemblies;

		// 单例对象
		private static AutofacBootStrapper _strapper;

		public static AutofacBootStrapper Instance
		{
			get { return _strapper ?? (_strapper = new AutofacBootStrapper()); }
		}

		/// <summary>
		/// IoC Container for Autofac.
		/// </summary>
		public IContainer AutofacContainer { get; set; }

		public void Initialise(Assembly controllerAssemblies)
		{
			_controllerAssemblies = controllerAssemblies;
			BuildContainer();
		}

		private void BuildContainer()
		{
			var builder = new ContainerBuilder();

			#region 注册工作单元

			var readConnString = ConfigurationManager.ConnectionStrings["ReadConnString"].ConnectionString;
			var writeConnString = ConfigurationManager.ConnectionStrings["WriteConnString"].ConnectionString;

			builder.RegisterType<DbContext>().As<IDbContext>().Named<IDbContext>(DbOperation.Read.ToString())
				.WithParameters(new[] { new NamedParameter("connString", readConnString) }).InstancePerDependency();

			builder.RegisterType<DbContext>().As<IDbContext>().Named<IDbContext>(DbOperation.Write.ToString())
				.WithParameters(new[] { new NamedParameter("connString", writeConnString) }).InstancePerDependency();

			#endregion 注册工作单元

			#region 注册服务层

			//builder.RegisterType<SiteConfigService>().As<ISiteConfigService>().SingleInstance();
			//builder.RegisterType<AdminService>().As<IAdminService>().InstancePerRequest();
			//builder.RegisterType<RecommendService>().As<IRecommendService>().InstancePerRequest();
			builder.RegisterType<ProductService>().As<IProductService>().InstancePerRequest();
			builder.RegisterType<FeatureService>().As<IFeatureService>().InstancePerRequest();
            builder.RegisterType<CouponService>().As<ICouponService>().InstancePerRequest();
            builder.RegisterType<OrderService>().As<IOrderService>().InstancePerRequest();
            builder.RegisterType<AdminService>().As<IAdminService>().InstancePerRequest();

			#endregion 注册服务层

			#region 注册控制器

			builder.RegisterControllers(controllerAssemblies: _controllerAssemblies).InstancePerRequest();

			#endregion 注册控制器

			AutofacContainer = builder.Build();
			DependencyResolver.SetResolver(new AutofacDependencyResolver(AutofacContainer));
		}
	}
}