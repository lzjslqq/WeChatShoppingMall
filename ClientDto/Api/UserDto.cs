using System;

namespace ClientDto.Api
{
    public class UserDto
    {
        public int Id { get; set; }
        public string NickName { get; set; }
        public string AvatarUrl { get; set; }
        public string OpenId { get; set; }
        public string Token { get; set; }
        public DateTime ExpireTime { get; set; }
    }
}