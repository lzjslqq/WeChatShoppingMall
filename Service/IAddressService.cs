using System.Collections.Generic;
using Model;
using Service.Base;
using ServiceDto;

namespace Service
{
    public interface IAddressService : IBaseService<UserAddress>
    {
        IEnumerable<UserAddressDto> List(int userId);
        UserAddressDto GetDetail(int userId, int id);
        UserAddressDto GetDefault(int userId);
        bool SetDefault(int userId, int id);

    }
}