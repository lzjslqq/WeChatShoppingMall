using System;
using System.Collections.Generic;
using System.Reflection;

namespace Service.Base
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        bool Delete(TEntity entity);

        bool Delete(IEnumerable<TEntity> entities);

        TEntity Get<TKey>(TKey id);

        IEnumerable<TEntity> GetAll();

        long Insert(TEntity entity);

        long Insert(IEnumerable<TEntity> entities);

        bool Update(TEntity entity);

        bool Update(IEnumerable<TEntity> entities);

        bool Update(TEntity entity, Func<PropertyInfo, bool> propertyFilter);

        bool Update(IEnumerable<TEntity> entities, Func<PropertyInfo, bool> propertyFilter);

        IEnumerable<TEntity> GetPagerList<TEntity>(string columns, string table, string where, string orderby, out int rowCount, int pageIndex = 1, int pageSize = 10, object param = null);
    }
}