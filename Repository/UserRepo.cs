using System;
using System.Linq;
using DapperExtension.Core;
using Model;
using ServiceDto;

namespace Repository
{
    public class UserRepo : BaseRepo<User>
    {
        public UserRepo(IDbContext dbContext)
            : base(dbContext)
        {
        }

        /// <summary>
        ///  true 未过期，false 过期
        /// </summary>
        public bool CheckToken(string token)
        {
            var sql = @"select count(1) from dbo.[User] where token=@token and DATEDIFF(s, tokenExpireTime , getdate()) < 0;";
            return DbManage.ExecuteScalar<int>(sql, new { token }) > 0;
        }

        public UserDto GetUserByOpenId(string openId)
        {
            var sql = @"select id,nickname,avatarurl,token from dbo.[User] where openid=@openId";
            return DbManage.Query<UserDto>(sql, new { openId }).FirstOrDefault();
        }

        public UserDto GetUserByToken(string token)
        {
            var sql = @"select id,nickname,avatarurl,token from dbo.[User] where token=@token;";
            return DbManage.Query<UserDto>(sql, new { token }).FirstOrDefault();
        }

        public bool UpdateToken(string token, string newToken, DateTime expireTime)
        {
            var sql = @"update dbo.[User] set token=@newToken, tokenExpireTime=@expireTime where token=@token;";
            return DbManage.Execute(sql, new {  token, newToken, expireTime }) > 0;
        }

    }
}