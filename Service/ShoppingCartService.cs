using Model;
using Repository;
using Service.Base;
using ServiceDto;
using System.Collections.Generic;

namespace Service
{
    public class ShoppingCartService : BaseService<ShoppingCart>, IShoppingCartService
    {
        public IEnumerable<ShoppingCartDetailDto> GetDetailsByUser(int userId)
        {
            if (userId <= 0)
                return null;
            using (var cxt = DbContext(DbOperation.Read))
            {
                var repo = new ShoppingCartRepo(cxt);
                return repo.GetDetailsByUser(userId);
            }
        }

        public bool AddToCart(int userId, int productId, int featureValueId, int num)
        {
            if (userId <= 0 || productId <= 0 || featureValueId <= 0)
                return false;

            using (var cxt = DbContext(DbOperation.Write))
            {
                cxt.BeginTransaction();
                var repo = new ShoppingCartRepo(cxt);
                var flag = repo.AddToCart(userId, productId, featureValueId, num);

                if (flag) cxt.Commit();
                else cxt.Rollback();

                return flag;
            }
        }

        public bool DeleteBatch(int userId, int[] ids)
        {
            if (userId <= 0 || ids.Length == 0)
                return false;

            using (var cxt = DbContext(DbOperation.Write))
            {
                cxt.BeginTransaction();
                var repo = new ShoppingCartRepo(cxt);
                var flag = repo.DeleteBatch(userId, ids);

                if (flag) cxt.Commit();
                else cxt.Rollback();

                return flag;
            }
        }

        public bool UpdateNum(int userId, int cartId, int num)
        {
            if (userId <= 0 || cartId <= 0 || num <= 0)
                return false;

            using (var cxt = DbContext(DbOperation.Write))
            {
                cxt.BeginTransaction();
                var repo = new ShoppingCartRepo(cxt);
                var flag = repo.UpdateNum(userId, cartId, num);

                if (flag) cxt.Commit();
                else cxt.Rollback();

                return flag;
            }
        }

        public IEnumerable<ShoppingCartDetailDto> GetItemsByCartIds(int[] cartIdArr)
        {
            if (cartIdArr.Length == 0)
                return null;
            using (var cxt = DbContext(DbOperation.Read))
            {
                var repo = new ShoppingCartRepo(cxt);
                return repo.GetItemsByCartIds(cartIdArr);
            }
        }

        public int GetCartCount(int userId)
        {
            if (userId <= 0)
                return 0;
            using (var cxt = DbContext(DbOperation.Read))
            {
                var repo = new ShoppingCartRepo(cxt);
                return repo.GetCartCount(userId);
            }
        }
    }
}