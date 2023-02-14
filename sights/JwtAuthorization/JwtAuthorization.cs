using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using sights.Models;

namespace sights.JwtAuthorization
{
    public static class JwtAuthorization
    {
        public static IEnumerable<Claim> CreateClaims(JwtUserToken userToken, Guid TokenId)
        {
            IEnumerable<Claim> claims = new Claim[] {
                    new Claim("UserId", userToken.UserId.ToString()),
                    new Claim(ClaimTypes.Name, userToken.UserName),
                    new Claim(ClaimTypes.NameIdentifier, TokenId.ToString()),
                    new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(1).ToString("MMM ddd dd yyyy HH:mm:ss tt"))
            };
            return claims;
        }
        public static IEnumerable<Claim> CreateClaims(JwtUserToken userToken, out Guid Id)
        {
            Id = Guid.NewGuid();
            return CreateClaims(userToken, Id);
        }


        public static JwtUserToken CreateJwtTokenKey(JwtUserToken userToken, JwtSettings jwtSettings)
        {
            var _token = new JwtUserToken();
            if (userToken == null) throw new ArgumentException(nameof(userToken));


            Guid tokenId = Guid.Empty;

            // Generate the secret key
            var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);
            DateTime expireTime = DateTime.UtcNow.AddDays(1);
            _token.Validity = expireTime.TimeOfDay;

            var JWToken = new JwtSecurityToken(issuer: jwtSettings.ValidIssuer,
                audience: jwtSettings.ValidAudience,
                claims: CreateClaims(userToken, out tokenId),
                notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                expires: new DateTimeOffset(expireTime).DateTime,
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256));

            _token.TokenId = tokenId;
            _token.EncryptedToken = new JwtSecurityTokenHandler().WriteToken(JWToken);
            _token.UserId = userToken.UserId;
            _token.UserName = userToken.UserName;

            return _token;
        }


        public static void AddJwtTokenService(this IServiceCollection Services, IConfiguration Configuration)
        {
            // Add Jwt Setings
            var bindJwtSettings = new JwtSettings();
            Configuration.Bind("JsonWebTokenKeys", bindJwtSettings);
            Services.AddSingleton(bindJwtSettings);

            Services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = bindJwtSettings.ValidateIssuerSigningKey,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(bindJwtSettings.IssuerSigningKey)),
                    ValidateIssuer = bindJwtSettings.ValidateIssuer,
                    ValidIssuer = bindJwtSettings.ValidIssuer,
                    ValidateAudience = bindJwtSettings.ValidateAudience,
                    ValidAudience = bindJwtSettings.ValidAudience,
                    RequireExpirationTime = bindJwtSettings.RequireExpirationTime,
                    ValidateLifetime = bindJwtSettings.RequireExpirationTime,
                    ClockSkew = TimeSpan.FromDays(1),
                };
            });
        }
    }
}

