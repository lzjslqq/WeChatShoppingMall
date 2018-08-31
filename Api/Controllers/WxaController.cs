using System;
using System.Configuration;
using System.Web.Http;
using ClientDto.Api;
using Model;
using Model.Common;
using Service;
using Utility;

namespace Api.Controllers
{
    public class WxaController : ApiController
    {
        public readonly IUserService UserService;
        private static readonly string AppId = ConfigurationManager.AppSettings.Get("appId");
        private static readonly string AppSecret = ConfigurationManager.AppSettings.Get("AppSecret");


        public WxaController(IUserService userService)
        {
            UserService = userService;
        }


        #region 授权与登录的逻辑

        // 微信登录流程获取用户信息
        [HttpGet]
        public SimpleResponse<UserDto> UserLogin(string code, string nickName, string avatarUrl)
        {
            var sessionJson = UserService.GetSessionInfo(AppId, AppSecret, code);
            var session = JsonHelper.Deserialize<dynamic>(sessionJson);

            var openId = (string)session.openid;
            if (!string.IsNullOrEmpty(openId))
            {
                // 1. 使用 openid 查询用户信息（若不存在，则新增用户）；2. token 更新至数据库，并更新时效；

                var userInfo = UserService.GetUserByOpenId(openId);

                if (userInfo == null || userInfo.Id == 0)
                {

                    #region 用户不存在

                    var token = SecurityHelper.EncryptSHA1(DateTime.Now.ConvertTimeStamp().ToString());
                    var expireTime = DateTime.Now.AddDays(1);

                    long id = UserService.Insert(new User
                    {
                        NickName = nickName,
                        AvatarUrl = avatarUrl,
                        OpenId = openId,
                        Token = token,
                        TokenExpireTime = expireTime,
                        RecentLoginTime = DateTime.Now,
                        RecentPayTime = DateTime.Now,
                        CreateTime = DateTime.Now,
                        UpdateTime = DateTime.Now,
                        Status = 1,
                        IsDeleted = 0,
                    });

                    if (id > 0)
                    {
                        return new ComplexResponse<UserDto>(1, "成功！", new UserDto()
                        {
                            Id = (int)id,
                            NickName = nickName,
                            AvatarUrl = avatarUrl,
                            Token = token
                        });
                    }
                    return new ComplexResponse<UserDto>(-1, "用户信息写入失败！");

                    #endregion

                }
                else
                {

                    #region 用户已存在

                    // 更新 token
                    var token = userInfo.Token;
                    var newToken = SecurityHelper.EncryptSHA1(DateTime.Now.ConvertTimeStamp().ToString());
                    var expireTime = DateTime.Now.AddDays(1);
                    var flag = UserService.UpdateToken(token, newToken, expireTime);

                    if (flag)
                    {
                        return new ComplexResponse<UserDto>(1, "成功！", new UserDto()
                        {
                            Id = userInfo.Id,
                            NickName = userInfo.NickName,
                            AvatarUrl = userInfo.AvatarUrl,
                            Token = newToken
                        });
                    }
                    return new ComplexResponse<UserDto>(-1, "token 更新失败！");

                    #endregion

                }
            }

            return new ComplexResponse<UserDto>(-1, "openid 获取失败！");
        }

        // 通过 token 查询用户信息
        [HttpGet]
        public SimpleResponse<UserDto> GetUserInfo(string token)
        {
            // 1. 先判断 token 是否过期。若没过期，则直接取用户信息；若已过期，则先更新token，再返回用户信息；。
            var flag = UserService.CheckToken(token);

            if (!flag)
            {// 已过期，更新token
                var newToken = SecurityHelper.EncryptSHA1(DateTime.Now.ConvertTimeStamp().ToString());
                var expireTime = DateTime.Now.AddDays(1);
                flag = UserService.UpdateToken(token, newToken, expireTime);
            }
            if (flag)
            {
                // 取用户信息
                var userInfo = UserService.GetUserByToken(token);
                if (userInfo != null && userInfo.Id > 0)
                {
                    return new ComplexResponse<UserDto>(1, "成功！", new UserDto
                    {
                        Id = userInfo.Id,
                        NickName = userInfo.NickName,
                        AvatarUrl = userInfo.AvatarUrl,
                        Token = token
                    });
                }
            }

            return new ComplexResponse<UserDto>(-1, "token 更新异常！");
        }

        #endregion

    }
}
