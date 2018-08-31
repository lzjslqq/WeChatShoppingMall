using Model.Common;
using Service;
using ServiceDto;
using System.Collections.Generic;
using System.Web.Http;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Api.Controllers
{
    public class ShoppingCartController : ApiController
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        [HttpGet]
        public ComplexResponse<IEnumerable<ShoppingCartDetailDto>> Detail(int userId)
        {
            ErrorMessage msg = ErrorMessage.失败;
            if (userId <= 0)
                msg = ErrorMessage.用户不存在;

            var dtos = _shoppingCartService.GetDetailsByUser(userId);
            if (dtos != null && dtos.Any())
                msg = ErrorMessage.成功;

            return new ComplexResponse<IEnumerable<ShoppingCartDetailDto>>((int)msg, msg.ToString(), dtos);
        }

        [HttpGet]
        public ComplexResponse<bool> Add(int userId, int productId, int featureValueId, int num)
        {
            bool flag = false;
            ErrorMessage msg = ErrorMessage.失败;
            if (userId > 0 && productId > 0 && featureValueId > 0 && num > 0)
            {
                flag = _shoppingCartService.AddToCart(userId, productId, featureValueId, num);
                if (flag) msg = ErrorMessage.成功;
            }
            return new ComplexResponse<bool>((int)msg, msg.ToString(), flag);
        }

        [HttpGet]
        public ComplexResponse<bool> Delete(int userId, string ids)
        {
            bool flag = false;
            ErrorMessage msg = ErrorMessage.失败;

            if (userId > 0 && !string.IsNullOrWhiteSpace(ids))
            {
                var idArr = ids.Split(',').Select(int.Parse).ToArray();

                flag = _shoppingCartService.DeleteBatch(userId, idArr);
                if (flag) msg = ErrorMessage.成功;
            }

            return new ComplexResponse<bool>((int)msg, msg.ToString(), flag);
        }

        [HttpGet]
        public ComplexResponse<bool> UpdateNum(int userId, int cartId, int num)
        {
            bool flag = false;
            ErrorMessage msg = ErrorMessage.失败;
            if (userId > 0 && cartId > 0 && num > 0)
            {
                flag = _shoppingCartService.UpdateNum(userId, cartId, num);
                if (flag) msg = ErrorMessage.成功;
            }
            return new ComplexResponse<bool>((int)msg, msg.ToString(), flag);
        }

        [HttpGet]
        public ComplexResponse<IEnumerable<ShoppingCartDetailDto>> GetItemsByCartIds(string cartIds)
        {
            ErrorMessage msg = ErrorMessage.失败;
            IEnumerable<ShoppingCartDetailDto> data = null;

            if (!string.IsNullOrWhiteSpace(cartIds))
            {
                var cartIdArr = cartIds.Split(',').Select(int.Parse).ToArray();
                data = _shoppingCartService.GetItemsByCartIds(cartIdArr);
                if (data != null && data.Any())
                    msg = ErrorMessage.成功;
            }

            return new ComplexResponse<IEnumerable<ShoppingCartDetailDto>>((int)msg, msg.ToString(), data);
        }

        [HttpGet]
        public ComplexResponse<object> GetCartCount(int userId)
        {
            ErrorMessage msg = ErrorMessage.失败;
            if (userId <= 0)
                msg = ErrorMessage.用户不存在;

            var count = _shoppingCartService.GetCartCount(userId);
            msg = ErrorMessage.成功;

            return new ComplexResponse<object>((int)msg, msg.ToString(), new { count });
        }

    }
}