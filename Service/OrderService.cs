using Common;
using Model;
using Repository;
using Service.Base;
using ServiceDto;
using System;
using Utility;
using System.Linq;
using Constants = Model.Common.Constants;

namespace Service
{
    public class OrderService : BaseService<Order>, IOrderService
    {
        public OrderDetailViewDto GetDetail(int userId, int orderId)
        {
            if (userId > 0 && orderId > 0)
            {
                using (var ctx = DbContext(DbOperation.Read))
                {
                    var repo = new OrderRepo(ctx);
                    return repo.GetDetail(userId, orderId);
                }
            }
            return null;
        }


        public bool CancelOrder(int userId, string tradeNo)
        {
            if (userId > 0 && !string.IsNullOrEmpty(tradeNo))
            {
                using (var ctx = DbContext(DbOperation.Write))
                {
                    ctx.BeginTransaction();

                    var repo = new OrderRepo(ctx);

                    if (repo.UpdateOrderStatus(tradeNo, (int) Enums.OrderStatus.交易关闭))
                    {
                        ctx.Commit();

                        #region 更新商品库存量

                        try
                        {
                            // 恢复商品库存量
                            var productRepo = new ProductRepo(ctx);
                            var productList = productRepo.GetProductsByTradeNo(tradeNo);

                            foreach (var orderItem in productList)
                            {
                                productRepo.AddAmount(orderItem.ProductId, orderItem.Num);
                            }
                        }
                        catch (Exception ex)
                        {
                            // nothing
                        }

                        #endregion

                        return true;
                    }
                    else
                    {
                        ctx.Rollback();
                        return false;
                    }
                }
            }
            return false;
        }

        public Enums.PayErrorMsg CheckParamsValid(GenerateOrderDto dto)
        {
            var errorMsg = Enums.PayErrorMsg.失败;
            if (dto.UserId <= 0 || dto.TotalFee < 0 || dto.Payment < 0 || dto.UserCouponId < 0 || dto.PostFee < 0)
                return Enums.PayErrorMsg.参数错误;

            using (var cxt = DbContext(DbOperation.Read))
            {
                var repo = new OrderRepo(cxt);
                var result = repo.CheckParamsValid(dto);
                EnumHelper.TryParsebyValue(result, out errorMsg);

                return errorMsg;
            }
        }

        public string GenerateOrder(GenerateOrderDto dto, out Enums.PayErrorMsg msg)
        {
            string tradeNo = string.Empty;
            msg = CheckParamsValid(dto);
            if (msg == Enums.PayErrorMsg.成功)
            {
                using (var cxt = DbContext(DbOperation.Write))
                {
                    cxt.BeginTransaction();
                    var repo = new OrderRepo(cxt);

                    var id = repo.Insert(new Order
                    {
                        TotalFee = dto.TotalFee,
                        Payment = dto.Payment,
                        PayType = (int) Enums.PayType.微信支付,
                        UserCouponId = dto.UserCouponId,
                        PostFee = dto.PostFee,
                        UserId = dto.UserId,
                        UserNickname = dto.UserNickname,
                        IsDeleted = 0,
                        Sort = 0,
                        Status = (int) Enums.OrderStatus.未付款,
                        Creator = "admin",
                        PayTime = DateTime.Now,
                        DeliveryTime = DateTime.Now,
                        EndTime = DateTime.Now,
                        CloseTime = DateTime.Now,
                        CreateTime = DateTime.Now,
                        UpdateTime = DateTime.Now,
                        ShippingName = dto.ShippingName,
                        UserMessage = dto.UserMessage,
                        IsRate = string.IsNullOrEmpty(dto.UserMessage) ? 0 : 1,
                    });

                    if (id > 0)
                    {
                        tradeNo = repo.GetTradeNo(id);
                        // 生成订单明细表
                        if (dto.OrderItemDtos.Any())
                        {
                            foreach (var orderItem in dto.OrderItemDtos)
                            {
                                orderItem.OrderId = (int) id;
                                if (repo.InsertOrderItem(orderItem) != 1)
                                {
                                    cxt.Rollback();
                                }
                            }
                        }
                        // 生成订单配送表
                        int result = repo.InsertOrderShipping(new OrderShipping
                        {
                            OrderId = (int) id,
                            Name = dto.Receiver,
                            Mobile = dto.Mobile,
                            Province = dto.Province,
                            City = dto.City,
                            District = dto.District,
                            Address = dto.Address,
                            PostCode = dto.PostCode,
                            CreateTime = DateTime.Now,
                            UpdateTime = DateTime.Now,
                            Status = 1,
                        });

                        if (result == 1)
                        {
                            var flag = true;

                            #region 冻结用户优惠券

                            if (dto.UserCouponId > 0)
                            {
                                flag = repo.UpdateUserCoupon(dto.UserCouponId);
                            }

                            #endregion

                            #region 删除购物车

                            if (!string.IsNullOrEmpty(dto.CartIds))
                            {
                                var cartIdArr = dto.CartIds.Split(',').Select(int.Parse).ToArray();
                                flag = repo.DeleteShoppingCart(cartIdArr);
                            }

                            #endregion

                            if (flag)
                                cxt.Commit();
                            else
                                cxt.Rollback();

                            #region 更新产品库存量（非必实现项）

                            try
                            {
                                var productRepo = new ProductRepo(cxt);

                                foreach (var orderItem in dto.OrderItemDtos)
                                {
                                    productRepo.AddAmount(orderItem.ProductId, -orderItem.Num);
                                }
                            }
                            catch (Exception)
                            {
                                // do nothing
                            }

                            #endregion

                        }
                        else
                        {
                            cxt.Rollback();
                        }
                    }
                    else
                    {
                        cxt.Rollback();
                    }
                }
            }

            return tradeNo;
        }

        public OrderItemViewDto GetDefaultProduct(int orderId)
        {
            if (orderId > 0)
            {
                using (var ctx = DbContext(DbOperation.Read))
                {
                    var repo = new OrderRepo(ctx);
                    return repo.GetDefaultProduct(orderId);
                }
            }
            return null;
        }

        public PayOrderDto GetPayOrderInfoByTradeNo(string tradeNo)
        {
            if (!string.IsNullOrEmpty(tradeNo))
            {
                using (var ctx = DbContext(DbOperation.Read))
                {
                    var repo = new OrderRepo(ctx);
                    return repo.GetPayOrderInfoByTradeNo(tradeNo);
                }
            }
            return null;
        }

        public bool UpdateOrderStatus(string tradeNo, int status)
        {
            if (Enum.IsDefined(typeof(Enums.OrderStatus), status))
            {
                using (var ctx = DbContext(DbOperation.Write))
                {
                    ctx.BeginTransaction();
                    var repo = new OrderRepo(ctx);
                    if (repo.UpdateOrderStatus(tradeNo, status))
                    {
                        ctx.Commit();
                        return true;
                    }
                    else
                    {
                        ctx.Rollback();
                        return false;
                    }
                }
            }
            return false;
        }

        public bool ExistOrder(int userId, string tradeNo)
        {
            if (userId > 0 && !string.IsNullOrEmpty(tradeNo))
            {
                using (var ctx = DbContext(DbOperation.Read))
                {
                    var repo = new OrderRepo(ctx);
                    return repo.ExistOrder(userId, tradeNo);
                }
            }
            return false;
        }

        public OrderStatViewDto GetStatCountInfo(int userId)
        {
            if (userId > 0)
            {
                using (var ctx = DbContext(DbOperation.Read))
                {
                    var repo = new OrderRepo(ctx);
                    return repo.GetStatCountInfo(userId);
                }
            }
            return null;
        }
    }
}