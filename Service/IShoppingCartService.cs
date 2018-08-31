using Model;
using Service.Base;
using ServiceDto;
using System.Collections.Generic;

namespace Service
{
	public interface IShoppingCartService : IBaseService<ShoppingCart>
	{
		IEnumerable<ShoppingCartDetailDto> GetDetailsByUser(int userId);

	    bool AddToCart(int userId, int productId, int featureValueId, int num);

        /// <summary>
        ///  批量删除购物车项目（软删除）
        /// </summary>
	    bool DeleteBatch(int userId, int[] ids);

	    bool UpdateNum(int userId, int cartId, int num);

	    IEnumerable<ShoppingCartDetailDto> GetItemsByCartIds(int[] cartIdArr);

        int GetCartCount(int userId);
	}
}