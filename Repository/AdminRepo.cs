using System.Linq;
using DapperExtension.Core;
using Model;

namespace Repository
{
    public class AdminRepo : BaseRepo<Admin>
    {
        public AdminRepo(IDbContext dbContext)
            : base(dbContext)
        {
        }

        public int Login(string username, string password)
        {
            var sql = @"select Id from dbo.Admin where [LogName] = @username and [LogPwd]= @password";
            return DbManage.ExecuteScalar<int>(sql, new { username = username, password = password });
        }

        public Admin GetByUserName(string username)
        {
            var sql = @"select top 1 * from [dbo].[Admin] where LogName=@userName";
            return DbManage.Query<Admin>(sql, new { username = username }).FirstOrDefault();
        }
    }
}