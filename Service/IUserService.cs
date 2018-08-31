using System;
using Model;
using Service.Base;
using ServiceDto;

namespace Service
{
    public interface IUserService : IBaseService<User>
    {
        UserDto GetUserByOpenId(string openId);
        UserDto GetUserByToken(string token);

        string GetSessionInfo(string appId, string appSecret, string code);
        /// <summary>
        /// true-未过期，false-已过期
        /// </summary>
        bool CheckToken(string token);
        bool UpdateToken(string token, string newToken, DateTime expireTime);
    }
}