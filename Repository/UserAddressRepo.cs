using System.Collections.Generic;
using System.Linq;
using DapperExtension.Core;
using ServiceDto;

namespace Repository
{
    public class UserAddressRepo : BaseRepo<UserAddressDto>
    {
        public UserAddressRepo(IDbContext dbContext)
            : base(dbContext)
        {
        }

        public IEnumerable<UserAddressDto> List(int userId)
        {
            var sql = @"SELECT ua.Id,ua.UserId, ua.Receiver, ua.Mobile, ua.Province, ua.City, ua.District, ua.Address, ua.PostCode, ua.IsDefault
FROM UserAddress ua 
WHERE ua.IsDeleted=0 AND ua.Status = 1 AND ua.UserId=@userId;";
            return DbManage.Query<UserAddressDto>(sql, new { userId });
        }

        public UserAddressDto GetDetail(int userId, int id)
        {
            var sql = @"SELECT ua.Id,ua.UserId, ua.Receiver, ua.Mobile, ua.Province, ua.City, ua.District, ua.Address, ua.PostCode, ua.IsDefault
FROM UserAddress ua 
WHERE ua.Id=@id AND ua.UserId=@userId";
            return DbManage.Query<UserAddressDto>(sql, new { userId, id }).FirstOrDefault();
        }

        public UserAddressDto GetDefault(int userId)
        {
            var sql = @"SELECT TOP 1 ua.Id,ua.UserId, ua.Receiver, ua.Mobile, ua.Province, ua.City, ua.District, ua.Address, ua.PostCode, ua.IsDefault
FROM UserAddress ua 
WHERE ua.IsDefault=1 AND ua.UserId=@userId
ORDER BY UpdateTime DESC;";
            return DbManage.Query<UserAddressDto>(sql, new { userId }).FirstOrDefault();
        }

        public bool SetDefault(int userId, int id)
        {
            var sql = @"UPDATE UserAddress SET IsDefault=0 WHERE UserId=@userId AND IsDefault=1;
UPDATE UserAddress SET IsDefault=1 WHERE UserId=@userId AND Id=@id;";
            return DbManage.Execute(sql, new { userId, id }) > 0;
        }
    }
}