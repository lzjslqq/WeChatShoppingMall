using Model;
using Service.Base;

namespace Service
{
    public interface IAdminService: IBaseService<Admin>
    {
        Admin GetByUserName(string userName);
    }
}