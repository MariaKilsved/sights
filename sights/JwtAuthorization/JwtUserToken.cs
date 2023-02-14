using System;
namespace sights.JwtAuthorization
{
    public class JwtUserToken
    {
        public Guid TokenId { get; set; }

        public string EncryptedToken { get; set; } = "";
        public string EncryptedRefreshToken { get; set; } = "";  //Not used in this example
        public TimeSpan Validity { get; set; }
        public DateTime ExpiredTime { get; set; }

        //This will be the User part of the Claim
        public long UserId { get; set; }
        public string UserName { get; set; } = "";
       
    }
}


