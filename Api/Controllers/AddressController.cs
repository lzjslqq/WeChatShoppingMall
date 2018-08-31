using System;
using System.Collections.Generic;
using System.Web.Http;
using Model;
using Model.Common;
using Service;
using ServiceDto;

namespace Api.Controllers
{
    public class AddressController : ApiController
    {
        public readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        // 列表查询
        [HttpGet]
        public ComplexResponse<IEnumerable<UserAddressDto>> List(int userId)
        {
            var msg = ErrorMessage.失败;
            IEnumerable<UserAddressDto> data = null;
            if (userId > 0)
            {
                data = _addressService.List(userId);
                msg = ErrorMessage.成功;
            }
            return new ComplexResponse<IEnumerable<UserAddressDto>>((int)msg, msg.ToString(), data);
        }

        // 获取默认收货地址
        [HttpGet]
        public ComplexResponse<UserAddressDto> Detail(int userId, int id)
        {
            var msg = ErrorMessage.失败;
            UserAddressDto data = null;

            if (userId > 0 && id > 0)
            {
                data = _addressService.GetDetail(userId, id);
                if (data != null)
                {
                    msg = ErrorMessage.成功;
                }
            }

            return new ComplexResponse<UserAddressDto>((int)msg, msg.ToString(), data);
        }

        // 编辑：新增 & 修改
        [HttpPost]
        public ComplexResponse<int> Edit(UserAddress model)
        {
            var msg = ErrorMessage.失败;
            int data = 0;

            if (model.Id <= 0)
            {
                model.IsDefault = 0;
                model.CreateTime = model.UpdateTime = DateTime.Now;
                model.IsDeleted = 0;
                model.Status = 1;
                model.IsDefault = 0;

                data = (int)_addressService.Insert(model);
                if (data > 0)
                {
                    msg = ErrorMessage.成功;
                }
            }
            else
            {
                var flag = _addressService.Update(model, p => p.Name.Equals("Receiver") || p.Name.Equals("Mobile") || p.Name.Equals("Province")
                    || p.Name.Equals("City") || p.Name.Equals("District") || p.Name.Equals("Address") || p.Name.Equals("PostCode"));

                if (flag)
                {
                    msg = ErrorMessage.成功;
                }
            }

            return new ComplexResponse<int>((int)msg, msg.ToString(), data);
        }

        // 获取默认收货地址
        [HttpGet]
        public ComplexResponse<UserAddressDto> Default(int userId)
        {
            var msg = ErrorMessage.失败;
            UserAddressDto data = null;

            if (userId > 0)
            {
                data = _addressService.GetDefault(userId);
                if (data != null)
                {
                    msg = ErrorMessage.成功;
                }
            }

            return new ComplexResponse<UserAddressDto>((int)msg, msg.ToString(), data);
        }

        [HttpGet]
        public ComplexResponse<bool> SetDefault(int userId, int id)
        {
            var msg = ErrorMessage.失败;
            bool data = false;

            if (userId > 0 && id > 0)
            {
                data = _addressService.SetDefault(userId, id);
                if (data)
                {
                    msg = ErrorMessage.成功;
                }
            }

            return new ComplexResponse<bool>((int)msg, msg.ToString(), data);
        }

        [HttpGet]
        public ComplexResponse<bool> Delete(int userId, int id)
        {
            var msg = ErrorMessage.失败;
            bool data = false;

            if (userId > 0 && id > 0)
            {
                var model = new UserAddress { Id = id, IsDeleted = 1 };
                data = _addressService.Update(model, p => p.Name.Equals("IsDeleted"));
                if (data)
                {
                    msg = ErrorMessage.成功;
                }
            }

            return new ComplexResponse<bool>((int)msg, msg.ToString(), data);
        }



    }
}
