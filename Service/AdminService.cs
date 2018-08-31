using DapperExtension.Core;
using Model;
using Repository;
using Service.Base;

namespace Service
{
    public class AdminService : BaseService<Admin>, IAdminService
    {
        public Admin GetByUserName(string userName)
        {
            using (var ctx = DbContext(DbOperation.Read))
            {
                var repo = new AdminRepo(ctx);
                return repo.GetByUserName(userName);
            }
        }
    }
}