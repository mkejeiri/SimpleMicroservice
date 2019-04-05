using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace SimpleAction.Common.Auth {
    public class JwtHandler : IJwtHandler {
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler ();
        private readonly JwtOptions _options;
        private readonly SecurityKey _issuerSignInKey;
        private readonly SigningCredentials _signingCredentials;
        private readonly JwtHeader _jwtHeader;
        private readonly TokenValidationParameters _tokenValidationParameters;

        public JwtHandler (IOptions<JwtOptions> options) {
            _options = options.Value;
            _issuerSignInKey = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (_options.SecretKey));
            _signingCredentials = new SigningCredentials (_issuerSignInKey, SecurityAlgorithms.HmacSha256);
            _jwtHeader = new JwtHeader (_signingCredentials);
            _tokenValidationParameters = new TokenValidationParameters {
                ValidateAudience = false, //we don't care which client should be authenticated
                ValidIssuer = _options.Issuer, //this would be the service creating the token
                IssuerSigningKey = _issuerSignInKey
            };
        }

        public MyJsonWebToken Create (Guid userId) {
           var nowUtc = DateTime.Now;
           var expires = nowUtc.AddMinutes(_options.ExpiryMinutes);
           //EPOCH : aka Unix timestamps,  number of seconds that have elapsed since January 1, 1970 at 00:00:00 GMT
           //https://www.freeformatter.com/epoch-timestamp-to-date-converter.html

           //Building the payload ...
           var centuryBegin = new DateTime(1970,1,1).ToUniversalTime();
           var exp = (long) (new TimeSpan(expires.Ticks-centuryBegin.Ticks).TotalSeconds);
           var now = (long) (new TimeSpan(nowUtc.Ticks-centuryBegin.Ticks).TotalSeconds);
           var payload = new JwtPayload{
               {"sub", userId},
               {"iss", _options.Issuer}, 
               {"iat", now},
               {"exp", exp},
               {"unique_name", userId}
           };

           var jwt = new JwtSecurityToken(_jwtHeader, payload);
           var token = _jwtSecurityTokenHandler.WriteToken(jwt);
           return new MyJsonWebToken{
               Token = token,
               Expires= exp
           };
        }
    }
}