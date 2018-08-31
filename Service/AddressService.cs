using System.Collections.Generic;
using Model;
using Repository;
using Service.Base;
using ServiceDto;

namespace Service
{
    public class AddressService : BaseService<UserAddress>, IAddressService
    {
        public IEnumerable<UserAddressDto> List(int userId)
        {
            if (userId <= 0)
                return null;

            using (var cxt = DbContext(DbOperation.Read))
            {
                var repo = new UserAddressRepo(cxt);
                return repo.List(userId);
            }
        }

        public UserAddressDto GetDetail(int userId, int id)
        {
            if (id <= 0 || userId <= 0)
                return null;

            using (var cxt = DbContext(DbOperation.Read))
            {
                var repo = new UserAddressRepo(cxt);
                return repo.GetDetail(userId, id);
            }
        }

        public UserAddressDto GetDefault(int userId)
        {
            if (userId <= 0)
                return null;

            using (var cxt = DbContext(DbOperation.Read))
            {
                var repo = new UserAddressRepo(cxt);
                return repo.GetDefault(userId);
            }
        }

        public bool SetDefault(int userId, int id)
        {
            if (id <= 0 || userId <= 0)
                return false;

            using (var cxt = DbContext(DbOperation.Write))
            {
                cxt.BeginTransaction();

                var repo = new UserAddressRepo(cxt);
                var flag = repo.SetDefault(userId, id);

                if(flag) cxt.Commit();
                else cxt.Rollback();

                return flag;
            }
        }


    }
}