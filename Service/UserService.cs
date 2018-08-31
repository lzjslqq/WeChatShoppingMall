using System;
using Model;
using Repository;
using Service.Base;
using ServiceDto;
using Utility;

namespace Service
{
    public class UserService : BaseService<User>, IUserService
    {
        public string GetSessionInfo(string appId, string appSecret, string code)
        {
            var apiUrlFmt = "https://api.weixin.qq.com/sns/jscode2session?appid={0}&secret={1}&js_code={2}&grant_type=authorization_code";
            var apiUrl = string.Format(apiUrlFmt, appId, appSecret, code);

            HttpReceiveInfo respInfo;
            HttpSendInfo reqInfo = new HttpSendInfo
            {
                Method = "get",
                Url = apiUrl
            };

            HttpHelper.Send(reqInfo, out respInfo);

            return respInfo.Result;
        }

        public UserDto GetUserByOpenId(string openId)
        {
            if (string.IsNullOrEmpty(openId))
                return null;

            using (var cxt = DbContext(DbOperation.Read))
            {
                var repo = new UserRepo(cxt);
                return repo.GetUserByOpenId(openId);
            }
        }

        public UserDto GetUserByToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                return null;

            using (var cxt = DbContext(DbOperation.Read))
            {
                var repo = new UserRepo(cxt);
                return repo.GetUserByToken(token);
            }
        }

        public bool CheckToken(string token)
        {
            if (string.IsNullOrEmpty(token))
                return false;

            using (var cxt = DbContext(DbOperation.Read))
            {
                var repo = new UserRepo(cxt);
                return repo.CheckToken(token);
            }
        }

        public bool UpdateToken(string token, string newToken, DateTime expireTime)
        {
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(newToken))
                return false;

            using (var cxt = DbContext(DbOperation.Write))
            {
                cxt.BeginTransaction();
                var repo = new UserRepo(cxt);
                var flag = repo.UpdateToken(token, newToken, expireTime);
                if (flag) cxt.Commit();
                else cxt.Rollback();
                return flag;
            }
        }


    }
}
