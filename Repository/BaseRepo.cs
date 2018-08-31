using DapperExtension.Core;

namespace Repository
{
	public class BaseRepo<TEntity> : Repository<TEntity> where TEntity : class
	{
		protected IDbManage DbManage { get; private set; }

		public BaseRepo(IDbContext dbContext)
			: base(dbContext)
		{
			DbManage = new DbManage(dbContext);
		}
	}
}