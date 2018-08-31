using Autofac;
using DapperExtension.Core;
using Repository;
using Service.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;

namespace Service
{
	public enum DbOperation
	{
		Read, Write
	};
}

namespace Service.Base
{
	public class BaseService<TEntity> where TEntity : class
	{
		protected IDbContext DbContext(DbOperation state)
		{
			// return AutofacBootStrapper.Instance.AutofacContainer.ResolveNamed<IDbContext>(state.ToString());
			var connString = state == DbOperation.Read ? ConfigurationManager.ConnectionStrings["ReadConnString"].ConnectionString : ConfigurationManager.ConnectionStrings["WriteConnString"].ConnectionString;
			return new DbContext(connString);
		}

		public virtual bool Delete(TEntity entity)
		{
			using (var cxt = DbContext(DbOperation.Write))
			{
				try
				{
					cxt.BeginTransaction();
					var repo = new BaseRepo<TEntity>(cxt);
					var isSuccess = repo.Delete(entity);

					if (isSuccess) cxt.Commit();
					else cxt.Rollback();

					return isSuccess;
				}
				catch (Exception ex)
				{
					cxt.Rollback();
					return false;
				}
			}
		}

		public virtual bool Delete(IEnumerable<TEntity> entities)
		{
			using (var cxt = DbContext(DbOperation.Write))
			{
				try
				{
					cxt.BeginTransaction();
					var repo = new BaseRepo<TEntity>(cxt);
					var isSuccess = repo.Delete(entities);

					if (isSuccess) cxt.Commit();
					else cxt.Rollback();

					return isSuccess;
				}
				catch (Exception ex)
				{
					cxt.Rollback();
					return false;
				}
			}
		}

		public virtual TEntity Get<TKey>(TKey id)
		{
			using (var cxt = DbContext(DbOperation.Read))
			{
				var repo = new BaseRepo<TEntity>(cxt);
				return repo.Get(id);
			}
		}

		public IEnumerable<TEntity> GetAll()
		{
			using (var cxt = DbContext(DbOperation.Read))
			{
				var repo = new BaseRepo<TEntity>(cxt);
				return repo.GetAll();
			}
		}

		public virtual long Insert(TEntity entity)
		{
			using (var cxt = DbContext(DbOperation.Write))
			{
				try
				{
					cxt.BeginTransaction();
					var repo = new BaseRepo<TEntity>(cxt);
					long id = repo.Insert(entity);

					if (id > 0) cxt.Commit();
					else cxt.Rollback();

					return id;
				}
				catch (Exception ex)
				{
					cxt.Rollback();
					return 0;
				}
			}
		}

		public virtual long Insert(IEnumerable<TEntity> entities)
		{
			using (var cxt = DbContext(DbOperation.Write))
			{
				try
				{
					cxt.BeginTransaction();
					var repo = new BaseRepo<TEntity>(cxt);
					long id = repo.Insert(entities);

					if (id > 0) cxt.Commit();
					else cxt.Rollback();

					return id;
				}
				catch (Exception ex)
				{
					cxt.Rollback();
					return 0;
				}
			}
		}

		public virtual bool Update(TEntity entity)
		{
			using (var cxt = DbContext(DbOperation.Write))
			{
				try
				{
					cxt.BeginTransaction();
					var repo = new BaseRepo<TEntity>(cxt);
					bool isSuccess = repo.Update(entity);

					if (isSuccess) cxt.Commit();
					else cxt.Rollback();

					return isSuccess;
				}
				catch (Exception ex)
				{
					cxt.Rollback();
					return false;
				}
			}
		}

		public virtual bool Update(IEnumerable<TEntity> entities)
		{
			using (var cxt = DbContext(DbOperation.Write))
			{
				try
				{
					cxt.BeginTransaction();
					var repo = new BaseRepo<TEntity>(cxt);
					bool isSuccess = repo.Update(entities);

					if (isSuccess) cxt.Commit();
					else cxt.Rollback();

					return isSuccess;
				}
				catch (Exception ex)
				{
					cxt.Rollback();
					return false;
				}
			}
		}

		public virtual bool Update(TEntity entity, Func<PropertyInfo, bool> propertyFilter)
		{
			using (var cxt = DbContext(DbOperation.Write))
			{
				try
				{
					cxt.BeginTransaction();
					var repo = new BaseRepo<TEntity>(cxt);
					bool isSuccess = repo.Update(entity, propertyFilter);

					if (isSuccess) cxt.Commit();
					else cxt.Rollback();

					return isSuccess;
				}
				catch (Exception ex)
				{
					cxt.Rollback();
					return false;
				}
			}
		}

		public virtual bool Update(IEnumerable<TEntity> entities, Func<PropertyInfo, bool> propertyFilter)
		{
			using (var cxt = DbContext(DbOperation.Write))
			{
				try
				{
					cxt.BeginTransaction();
					var repo = new BaseRepo<TEntity>(cxt);
					bool isSuccess = repo.Update(entities, propertyFilter);

					if (isSuccess) cxt.Commit();
					else cxt.Rollback();

					return isSuccess;
				}
				catch (Exception ex)
				{
					cxt.Rollback();
					return false;
				}
			}
		}

		public IEnumerable<TEntity> GetPagerList<TEntity>(string columns, string table, string where, string orderby, out int rowCount, int pageIndex = 1, int pageSize = 10, object param = null)
		{
			using (var cxt = DbContext(DbOperation.Read))
			{
				var list = new DbManage(cxt).GetPagerList<TEntity>(columns, table, where, orderby, out rowCount, pageIndex, pageSize, param);

				return list;
			}
		}
	}
}