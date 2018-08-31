using System;

namespace Model.Common
{
    [Serializable]
    public class CurrentUser : ICurrentUser
    {
        public string LoginedId { get; set; }

        public string OpenId { get; set; }

        public string Token { get; set; }

        public string State { get; set; }

        public int UserId { get; set; }

        public string UserName { get; set; }

        public string NickName { get; set; }

        public DateTime LoginTime { get; set; }

        public CurrentUser(string loginedId = "")
        {
            LoginedId = loginedId;
            OpenId = "";
            Token = "";
            State = "";
            UserId = 0;
            UserName = "";
            NickName = "";
            LoginTime = DateTime.Now;
        }
    }
}