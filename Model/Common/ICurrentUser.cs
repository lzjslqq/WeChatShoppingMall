using System;

namespace Model.Common
{
    /// <summary>
    /// 当前登录用户
    /// </summary>
    public interface ICurrentUser
    {
        string LoginedId { get; set; }
        string OpenId { get; set; }
        string Token { get; set; }
        string State { get; set; }
        int UserId { get; set; }
        string UserName { get; set; }
        string NickName { get; set; }
        DateTime LoginTime { get; set; }
    }
}